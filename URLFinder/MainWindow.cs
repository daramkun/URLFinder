using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using URLFinder.Finder;
using URLFinder.Processors;
using URLFinder.Properties;
using URLFinder.Utilities;

namespace URLFinder
{
	public partial class MainWindow : Form
	{
		readonly Thread uiThread;

		ExcelIndexer indexer;

		public MainWindow ()
		{
			uiThread = Thread.CurrentThread;

			InitializeComponent ();

			buttonRegisterStartProgram.Enabled = !StartupRegistry.IsRegistered;
			buttonRegisterStartProgramWithStartupUtility.Enabled = !StartupRegistry.IsRegistered;
			buttonUnregisterStartProgram.Enabled = StartupRegistry.IsRegistered;
		}

		public TreeNode Log ( string v, Color color, TreeNode parentItem = null )
		{
			if ( Thread.CurrentThread != uiThread )
			{
				TreeNode ret = null;
				Invoke ( new Action ( () =>
				{
					ret = __Log ( v, color, parentItem );
				} ) );
				return ret;
			}
			else return __Log ( v, color, parentItem );
		}
		private TreeNode __Log ( string v, Color color, TreeNode parentItem )
		{
			var item = new TreeNode ()
			{
				Text = v,
				BackColor = color,
			};
			if ( parentItem == null )
				treeViewLog.Nodes.Add ( item );
			else
			{
				parentItem.Nodes.Add ( item );
				parentItem.Expand ();
			}
			return item;
		}

		private async void MainWindow_Shown ( object sender, EventArgs e )
		{
			ExcelIndexerBuilderState builderState = new ExcelIndexerBuilderState ();
			TreeNode initializeItem = Log ( "[초기화중][검색자 초기화 수행 중][0개 찾음][0개 엑셀 파일 인덱싱 됨]", Color.Transparent );

			Thread counter = new Thread ( () =>
			{
				int lastCount = -1;
				while ( true )
				{
					if ( indexer != null )
					{
						Invoke ( new Action ( () =>
						{
							initializeItem.Text = $"[초기화중][검색자 초기화 수행 중][{builderState.TotalItemCount}개 찾음][{builderState.ExcelFileCount}개 엑셀 파일 인덱싱 됨]";
							while ( builderState.OpeningFailedFiles.TryDequeue ( out string file ) )
								Log ( $"[오류발생]{Path.GetFileName ( file )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.", Color.LightSalmon, initializeItem );
						} ) );
						break;
					}

					Invoke ( new Action ( () =>
					{
						if ( lastCount != builderState.TotalItemCount )
						{
							lastCount = builderState.TotalItemCount;
							initializeItem.Text = $"[초기화중][검색자 초기화 수행 중][{lastCount}개 찾음][{builderState.ExcelFileCount}개 엑셀 파일 인덱싱 됨]";
						}

						if ( builderState.OpeningFailedFiles.TryDequeue ( out string file ) )
						{
							Log ( $"[오류발생]{Path.GetFileName ( file )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.", Color.LightSalmon, initializeItem );
						}
					} ) );

					Thread.Sleep ( 100 );
				}
			} );
			counter.Start ();

			indexer = await ExcelIndexerBuilder.ToIndexer ( builderState );

			var sorted = from date in indexer.IndexedDates orderby date ascending select date;
			monthCalendar.MinDate = sorted.First ();
			monthCalendar.MaxDate = DateTime.Today;

			for ( DateTime d = sorted.First (); d < DateTime.Today; d = d.AddDays ( 1 ) )
			{
				if ( ( indexer.IndexedDates as List<DateTime> ).Contains ( d ) )
					monthCalendar.AddBoldedDate ( d );
			}

			MonthCalendar_DateSelected ( monthCalendar, new DateRangeEventArgs ( DateTime.Today, DateTime.Today ) );

			tabControlKeywords.SelectedIndex = ( ( int ) DateTime.Today.DayOfWeek ) - 1;

			textBoxKeywordMonday.Text = Settings.Default.search_keyword_mon;
			textBoxKeywordTuesday.Text = Settings.Default.search_keyword_tue;
			textBoxKeywordWednesday.Text = Settings.Default.search_keyword_wed;
			textBoxKeywordThursday.Text = Settings.Default.search_keyword_thu;
			textBoxKeywordFriday.Text = Settings.Default.search_keyword_fri;
			textBoxKeywordAdditional.Text = Settings.Default.search_keyword_extra;

			tabControl.Enabled = true;
		}

