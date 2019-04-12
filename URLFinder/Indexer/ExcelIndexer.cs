using Daramee.Winston.File;
using LiteDB;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using URLFinder.Processors;
using URLFinder.Utilities;

namespace URLFinder.Finder
{
	public class IndexedItem
	{
		[BsonId]
		public ObjectId Id { get; set; }
		public string WebsiteName { get; set; }
		public string URL { get; set; }
		public string BaseURL { get; set; }
		public string Filename { get; set; }

		public IndexedItem () { }

		public IndexedItem ( string website, string url, string baseUrl, string filename )
		{
			WebsiteName = website;
			URL = url;
			BaseURL = baseUrl;
			Filename = filename;
		}

		public override int GetHashCode ()
		{
			return ToString ().GetHashCode ();
		}

		public override string ToString () => $"{{{WebsiteName}, {URL}, {BaseURL}, {Filename}}}";
	}

	public class ExcelIndexerFindState
	{
		internal int proceed;
		public int Proceed => proceed;
	}

	public class ExcelIndexer
	{
		public static ExcelIndexer SharedExcelIndexer { get; private set; }

		readonly List<IndexedItem> indexedItems;
		readonly List<DateTime> indexedDates;

		public IEnumerable<IndexedItem> IndexedItems => indexedItems;
		public IEnumerable<DateTime> IndexedDates => indexedDates;
		public int Count => indexedItems.Count;

		internal ExcelIndexer ( IEnumerable<IndexedItem> indexed, IEnumerable<DateTime> dates )
		{
			indexedItems = indexed.ToList ();
			indexedDates = dates.ToList ();
			SharedExcelIndexer = this;
		}

		public IEnumerable<string> Find ( string url, ExcelIndexerFindState state = null )
		{
			if ( state != null )
				state.proceed = 0;
			ConcurrentQueue<string> queue = new ConcurrentQueue<string> ();
			bool finished = false;
			Task.Factory.StartNew ( () =>
			{
				foreach ( var index in from index in indexedItems.AsParallel () where index.URL.Contains ( url ) select index )
				{
					queue.Enqueue ( index.Filename );
					if ( state != null )
						Interlocked.Increment ( ref state.proceed );
				};
				finished = true;
				if ( state != null )
					state.proceed = indexedItems.Count;
			} );

			while ( !finished || queue.Count > 0 )
			{
				if ( queue.Count == 0 )
					continue;
				if ( !queue.TryDequeue ( out string result ) )
					continue;
				yield return result;
			}
		}

		public BaseProcessor GetGuessedProcessor ( string url )
		{
			string guessedBaseUrl = new Uri ( url.Substring ( 0, url.IndexOf ( '/', 9 ) ) ).Host;
			var foundIndex = ( from index in indexedItems.AsParallel () where index.BaseURL.Contains ( guessedBaseUrl ) select index ).FirstOrDefault ();
			if ( foundIndex != null )
			{
				return new SimpleProcessor ( foundIndex.WebsiteName, new Uri ( foundIndex.BaseURL ) );
			}
			return null;
		}
	}

	public class ExcelIndexerBuilderState
	{
		internal int totalItemCount = 0, excelFileCount = 0;

		public ConcurrentQueue<string> OpeningFailedFiles { get; private set; } = new ConcurrentQueue<string> ();
		public int TotalItemCount => totalItemCount;
		public int ExcelFileCount => excelFileCount;
	}

	public static class ExcelIndexerBuilder
	{
		private readonly static string todayRegexString = $"(.*)모니터링일지-[가-힣]+-{DateTime.Today.ToString ( "yyMMdd" )}\\\\(.*).xlsx";
		private readonly static string selectQuery = "SELECT * FROM [엑셀자료$]";

		private class CacheFiles
		{
			[BsonId]
			public ObjectId Id { get; set; }
			public string Filename { get; set; }
			public DateTime LastModifiedDateTime { get; set; }
		}

