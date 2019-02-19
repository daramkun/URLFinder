using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace URLFinder.Utilities
{
	public static class FinderLog
	{
		static StreamWriter streamWriter;
		static ConcurrentQueue<string> logQueue = new ConcurrentQueue<string> ();

		static FinderLog ()
		{
			streamWriter = new StreamWriter ( new FileStream ( Path.Combine ( Program.ProgramPath, "URLFinder.log" ),
				FileMode.Append, FileAccess.Write, FileShare.ReadWrite ), Encoding.UTF8, 4096, false )
			{
				AutoFlush = true
			};

			streamWriter.WriteLine ( "=============================================" );
			streamWriter.WriteLine ( " URL Finder v3 Log Data" );
			streamWriter.WriteLine ( "=============================================" );

			new Thread ( () =>
			{
				try
				{
					while ( Program.ProcessRunning )
					{
						while ( logQueue.TryDequeue ( out string message ) )
						{
							Debug.Write ( message );
							streamWriter.Write ( message );
						}
						Thread.Sleep ( 1 );
					}
				}
				catch { }
			} ).Start ();
		}

		public static void Log ( string message )
		{
			StringBuilder logMsg = new StringBuilder ();
			logMsg.AppendFormat ( "[{0:yyyy-MM-dd hh:mm:ss}]", DateTime.Now )
				.Append ( message ).Append ( Environment.NewLine );
			message = logMsg.ToString ();
			
			logQueue.Enqueue ( message );
		}

		public static void Flush () { streamWriter.Flush (); }
	}
}