		private void MainWindow_FormClosing ( object sender, FormClosingEventArgs e )
		{
			Settings.Default.search_keyword_mon = textBoxKeywordMonday.Text;
			Settings.Default.search_keyword_tue = textBoxKeywordTuesday.Text;
			Settings.Default.search_keyword_wed = textBoxKeywordWednesday.Text;
			Settings.Default.search_keyword_thu = textBoxKeywordThursday.Text;
			Settings.Default.search_keyword_fri = textBoxKeywordFriday.Text;
			Settings.Default.search_keyword_extra = textBoxKeywordAdditional.Text;
			Settings.Default.Save ();

			CustomizedValue.Save ();
		}

		private void TextBoxSearch_Pasted ( object sender, EventArgs e )
		{
			string clipboardText = Clipboard.GetText ();

			BaseProcessor processor = ProcessorFinder.FindProcessor ( clipboardText, false );
			if ( processor != null )
				textBoxSearch.Text = processor.ConvertUrl ( clipboardText );
			else
				textBoxSearch.Text = clipboardText;
		}

		private async void ButtonSearch_Click ( object sender, EventArgs e )
		{
			if ( string.IsNullOrEmpty ( textBoxSearch.Text ) )
			{
				Log ( "[오류발생]URL을 입력해주세요.", Color.LightSalmon );
				return;
			}
			if ( !Regex.IsMatch ( textBoxSearch.Text, "([a-zA-Z0-9]+://)?([a-zA-Z0-9\\.]+)\\.[a-zA-Z0-9]+(\\/[a-zA-Z0-9가-힣_\\-&%+?/.=]*)?" ) )
			{
				Log ( "[오류발생]URL 형식으로 입력해주세요.", Color.LightSalmon );
				return;
			}

			string url = textBoxSearch.Text;
			url = Regex.Replace ( url, "https?://", "" );

			textBoxSearch.Enabled = false;
			buttonSearch.Enabled = false;

			ExcelIndexerFindState findState = new ExcelIndexerFindState ();
			var countItem = Log ( $"[검색시작][{url}][0/{indexer.Count}]", Color.Transparent );

			Thread counter = new Thread ( () =>
			{
				int recent = 0;
				while ( true )
				{
					Invoke ( new Action ( () =>
					{
						if ( indexer == null )
							return;
						if ( recent == findState.Proceed )
							return;
						countItem.Text = $"[검색시작][{url}][{findState.Proceed}/{indexer.Count}]";
						recent = findState.Proceed;
					} ) );

					if ( recent == indexer.Count )
						break;

					Thread.Sleep ( 50 );
				}
			} );
			counter.Start ();

			await Task.Run ( () =>
			{
				foreach ( var file in indexer.Find ( url, findState ) )
					Log ( $"[항목찾음]{Path.GetFileName ( file )}", Color.LightGreen, countItem );
			} );

			counter.Join ( 100 );

			buttonSearch.Enabled = true;
			textBoxSearch.Enabled = true;
		}

		private void ButtonMergedCopy_Click ( object sender, EventArgs e )
		{
			string text = Regex.Replace ( Regex.Replace ( textBoxLineMerge.Text, "[\r\n]+", " " ), "( [ ]+)", "" );
			Clipboard.SetText ( text );
			textBoxLineMerge.Text = "";
		}

		private string DeleteSlash ( string v )
		{
			if ( v [ v.Length - 1 ] == '/' )
				return v.Substring ( 0, v.Length - 1 );
			return v;
		}

		private void ButtonSplit_Click ( object sender, EventArgs e )
		{
			var urlTitle = textBoxBeforeSplit.Text.Split ( new string [] { "\r\n", "\n" }, StringSplitOptions.None );
			StringBuilder urlList = new StringBuilder ();
			StringBuilder titleList = new StringBuilder ();
			StringBuilder webSiteList = new StringBuilder ();
			StringBuilder managedUrlList = new StringBuilder ();

			string nextPostfix = null;
			foreach ( string text in urlTitle )
			{
				if ( string.IsNullOrEmpty ( text.Trim () ) )
					continue;
				if ( Regex.IsMatch ( text, "https?://(.*)" ) )
				{
					BaseProcessor processor = ProcessorFinder.FindProcessor ( text );

					urlList.Append ( text ).Append ( Environment.NewLine );
					var managedUrl = processor.GetDetailBaseUrl ( text ).AbsoluteUri;
					managedUrlList.Append ( DeleteSlash ( managedUrl ) ).Append ( Environment.NewLine );
					string websiteName = processor.GetDetailWebSiteName ( text );
					webSiteList.Append ( websiteName ).Append ( Environment.NewLine );

					switch ( websiteName )
					{
						case "페이스북":
						case "트위터": nextPostfix = ""; break;
						default: nextPostfix = "(제목) "; break;
					}
				}
				else
				{
					titleList.Append ( text.Trim () ).Append ( nextPostfix );
					titleList.Append ( Environment.NewLine );
				}
			}

			textBoxSplittedURL.Text = urlList.ToString ();
			textBoxSplittedTitle.Text = titleList.ToString ();
			textBoxSplittedSiteName.Text = webSiteList.ToString ();
			textBoxSplittedBaseURL.Text = managedUrlList.ToString ();
		}

