using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using URLFinder.Utilities;

namespace URLFinder.Finders
{
	public sealed class ExcelFinder : IDisposable
	{
		ConcurrentDictionary<string, OleDbConnection> excels
			= new ConcurrentDictionary<string, OleDbConnection> ();
		ConcurrentDictionary<OleDbConnection, string> tableNames
			= new ConcurrentDictionary<OleDbConnection, string> ();
		ConcurrentQueue<string> cannotOpenedFiles = new ConcurrentQueue<string> ();

		public IEnumerable<string> CannotOpenedFiles => cannotOpenedFiles;

		int proceed;
		public int Procceed => proceed;
		public int Count => excels.Count;

		public ExcelFinder ( string findingPathes, string findingPatterns )
		{
			string [] pathes = findingPathes.Split ( '|' );
			string [] patterns = findingPatterns.Split ( '|' );

			foreach ( string path in pathes )
			{
				foreach ( string pattern in patterns )
				{
					//Parallel.ForEach ( Directory.GetFiles ( path, pattern, SearchOption.AllDirectories ), ( file ) =>
					//foreach ( var file in Directory.GetFiles ( path, pattern, SearchOption.AllDirectories ) )
					Parallel.ForEach ( FilesEnumerator.EnumerateFiles(path, pattern, false ), ( file ) => 
					{
						if ( Regex.IsMatch ( file, $"(.*)모니터링일지-[가-힣]+-{DateTime.Now.ToString ( "yyMMdd" )}.xlsx" ) )
							return;

						try
						{
							OleDbConnection connection = new OleDbConnection (
								$"Provider=\"Microsoft.ACE.OLEDB.12.0\";Data Source=\"{file}\";Extended Properties=\"Excel 12.0;HDR=NO\";"
							);
							connection.Open ();

							excels.TryAdd ( file, connection );

							string tableName = Regex.Replace ( connection.GetSchema ( "Tables" ).Rows [ 1 ] [ "TABLE_NAME" ] as string, "['\"]", "" );
							tableNames.TryAdd ( connection, tableName );

							connection.Close ();
						}
						catch ( Exception ex )
						{
							Debug.WriteLine ( ex.Message );
							cannotOpenedFiles.Enqueue ( file );
						}
					} );
				}
			}
		}

		~ExcelFinder ()
		{
			Dispose ();
		}

		public void Dispose ()
		{
			foreach ( var kv in excels )
				kv.Value.Dispose ();
			excels = null;
			cannotOpenedFiles = null;

			GC.SuppressFinalize ( this );
		}

		public void Clear () { proceed = 0; }

		public IEnumerable<string> Find ( string url )
		{
			proceed = 0;
			ConcurrentQueue<string> queue = new ConcurrentQueue<string> ();
			bool finished = false;
			Task.Factory.StartNew ( () =>
			{
				Parallel.ForEach ( excels, ( kv ) =>
				{
					var connection = kv.Value;
					var tableName = tableNames [ connection ];
					using ( OleDbCommand command = new OleDbCommand (
											$"SELECT * FROM [{tableName}J2:J9999] WHERE F1 LIKE'%{url}%'",
											connection
										) )
					{
						command.Connection.Open ();
						using ( var reader = command.ExecuteReader () )
						{
							if ( reader.Read () )
								queue.Enqueue ( kv.Key );
						}
						command.Connection.Close ();
						Interlocked.Increment ( ref proceed );
					}
				} );
				finished = true;
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
	}
}
