using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using URLFinder.Finder;
using URLFinder.Processors;
using URLFinder.Utilities;

namespace URLFinder
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		Thread uiThread;
		ExcelIndexer indexer;

		public event PropertyChangedEventHandler PropertyChanged;

		public ExcelIndexer ExcelIndexer => indexer;

		public MainWindow ()
		{
			uiThread = Thread.CurrentThread;
			InitializeComponent ();

			calendarManagement.DisplayDateEnd = DateTime.Today;
		}

		private TreeViewItem Log ( string v, Color color, TreeViewItem parentItem = null )
		{
			if ( Thread.CurrentThread != uiThread )
			{
				TreeViewItem ret = null;
				Dispatcher.BeginInvoke ( new Action ( () =>
				{
					ret = __Log ( v, color, parentItem );
				} ) ).Wait ();
				return ret;
			}
			else return __Log ( v, color, parentItem );
		}
		private TreeViewItem __Log ( string v, Color color, TreeViewItem parentItem )
		{
			var item = new TreeViewItem ()
			{
				Header = v,
				Background = new SolidColorBrush ( color ),
			};
			if ( parentItem == null )
				treeViewLog.Items.Add ( item );
			else
			{
				parentItem.Items.Add ( item );
				parentItem.ExpandSubtree ();
			}
			return item;
		}

		private string DeleteSlash ( string v )
		{
			if ( v [ v.Length - 1 ] == '/' )
				return v.Substring ( 0, v.Length - 1 );
			return v;
		}

		private bool FixUrl ()
		{
			if ( string.IsNullOrEmpty ( textBoxSearchText.Text ) )
			{
				Log ( "[오류발생]URL을 입력해주세요.", Colors.LightSalmon );
				return false;
			}
			if ( !Regex.IsMatch ( textBoxSearchText.Text, "([a-zA-Z0-9]+://)?([a-zA-Z0-9\\.]+)\\.[a-zA-Z0-9]+(\\/[a-zA-Z0-9가-힣_\\-&%+?/.=]*)?" ) )
			{
				Log ( "[오류발생]URL 형식으로 입력해주세요.", Colors.LightSalmon );
				return false;
			}

			BaseProcessor processor = ProcessorFinder.FindProcessor ( textBoxSearchText.Text, false );
			if ( processor != null )
				textBoxSearchText.Text = processor.ConvertUrl ( textBoxSearchText.Text );

			return true;
		}

		private async void MainWindow_Loaded ( object sender, RoutedEventArgs e )
		{
			ExcelIndexerBuilderState builderState = new ExcelIndexerBuilderState ();

			TreeViewItem initializeItem = Log ( "[초기화중][검색자 초기화 수행 중][0개 찾음][0개 엑셀 파일 인덱싱 됨]", Colors.Transparent );

			Thread counter = new Thread ( () =>
			{
				int lastCount = -1;
				while ( true )
				{
					if ( indexer != null )
					{
						Dispatcher.BeginInvoke ( new Action ( () =>
						{
							initializeItem.Header = $"[초기화중][검색자 초기화 수행 중][{builderState.TotalItemCount}개 찾음][{builderState.ExcelFileCount}개 엑셀 파일 인덱싱 됨]";
							while ( builderState.OpeningFailedFiles.TryDequeue ( out string file ) )
								Log ( $"[오류발생]{System.IO.Path.GetFileName ( file )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.", Colors.LightSalmon, initializeItem );
						} ) ).Wait ();
						break;
					}

					Dispatcher.BeginInvoke ( new Action ( () =>
					{
						if ( lastCount != builderState.TotalItemCount )
						{
							lastCount = builderState.TotalItemCount;
							initializeItem.Header = $"[초기화중][검색자 초기화 수행 중][{lastCount}개 찾음][{builderState.ExcelFileCount}개 엑셀 파일 인덱싱 됨]";
						}

						if ( builderState.OpeningFailedFiles.TryDequeue ( out string file ) )
						{
							initializeItem.Items.Add ( new TreeViewItem ()
							{
								Header = $"[오류발생]{System.IO.Path.GetFileName ( file )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.",
								Background = new SolidColorBrush ( Colors.LightSalmon ),
							} );
						}
					} ) ).Wait ();
					
					Thread.Sleep ( 100 );
				}
			} );
			counter.Start ();

			indexer = await ExcelIndexerBuilder.ToIndexer ( builderState );

			var sorted = from date in indexer.IndexedDates orderby date ascending select date;
			calendarManagement.DisplayDateStart = sorted.First ();

			for ( DateTime d = sorted.First (); d < DateTime.Today; d = d.AddDays ( 1 ) )
			{
				if ( !( indexer.IndexedDates as List<DateTime> ).Contains ( d ) )
					calendarManagement.BlackoutDates.Add ( new CalendarDateRange ( d ) );
			}

			calendarManagement.SelectedDate = DateTime.Today;

			PropertyChanged?.Invoke ( this, new PropertyChangedEventArgs ( nameof ( ExcelIndexer ) ) );
		}

		private void MainWindow_Closing ( object sender, CancelEventArgs e )
		{
			Properties.Settings.Default.Save ();
		}

		private async void URLSearchButton_Click ( object sender, RoutedEventArgs e )
		{
			if ( FixUrl () == false )
				return;

			string url = textBoxSearchText.Text;
			url = Regex.Replace ( url, "https?://", "" );

			textBoxSearchText.IsEnabled = false;
			buttonSearch.IsEnabled = false;

			ExcelIndexerFindState findState = new ExcelIndexerFindState ();
			var countItem = Log ( $"[검색시작][{url}][0/{indexer.Count}]", Colors.Transparent );

			Thread counter = new Thread ( () =>
			{
				int recent = 0;
				while ( true )
				{
					Dispatcher.BeginInvoke ( new Action ( () =>
					{
						if ( indexer == null )
							return;
						if ( recent == findState.Proceed )
							return;
						countItem.Header = $"[검색시작][{url}][{findState.Proceed}/{indexer.Count}]";
						recent = findState.Proceed;
					} ) ).Wait ();

					if ( recent == indexer.Count )
						break;

					Thread.Sleep ( 50 );
				}
			} );
			counter.Start ();

			await Task.Run ( () =>
			{
				foreach ( var file in indexer.Find ( url, findState ) )
					Log ( $"[항목찾음]{System.IO.Path.GetFileName ( file )}", Colors.LightGreen, countItem );
			} );

			counter.Join ( 100 );

			buttonSearch.IsEnabled = true;
			textBoxSearchText.IsEnabled = true;
		}

		private void TextMergeAndCutButton_Click ( object sender, RoutedEventArgs e )
		{
			string text = Regex.Replace ( Regex.Replace ( textBoxMultilineMergerText.Text, "[\r\n]+", " " ), "( [ ]+)", "" );
			Clipboard.SetText ( text );
			textBoxMultilineMergerText.Text = "";
		}

		private void UrlAndTitleSplitterButton_Click ( object sender, RoutedEventArgs e )
		{
			var urlTitle = textBoxUrlAndTitleText.Text.Split ( new string [] { "\r\n", "\n" }, StringSplitOptions.None );
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

			textBoxUrlAndTitleUrlText.Text = urlList.ToString ();
			textBoxUrlAndTitleTitleText.Text = titleList.ToString ();
			textBoxUrlAndTitleWebsiteText.Text = webSiteList.ToString ();
			textBoxUrlAndTitleBaseUrlText.Text = managedUrlList.ToString ();
		}

		private void UrlAndTitleResultTextBox_GotFocus ( object sender, RoutedEventArgs e )
		{
			( sender as TextBox ).SelectAll ();
		}

		private void UrlAndTitleResultTextBox_SelectionChanged ( object sender, RoutedEventArgs e )
		{
			if ( ( sender as TextBox ).SelectionLength == ( sender as TextBox ).Text.Length )
				return;
			( sender as TextBox ).SelectAll ();
		}

		private void OpenSelectedDateFolderButton_Click ( object sender, RoutedEventArgs e )
		{
			var date = calendarManagement.SelectedDate.Value;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\";
			Process.Start ( "explorer", path );
		}

		private void OpenSelectedDateHWPButton_Click ( object sender, RoutedEventArgs e )
		{
			var date = calendarManagement.SelectedDate.Value;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\모니터링 일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}.hwp";
			Process.Start ( path );
		}

		private void OpenSelectedDateXLSXButton_Click ( object sender, RoutedEventArgs e )
		{
			var date = calendarManagement.SelectedDate.Value;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\엑셀자료-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}.xlsx";
			Process.Start ( path );
		}

		private void OpenSelectedDatePDFButton_Click ( object sender, RoutedEventArgs e )
		{
			var date = calendarManagement.SelectedDate.Value;
			var path = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}\{date.Month:00}{date.Day:00}.zip";
			if ( date.Date == DateTime.Today )
				if ( !System.IO.File.Exists ( path ) )
				{
					MessageBox.Show ( "PDF 압축본이 아직 생성되지 않았습니다." );
					return;
				}
			Process.Start ( path );
		}

		private async void CompressTodaysFilesButton_Click ( object sender, RoutedEventArgs e )
		{
			( sender as Button ).IsEnabled = false;

			var date = calendarManagement.SelectedDate.Value;
			await Task.Run ( () =>
			{
				var archiveName = $"모니터링일지-{CustomizedValue.WorkerName}-{date.ToString ( "yyMMdd" )}";
				var dateDir = $@"{CustomizedValue.WorkingDirectory}\{date.Year}\{date.Month:00}\{date.GetWeekOfMonth ()}주\{archiveName}";
				var pdfZipPath = $@"{dateDir}\{date.Month:00}{date.Day:00}.zip";

				string [] pdfs = Directory.GetFiles ( CustomizedValue.WorkingDirectory, "*.pdf", SearchOption.TopDirectoryOnly );
				if ( pdfs == null || pdfs.Length < 10 )
				{
					MessageBox.Show ( "본 뜬 PDF 파일 개수가 10개 미만입니다." );
					return;
				}

				ArchivingUtility.ArchivePdfs ( pdfZipPath, pdfs );
				FileDeleter.Delete ( pdfs );

				ArchivingUtility.ArchiveDirectory ( System.IO.Path.Combine ( CustomizedValue.WorkingDirectory, $"{archiveName}.zip" ), dateDir );
			} );

			MessageBox.Show ( "압축이 완료되었습니다." );

			( sender as Button ).IsEnabled = true;
		}

		private void textBoxSearchText_PreviewMouseLeftButtonDown ( object sender, MouseButtonEventArgs e )
		{
			TextBox tb = ( sender as TextBox );
			if ( tb != null )
			{
				if ( !tb.IsKeyboardFocusWithin )
				{
					e.Handled = true;
					tb.Focus ();
				}
			}
		}

		private void textBoxSearchText_GotKeyboardFocus ( object sender, KeyboardFocusChangedEventArgs e )
		{
			TextBox tb = ( sender as TextBox );
			if ( tb != null )
			{
				tb.SelectAll ();
			}
		}
	}
}
