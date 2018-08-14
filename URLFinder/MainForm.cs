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
using URLFinder.Processors;
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

		private TreeNode Log ( string message, Color backColor, TreeNode mainItem = null )
		{
			var item = new TreeNode ()
			{
				Text = message,
				BackColor = backColor,
			};
			
			Invoke ( new Action ( () =>
			{
				if ( mainItem != null )
				{
					mainItem.Nodes.Add ( item );
					mainItem.Expand ();
				}
				else
					treeViewFind.Nodes.Add ( item );
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
			
			BaseProcessor processor = ProcessorFinder.FindProcessor ( textBoxFind.Text );
			textBoxFind.Text = processor.ConvertUrl ( textBoxFind.Text );
			textBoxManagedSite.Text = processor.BaseUrl.AbsoluteUri;

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
				var initial = Log ( "[초기화중][검색자 초기화 수행 중]", Color.Transparent );
				await Task.Run ( () =>
				{
					finder = new ExcelFinder ( textBoxFindingPaths.Text, textBoxFilePatterns.Text );
					foreach ( var file in finder.CannotOpenedFiles )
						Log ( $"[오류발생]{Path.GetFileName ( file )} - 파일에 접근할 수 없거나 잘못된 엑셀 파일임.", Color.LightSalmon, initial );
				} );
				Log ( $"[작업완료][총 {finder.Count}개 엑셀 파일에서 검색 가능한 상태]", Color.Transparent, initial );
			}

			var countItem = Log ( $"[검색시작][{url}][0/{finder.Count}]", Color.Transparent );

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
			var urlTitle = textBoxURLTitle.Text.Split ( new string [] { "\r\n", "\n" }, StringSplitOptions.None );
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
					BaseProcessor processor = ProcessorFinder.FindProcessor ( text );

					urlList.Append ( text ).Append ( Environment.NewLine );
					var managedUrl = processor.GetDetailBaseUrl ( text ).AbsoluteUri;
					managedUrlList.Append ( managedUrl ).Append ( Environment.NewLine );
					webSiteList.Append ( processor.GetDetailWebSiteName ( text ) ).Append ( Environment.NewLine );
				}
				else
					titleList.Append ( text ).Append ( Environment.NewLine );
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
