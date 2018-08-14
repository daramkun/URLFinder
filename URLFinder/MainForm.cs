using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using URLFinder.Finders;
using URLFinder.Properties;

namespace URLFinder
{
	public partial class MainForm : Form
	{
		ExcelFinder finder;

		public MainForm ()
		{
			InitializeComponent ();

			textBoxFindingPaths.Text = Settings.Default.finding_paths;
			textBoxFilePatterns.Text = Settings.Default.finding_file_patterns;
		}

		private void MainForm_FormClosed ( object sender, FormClosedEventArgs e )
		{
			if ( finder != null )
			{
				finder.Dispose ();
				finder = null;
			}

			Settings.Default.finding_paths = textBoxFindingPaths.Text;
			Settings.Default.finding_file_patterns = textBoxFilePatterns.Text;
			Settings.Default.Save ();
		}

		private ListViewItem Log ( string message, Color backColor, ListViewItem mainItem = null )
		{
			var item = mainItem == null ? new ListViewItem ()
			{
				Text = message,
				BackColor = backColor,
			} : null;
			
			Invoke ( new Action ( () =>
			{
				if ( mainItem != null )
				{
					mainItem.SubItems.Add ( message, Color.Black, backColor, DefaultFont );
				}
				else
				{
					listViewFind.Items.Add ( item );
					listViewFind.SelectedIndices.Clear ();
					listViewFind.SelectedIndices.Add ( listViewFind.Items.Count - 1 );
					listViewFind.Items [ listViewFind.Items.Count - 1 ].EnsureVisible ();
				}
			} ) );

			return item;
		}

		private bool FixUrl ()
		{
			if ( string.IsNullOrEmpty ( textBoxFind.Text ) )
			{
				Log ( "[오류발생]URL을 입력해주세요.", Color.LightSalmon );
				return false;
			}
			if ( !Regex.IsMatch ( textBoxFind.Text, "([a-zA-Z0-9]+://)?([a-zA-Z0-9\\.]+)\\.[a-zA-Z0-9]+(\\/[a-zA-Z0-9가-힣_\\-&%+?/.=]*)?" ) )
			{
				Log ( "[오류발생]URL 형식으로 입력해주세요.", Color.LightSalmon );
				return false;
			}

			textBoxFind.Text = URLUtility.Compress ( textBoxFind.Text );
			textBoxManagedSite.Text = URLUtility.GetManagedSiteUrl ( textBoxFind.Text );

			return true;
		}

		private async void ButtonFind_Click ( object sender, EventArgs e )
		{
			if ( FixUrl () == false )
				return;

			string url = textBoxFind.Text;
			url = Regex.Replace ( url, "https?://", "" );

			textBoxFind.Enabled = false;
			textBoxFindingPaths.Enabled = false;
			textBoxFilePatterns.Enabled = false;
			buttonFind.Enabled = false;

			if ( finder == null )
			{
				Log ( "[초기화중][검색자 초기화 수행 중]", Color.White );
				await Task.Run ( () =>
				{
					finder = new ExcelFinder ( textBoxFindingPaths.Text, textBoxFilePatterns.Text );
					foreach ( var file in finder.CannotOpenedFiles )
						Log ( $"[오류발생]{Path.GetFileName ( file )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.", Color.LightSalmon );
				} );
			}

			ListViewItem countItem = Log ( $"[검색시작][{url}][0/{finder.Count}]", Color.Transparent );

			finder.Clear ();
			
			Thread counter = new Thread ( () =>
			{
				int recent = 0;
				while ( true )
				{
					Invoke ( new Action ( () =>
					{
						if ( recent == finder.Procceed )
							return;
						countItem.Text = $"[검색시작][{url}][{finder.Procceed}/{finder.Count}]";
						recent = finder.Procceed;
					} ) );

					if ( recent == finder.Count )
						break;

					Thread.Sleep ( 50 );
				}
			} );
			counter.Start ();
			
			await Task.Run ( () =>
			{
				foreach ( var file in finder.Find ( url ) )
					Log ( $"[항목찾음]{Path.GetFileName ( file )}", Color.LightGreen, countItem );
			} );

			Log ( $"[검색종료][{url}]", Color.Transparent );

			counter.Join ( 100 );

			textBoxFind.Enabled = true;
			textBoxFindingPaths.Enabled = true;
			textBoxFilePatterns.Enabled = true;
			buttonFind.Enabled = true;

			textBoxFind.SelectAll ();
			textBoxFind.Focus ();

			GC.Collect ();
		}

		private void TextBoxFindingPaths_TextChanged ( object sender, EventArgs e )
		{
			if ( finder != null )
				finder.Dispose ();
			finder = null;
		}

		private void TextBoxFilePatterns_TextChanged ( object sender, EventArgs e )
		{
			if ( finder != null )
				finder.Dispose ();
			finder = null;
		}

		private void ButtonFixUrl_Click ( object sender, EventArgs e )
		{
			FixUrl ();
		}

		private void ButtonMergeLine_Click ( object sender, EventArgs e )
		{
			string text = Regex.Replace ( Regex.Replace ( textBoxMultiline.Text, "[\r\n]+", " " ), "( [ ]+)", "" );
			Clipboard.SetText ( text );
			textBoxMultiline.Text = "";
		}

		private void ButtonSplitURLTitle_Click ( object sender, EventArgs e )
		{
			var urlTitle = textBoxURLTitle.Text.Split ( '\n' );
			StringBuilder urlList = new StringBuilder ();
			StringBuilder titleList = new StringBuilder ();
			StringBuilder webSiteList = new StringBuilder ();
			StringBuilder managedUrlList = new StringBuilder ();

			foreach ( string text in urlTitle )
			{
				if ( string.IsNullOrEmpty ( text.Trim () ) )
					continue;
				if ( Regex.IsMatch ( text, "https?://(.*)" )/* || Regex.IsMatch ( text, "[0-9a-zA-Z](.[0-9a-zA-Z])+/?(.*)" )*/ )
				{
					urlList.Append ( text ).Append ( "\r\n" );
					var managedUrl = URLUtility.GetManagedSiteUrl ( text );
					managedUrlList.Append ( managedUrl ).Append ( "\r\n" );
					webSiteList.Append ( URLUtility.GetWebSiteName ( managedUrl ) ).Append ( "\r\n" );
				}
				else
					titleList.Append ( text ).Append ( "\r\n" );
			}

			textBoxURL.Text = urlList.ToString ();
			textBoxTitle.Text = titleList.ToString ();
			textBoxWebSite.Text = webSiteList.ToString ();
			textBoxManagedUrl.Text = managedUrlList.ToString ();
		}

		private void TextBoxURL_Click ( object sender, EventArgs e )
		{
			textBoxURL.SelectAll ();
		}

		private void TextBoxTitle_Click ( object sender, EventArgs e )
		{
			textBoxTitle.SelectAll ();
		}

		private void TextBoxWebSite_Click ( object sender, EventArgs e )
		{
			textBoxWebSite.SelectAll ();
		}

		private void TextBoxManagedUrl_Click ( object sender, EventArgs e )
		{
			textBoxManagedUrl.SelectAll ();
		}
	}
}
