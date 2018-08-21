using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URLFinder
{
	static class DateTimeExtensions
	{
		static GregorianCalendar _gc = new GregorianCalendar ();
		public static int GetWeekOfMonth ( this DateTime time )
		{
			DateTime first = new DateTime ( time.Year, time.Month, 1 );
			return time.GetWeekOfYear () - first.GetWeekOfYear () + 1;
		}

		static int GetWeekOfYear ( this DateTime time )
		{
			return _gc.GetWeekOfYear ( time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday );
		}
	}

	static class Program
	{
		public static readonly string Name = "진재연";

		[STAThread]
		static void Main ( string [] args )
		{
			if ( args.Length >= 1 && args [ 0 ] == "--startuputil" )
				DoStartupUtility ( args );

			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault ( false );
			Application.Run ( new MainForm () );
		}

		private static void DoStartupUtility ( string [] args )
		{
			var now = DateTime.Now;
			var path = $@"D:\모니터링 일지\{now.Year}\{now.Month:00}\{now.GetWeekOfMonth ()}주\모니터링일지-{Name}-{now.ToString ( "yyMMdd" )}\";
			if ( !Directory.Exists ( path ) )
				Directory.CreateDirectory ( path );

			var hwpFilename = $@"{path}모니터링 일지-{Name}-{now.ToString ( "yyMMdd" )}.hwp";
			var excelFilename = $@"{path}엑셀자료-{Name}-{now.ToString ( "yyMMdd" )}.xlsx";

			if ( !File.Exists ( hwpFilename ) )
			{
				File.Copy ( $@"D:\모니터링 일지\일지 양식\모니터링 일지-{Name}-180101.hwp", hwpFilename );
			}
			if ( !File.Exists ( excelFilename ) )
			{
				File.Copy ( $@"D:\모니터링 일지\일지 양식\엑셀자료-{Name}-180101.xlsx", excelFilename );

				using ( OleDbConnection connection = new OleDbConnection (
					$"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"{excelFilename}\";Extended Properties=\"Excel 12.0;HDR=NO\""
				) )
				{
					connection.Open ();

					using ( OleDbCommand command = new OleDbCommand (
						$"UPDATE [엑셀자료$E2:E2] SET F1='{now.ToString ( "yyyy-MM-dd" )}'",
						connection
					) )
						command.ExecuteNonQuery ();

					/*if ( Name != "진재연" )
					{
						using ( OleDbCommand command = new OleDbCommand (
							$"UPDATE [엑셀자료$AB2:AB2] SET F1='{Name}'",
							connection
						) )
							command.ExecuteNonQuery ();
					}*/

					connection.Close ();
				}
			}

			//Process.Start ( "explorer", $"\"{path}\"" );
			Process.Start (
				@"C:\HNC\Hwp70\Hwp.exe",
				$"\"{hwpFilename}\""
			);
			Process.Start (
				@"C:\Program Files\Microsoft Office\Office12\EXCEL.EXE",
				$"\"{excelFilename}\""
			);

			var files = Directory.GetFiles (
				@"D:\모니터링 일지\전체 색출자료 및 색출면탈 관리사이트",
				"병역면탈조장관리사이트*-*.xlsx",
				SearchOption.TopDirectoryOnly );
			Process.Start (
				@"C:\Program Files\Microsoft Office\Office12\EXCEL.EXE",
				$"\"{( from file in files orderby File.GetLastWriteTime ( file ) descending select file ).FirstOrDefault ()}\""
			);
		}
	}
}