		public static async Task<ExcelIndexer> ToIndexer ( ExcelIndexerBuilderState state = null )
		{
			IndexedItem [] indexedItems = null;
			DateTime [] indexedDates = null;

			await Task.Run ( () =>
			{
				using ( LiteDatabase db = new LiteDatabase ( $"Filename=\"{Path.Combine ( Program.ProgramPath, "ExcelFilesCache.litedb" )}\"; Journal=true; Mode=Shared; Flush=true;" ) )
				{
					var cacheFilesCollection = db.GetCollection<CacheFiles> ( "cachefiles" );
					var cachedItemsCollection = db.GetCollection<IndexedItem> ( "indexed" );

					var newFile = new ConcurrentQueue<CacheFiles> ();
					var newRecord = new ConcurrentQueue<IndexedItem> ();

					Parallel.ForEach ( FilesEnumerator.EnumerateFiles ( CustomizedValue.WorkingDirectory, "엑셀자료-*.xlsx", false ).AsParallel (), ( file ) =>
					{
						if ( file.Contains ( "일지 양식" ) || file.Contains ( "샘플" ) || Regex.IsMatch ( file, todayRegexString ) )
							return;
						var fileItem = cacheFilesCollection.FindOne ( Query.EQ ( "Filename", file ) );
						if ( fileItem != null )
						{
							if ( fileItem.LastModifiedDateTime.ToString () == File.GetLastWriteTime ( file ).ToString () )
							{
								if ( state != null )
								{
									Interlocked.Increment ( ref state.excelFileCount );
									int recordCount = cachedItemsCollection.Find ( Query.EQ ( "Filename", file ) ).Count ();
									Interlocked.Add ( ref state.totalItemCount, recordCount );
									FinderLog.Log ( $"{Path.GetFileName ( file )}으로부터 {recordCount}개를 이미 인덱싱 했음." );
								}
								return;
							}
							else
							{
								cachedItemsCollection.Delete ( Query.EQ ( "Filename", file ) );
								cacheFilesCollection.Delete ( Query.EQ ( "Filename", file ) );
								FinderLog.Log ( $"{Path.GetFileName ( file )}의 인덱싱 데이터가 최신이 아니므로 재인덱싱 시작." );
							}
						}
						else
							FinderLog.Log ( $"{Path.GetFileName ( file )}은 인덱싱되지 않은 파일임." );

						try
						{
							OleDbConnection connection = new OleDbConnection (
								$"Provider=\"Microsoft.ACE.OLEDB.12.0\";Data Source=\"{file}\";Extended Properties=\"Excel 12.0;HDR=NO\";"
							);
							connection.Open ();

							int count = 0;
							//Regex.Replace ( connection.GetSchema ( "Tables" ).Rows [ 1 ] [ "TABLE_NAME" ] as string, "['\"]", "" );
							using ( OleDbCommand command = new OleDbCommand (
												   selectQuery,
												   connection
											   ) )
							{

								using ( var reader = command.ExecuteReader () )
								{
									if ( !reader.Read () )
									{
										state?.OpeningFailedFiles.Enqueue ( file );
										return;
									}

									while ( reader.Read () )
									{
										object websiteName = reader.GetValue ( 8 );
										object url = reader.GetValue ( 9 );
										object baseUrl = reader.GetValue ( 31 );

										if ( websiteName == DBNull.Value || url == DBNull.Value || baseUrl == DBNull.Value )
											break;

										newRecord.Enqueue ( new IndexedItem ( websiteName as string, url as string, baseUrl as string, file ) );
										
										++count;
									}
								}
							}

							connection.Close ();

							if ( state != null )
							{
								Interlocked.Add ( ref state.totalItemCount, count );
								Interlocked.Increment ( ref state.excelFileCount );
							}

							newFile.Enqueue ( new CacheFiles () { Filename = file, LastModifiedDateTime = File.GetLastWriteTime ( file ) } );

							FinderLog.Log ( $"{Path.GetFileName ( file )}의 데이터 {count}개 캐시 완료." );
						}
						catch ( Exception ex )
						{
							FinderLog.Log ( $"{Path.GetFileName ( file )} 캐시 중 오류 발생: {ex.ToString ()}" );
							state?.OpeningFailedFiles.Enqueue ( file );
						}
					} );

					cacheFilesCollection.Insert ( newFile );
					cachedItemsCollection.Insert ( newRecord );

					db.Shrink ();

					indexedItems = new List<IndexedItem> ( cachedItemsCollection.FindAll () ).ToArray ();
					List<DateTime> dates = new List<DateTime> ();
					foreach ( var f in cacheFilesCollection.FindAll () )
					{
						string date = "20" + Regex.Match ( f.Filename, $"(.*)모니터링일지-[가-힣]+-([0-9][0-9][0-1][0-9][0-3][0-9])\\\\(.*).xlsx" ).Groups [ 2 ].Value;
						dates.Add ( new DateTime ( int.Parse ( date.Substring ( 0, 4 ) ), int.Parse ( date.Substring ( 4, 2 ) ), int.Parse ( date.Substring ( 6, 2 ) ) ) );
					}
					indexedDates = dates.ToArray ();
				}
			} );

			GC.Collect ();

			return new ExcelIndexer ( indexedItems, indexedDates );
		}
	}
}