using Daramee.Winston.Dialogs;
using Daramee.Winston.File;
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
		public static MainWindow SharedWindow { get; private set; }

		readonly Thread uiThread;

		ExcelIndexer indexer;

		public MainWindow ()
		{
			SharedWindow = this;

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
							while ( builderState.OpeningFailedFiles.TryDequeue ( out string filename ) )
								Log ( $"[오류발생]{Path.GetFileName ( filename )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.", Color.LightSalmon, initializeItem );
						} ) );
						break;
					}

					if ( lastCount != builderState.TotalItemCount )
					{
						lastCount = builderState.TotalItemCount;
						Invoke ( new Action ( () =>
						{
							initializeItem.Text = $"[초기화중][검색자 초기화 수행 중][{lastCount}개 찾음][{builderState.ExcelFileCount}개 엑셀 파일 인덱싱 됨]";
						} ) );
					}

					while ( builderState.OpeningFailedFiles.TryDequeue ( out string file ) )
					{
						Invoke ( new Action ( () =>
						{
							Log ( $"[오류발생]{Path.GetFileName ( file )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.", Color.LightSalmon, initializeItem );
						} ) );
					}

					Thread.Sleep ( 300 );
				}
			} );
			counter.Start ();

			indexer = await ExcelIndexerBuilder.ToIndexer ( builderState );

			var sorted = from date in indexer.IndexedDates orderby date ascending select date;
			if ( sorted != null && sorted.Count () > 0 )
			{
				monthCalendar.MinDate = sorted.First ();
				monthCalendar.MaxDate = DateTime.Today;

				for ( DateTime d = sorted.First (); d < DateTime.Today; d = d.AddDays ( 1 ) )
				{
					if ( ( indexer.IndexedDates as List<DateTime> ).Contains ( d ) )
						monthCalendar.AddBoldedDate ( d );
				}
			}
			else monthCalendar.MinDate = monthCalendar.MaxDate = DateTime.Today;

			MonthCalendar_DateSelected ( monthCalendar, new DateRangeEventArgs ( DateTime.Today, DateTime.Today ) );

			tabControlKeywords.SelectedIndex = ( ( int ) DateTime.Today.DayOfWeek ) - 1;

			textBoxKeywordMonday.Text = CustomizedValue.MondayKeyword;
			textBoxKeywordTuesday.Text = CustomizedValue.TuesdayKeyword;
			textBoxKeywordWednesday.Text = CustomizedValue.WednesdayKeyword;
			textBoxKeywordThursday.Text = CustomizedValue.ThursdayKeyword;
			textBoxKeywordFriday.Text = CustomizedValue.FridayKeyword;
			textBoxKeywordAdditional.Text = CustomizedValue.AdditiveKeyword;

			tabControl.Enabled = true;
		}

		private void MainWindow_FormClosing ( object sender, FormClosingEventArgs e )
		{
			CustomizedValue.MondayKeyword = textBoxKeywordMonday.Text;
			CustomizedValue.TuesdayKeyword = textBoxKeywordTuesday.Text;
			CustomizedValue.WednesdayKeyword = textBoxKeywordWednesday.Text;
			CustomizedValue.ThursdayKeyword = textBoxKeywordThursday.Text;
			CustomizedValue.FridayKeyword = textBoxKeywordFriday.Text;
			CustomizedValue.AdditiveKeyword = textBoxKeywordAdditional.Text;

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
			buttonManagementArchiving.Enabled = e.Start.Date == DateTime.Today.Date;
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
					MessageBox.Show ( this, "PDF 압축본이 아직 생성되지 않았습니다." );
					return;
				}
			Process.Start ( path );
		}

		private async void ButtonManagementArchiving_Click ( object sender, EventArgs e )
		{
			( sender as Button ).Enabled = false;

			var date = DateTime.Now;
			var handle = Handle;

			await Task.Run ( () =>
			{
				var archiveName = $"모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}";
				var dateDir = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\{archiveName}";
				var pdfZipPath = $@"{dateDir}\{date.Month:00}{date.Day:00}.zip";

				string [] pdfs = Directory.GetFiles ( CustomizedValue.WorkingDirectory, "*.pdf", SearchOption.TopDirectoryOnly );
				if ( pdfs == null || pdfs.Length < 5 )
				{
					if ( !File.Exists ( pdfZipPath ) )
					{
						MessageBox.Show ( this, "본 뜬 PDF 파일 개수가 5개 미만입니다." );
						return;
					}
					else
					{
						ArchivingUtility.ArchiveDirectory ( Path.Combine ( CustomizedValue.WorkingDirectory, $"{archiveName}.zip" ), dateDir );
						Invoke ( new Action ( () => MessageBox.Show ( this, "압축이 완료되었습니다.", "PDF 압축" ) ) );
					}
				}
				else
				{
					CancellationTokenSource token = new CancellationTokenSource ();

					TaskDialog dialog = new TaskDialog ();
					TaskDialog nextDialog = new TaskDialog ();

					dialog.Title = "PDF 압축";
					dialog.MainInstruction = "PDF 파일을 ZIP 파일로 압축합니다.";
					dialog.Content = "Ghostscript를 이용해 PDF 파일의 크기를 줄인 후 ZIP 파일로 묶습니다.";
					dialog.CommonButtons = TaskDialogCommonButtonFlags.Cancel;
					dialog.ShowProgressBar = true;
					dialog.Footer = "준비 중.";
					dialog.ButtonClicked += ( sender2, e2 ) =>
					{
						token.Cancel ();
					};
					dialog.Created += async ( sender2, e2 ) =>
					{
						e2.SetProgressBarRange ( 0, ( ushort ) pdfs.Length );
						await Task.Run ( () =>
						{
							int proceed = 0;
							ArchivingUtility.ArchivePdfs ( pdfZipPath,
								( path ) =>
								{
									e2.SetElementText ( TaskDialogElement.Footer, $"처리 중: {path}" );
								},
								( path ) =>
								{
									e2.SetProgressBarPosition ( ++proceed );
								}, token.Token, pdfs );
							e2.SetElementText ( TaskDialogElement.Footer, "정리 중..." );

							if ( token.IsCancellationRequested )
								return;

							Operation.Begin ();
							foreach ( var pdf in pdfs )
							{
								if ( token.IsCancellationRequested )
									return;
								Operation.Delete ( pdf );
							}
							Operation.End ();

							if ( token.IsCancellationRequested )
								return;

							ArchivingUtility.ArchiveDirectory ( Path.Combine ( CustomizedValue.WorkingDirectory, $"{archiveName}.zip" ), dateDir );

							nextDialog.Title = "PDF 압축";
							nextDialog.MainInstruction = "압축이 완료되었습니다.";
							nextDialog.Content = "파일 압축이 완료되었습니다.";
							nextDialog.CommonButtons = TaskDialogCommonButtonFlags.OK;
							nextDialog.EnableHyperlinks = true;
							nextDialog.Footer = "<a>파일 위치로 이동</a>";
							nextDialog.HyperlinkClicked += ( sender3, e3 ) =>
							{
								Process.Start ( "explorer", CustomizedValue.WorkingDirectory );
							};
							e2.NavigatePage ( nextDialog );
						}, token.Token );
					};
					dialog.Show ( handle );
				}
			} );

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
