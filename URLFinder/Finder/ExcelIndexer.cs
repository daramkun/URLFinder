using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
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
		public string WebsiteName { get; private set; }
		public string URL { get; private set; }
		public string BaseURL { get; private set; }
		public string Filename { get; private set; }

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

		List<IndexedItem> indexedItems;
		List<DateTime> indexedDates;

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
		public static async Task<ExcelIndexer> ToIndexer ( ExcelIndexerBuilderState state = null )
		{
			ConcurrentBag<IndexedItem> indexedItems = new ConcurrentBag<IndexedItem> ();
			ConcurrentBag<DateTime> indexedDates = new ConcurrentBag<DateTime> ();

			await Task.Run ( () =>
			{
				Parallel.ForEach ( FilesEnumerator.EnumerateFiles ( CustomizedValue.WorkingDirectory, "엑셀자료-*.xlsx", false ), ( file ) =>
				{
					if ( file.Contains ( "일지 양식" ) || file.Contains ( "샘플" ) )
						return;

					if ( Regex.IsMatch ( file, $"(.*)모니터링일지-[가-힣]+-{DateTime.Now.ToString ( "yyMMdd" )}\\\\(.*).xlsx" ) )
						return;

					try
					{
						OleDbConnection connection = new OleDbConnection (
							$"Provider=\"Microsoft.ACE.OLEDB.12.0\";Data Source=\"{file}\";Extended Properties=\"Excel 12.0;HDR=NO\";"
						);
						connection.Open ();

						string tableName = "엑셀자료$";//Regex.Replace ( connection.GetSchema ( "Tables" ).Rows [ 1 ] [ "TABLE_NAME" ] as string, "['\"]", "" );

						using ( OleDbCommand command = new OleDbCommand (
												$"SELECT * FROM [{tableName}]",
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

									indexedItems.Add ( new IndexedItem ( websiteName as string, url as string, baseUrl as string, file ) );

									if ( state != null )
										Interlocked.Increment ( ref state.totalItemCount );
								}
							}
						}

						connection.Close ();

						if ( state != null )
							Interlocked.Increment ( ref state.excelFileCount );

						string date = "20" + Regex.Match ( file, $"(.*)모니터링일지-[가-힣]+-([0-9][0-9][0-1][0-9][0-3][0-9])\\\\(.*).xlsx" ).Groups [ 2 ].Value;
						indexedDates.Add ( new DateTime ( int.Parse ( date.Substring ( 0, 4 ) ), int.Parse ( date.Substring ( 4, 2 ) ), int.Parse ( date.Substring ( 6, 2 ) ) ) );
					}
					catch ( Exception ex )
					{
						Debug.WriteLine ( ex.ToString () );
						state?.OpeningFailedFiles.Enqueue ( file );
					}
				} );
			} );

			return new ExcelIndexer ( indexedItems, indexedDates );
		}
	}
}
