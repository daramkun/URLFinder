using System;
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

		static FinderLog ()
		{
			streamWriter = File.AppendText ( "URLFinder.log" );
			streamWriter.WriteLine ( "=============================================" );
			streamWriter.WriteLine ( " URL Finder v3 Log Data" );
			streamWriter.WriteLine ( "=============================================" );
		}

		public static void Log ( string message )
		{
			StringBuilder logMsg = new StringBuilder ();
			logMsg.AppendFormat ( "[{0:yyyy-MM-dd hh:mm:ss}]", DateTime.Now ).AppendFormat ( "[{0:x}]", Thread.CurrentThread.ManagedThreadId )
				.Append ( message ).Append ( Environment.NewLine );
			message = logMsg.ToString ();

			Debug.Write ( message );
			streamWriter.Write ( message );
		}

		public static void Flush () { streamWriter.Flush (); }
	}
}