		private void MonthCalendar_DateSelected ( object sender, DateRangeEventArgs e )
		{
			bool enabled = indexer.IndexedDates.Contains ( e.Start.Date )
				|| ( e.Start.Date == DateTime.Today );
			buttonManagementOpenFolder.Enabled =
				buttonManagementOpenHWP.Enabled =
				buttonManagementOpenXLSX.Enabled =
				buttonManagementOpenPDFs.Enabled = enabled;
			buttonManagementArchiving.Enabled = e.Start.Date == DateTime.Today;
		}

		private void ButtonManagementOpenFolder_Click ( object sender, EventArgs e )
		{
			var date = monthCalendar.SelectionStart;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\";
			Process.Start ( "explorer", path );
		}

		private void ButtonManagementOpenHWP_Click ( object sender, EventArgs e )
		{
			var date = monthCalendar.SelectionStart;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\모니터링 일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}.hwp";
			Process.Start ( path );
		}

		private void ButtonManagementOpenXLSX_Click ( object sender, EventArgs e )
		{
			var date = monthCalendar.SelectionStart;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\엑셀자료-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}.xlsx";
			Process.Start ( path );
		}

		private void ButtonManagementOpenPDFs_Click ( object sender, EventArgs e )
		{
			var date = monthCalendar.SelectionStart;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\{date.Month:00}{date.Day:00}.zip";
			if ( date.Date == DateTime.Today )
				if ( !File.Exists ( path ) )
				{
					MessageBox.Show ( "PDF 압축본이 아직 생성되지 않았습니다." );
					return;
				}
			Process.Start ( path );
		}

		private async void ButtonManagementArchiving_Click ( object sender, EventArgs e )
		{
			( sender as Button ).Enabled = false;

			var date = DateTime.Now;
			await Task.Run ( () =>
			{
				var archiveName = $"모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}";
				var dateDir = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\{archiveName}";
				var pdfZipPath = $@"{dateDir}\{date.Month:00}{date.Day:00}.zip";

				string [] pdfs = Directory.GetFiles ( CustomizedValue.WorkingDirectory, "*.pdf", SearchOption.TopDirectoryOnly );
				if ( pdfs == null || pdfs.Length < 10 )
				{
					if ( !File.Exists ( pdfZipPath ) )
					{
						MessageBox.Show ( "본 뜬 PDF 파일 개수가 10개 미만입니다." );
						return;
					}
				}
				else
				{
					ArchivingUtility.ArchivePdfs ( pdfZipPath, pdfs );
					FileDeleter.Delete ( pdfs );
				}

				ArchivingUtility.ArchiveDirectory ( Path.Combine ( CustomizedValue.WorkingDirectory, $"{archiveName}.zip" ), dateDir );
			} );

			MessageBox.Show ( "압축이 완료되었습니다." );

			( sender as Button ).Enabled = true;
		}

		private void TextBoxClickToSelectAll_MouseClick ( object sender, MouseEventArgs e )
		{
			( sender as TextBox ).SelectAll ();
		}

		private void ButtonRegisterStartProgram_Click ( object sender, EventArgs e )
		{
			StartupRegistry.Register ( false );
			buttonRegisterStartProgram.Enabled = false;
			buttonRegisterStartProgramWithStartupUtility.Enabled = false;
			buttonUnregisterStartProgram.Enabled = true;
		}

		private void ButtonRegisterStartProgramWithStartupUtility_Click ( object sender, EventArgs e )
		{
			StartupRegistry.Register ( true );
			buttonRegisterStartProgram.Enabled = buttonRegisterStartProgramWithStartupUtility.Enabled = false;
			buttonUnregisterStartProgram.Enabled = true;
		}

		private void ButtonUnregisterStartProgram_Click ( object sender, EventArgs e )
		{
			StartupRegistry.Unregister ();
			buttonRegisterStartProgram.Enabled = buttonRegisterStartProgramWithStartupUtility.Enabled = true;
			buttonUnregisterStartProgram.Enabled = false;
		}
	}
}
