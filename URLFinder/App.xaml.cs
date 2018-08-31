using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using URLFinder.Utilities;

namespace URLFinder
{
	/// <summary>
	/// App.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class App : Application
	{
		public App ()
		{
			RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
		}

		protected override void OnStartup ( StartupEventArgs e )
		{
			if ( e.Args.Length >= 1 && e.Args [ 0 ] == "--startuputil" )
				DoStartupUtility ( e.Args );

			base.OnStartup ( e );
		}

		private void DoStartupUtility ( string [] args )
		{
			var now = DateTime.Now;
			var path = $@"{CustomizedValue.WorkingDirectory}\{now.Year}\{now.Month:00}\{now.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{now.ToString ( "yyMMdd" )}\";
			if ( !Directory.Exists ( path ) )
				Directory.CreateDirectory ( path );

			var hwpFilename = $@"{path}모니터링 일지-{CustomizedValue.WorkerName}-{now.ToString ( "yyMMdd" )}.hwp";
			var excelFilename = $@"{path}엑셀자료-{CustomizedValue.WorkerName}-{now.ToString ( "yyMMdd" )}.xlsx";

			if ( !File.Exists ( hwpFilename ) )
			{
				File.Copy ( $@"{CustomizedValue.TemplateDirectory}\모니터링 일지-{CustomizedValue.WorkerName}-180101.hwp", hwpFilename );
			}
			if ( !File.Exists ( excelFilename ) )
			{
				File.Copy ( $@"{CustomizedValue.TemplateDirectory}\엑셀자료-{CustomizedValue.WorkerName}-180101.xlsx", excelFilename );

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

					connection.Close ();
				}
			}
			
			Process.Start ( $"\"{hwpFilename}\"" );
			Process.Start ( $"\"{excelFilename}\"" );

			var files = Directory.GetFiles ( $@"{CustomizedValue.WorkingDirectory}\전체 색출자료 및 색출면탈 관리사이트", "병역면탈조장관리사이트*-*.xlsx", SearchOption.TopDirectoryOnly );
			Process.Start ( $"\"{( from file in files orderby File.GetLastWriteTime ( file ) descending select file ).FirstOrDefault ()}\"" );
		}
	}
}
