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
using System.Threading.Tasks;
using System.Windows.Forms;
using URLFinder.Properties;

namespace URLFinder
{
	public partial class MainForm : Form
	{
		public MainForm ()
		{
			InitializeComponent ();

			textBoxFindingPaths.Text = Settings.Default.finding_paths;
			textBoxFilePatterns.Text = Settings.Default.finding_file_patterns;
		}

		private void MainForm_FormClosed ( object sender, FormClosedEventArgs e )
		{
			Settings.Default.finding_paths = textBoxFindingPaths.Text;
			Settings.Default.finding_file_patterns = textBoxFilePatterns.Text;
			Settings.Default.Save ();
		}

		private void Log ( string message, Color backColor )
		{
			Invoke ( new Action ( () =>
			{
				listViewFind.Items.Add ( new ListViewItem ()
				{
					Text = message,
					BackColor = backColor,
				} );
				listViewFind.SelectedIndices.Clear ();
				listViewFind.SelectedIndices.Add ( listViewFind.Items.Count - 1 );
				listViewFind.Items [ listViewFind.Items.Count - 1 ].EnsureVisible ();
			} ) );
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

			string [] pathes = textBoxFindingPaths.Text.Split ( '|' );
			string [] patterns = textBoxFilePatterns.Text.Split ( '|' );

			Log ( $"[검색시작][{url}]", Color.Transparent );

			int totalSearched = 0;
			await Task.Run ( () =>
			{
				foreach ( string path in pathes )
					foreach ( string pattern in patterns )
						Parallel.ForEach ( Directory.GetFiles ( path, pattern, SearchOption.AllDirectories ), ( file ) =>
						{
							try
							{
								using ( OleDbConnection connection = new OleDbConnection (
									$"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"{file}\";Extended Properties=\"Excel 12.0;HDR=NO\""
								) )
								{
									connection.Open ();

									string tableName = Regex.Replace ( connection.GetSchema ( "Tables" ).Rows [ 1 ] [ "TABLE_NAME" ] as string, "['\"]", "" );
									using ( OleDbCommand command = new OleDbCommand (
										$"SELECT * FROM [{tableName}J2:J9999] WHERE F1 LIKE'%{url}%'",
										connection
									) )
									{
										using ( var reader = command.ExecuteReader () )
										{
											if ( reader.Read () )
											{
												Log ( $"[항목찾음]{Path.GetFileName ( file )}", Color.LightGreen );
											}
										}
									}

									connection.Close ();
								}
							}
							catch ( Exception ex )
							{
								Log ( $"[오류발생]{Path.GetFileName ( file )} - {ex.Message}", Color.LightSalmon );
							}
							finally
							{
								System.Threading.Interlocked.Increment ( ref totalSearched );
							}
						} );
			} );

			Log ( $"[검색종료][{url}][{totalSearched}개 파일에서 검색 완료]", Color.Transparent );

			textBoxFind.Enabled = true;
			textBoxFindingPaths.Enabled = true;
			textBoxFilePatterns.Enabled = true;
			buttonFind.Enabled = true;

			textBoxFind.SelectAll ();
			textBoxFind.Focus ();

			GC.Collect ();
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
				if ( Regex.IsMatch ( text, "https?://(.*)" ) || Regex.IsMatch ( text, "[0-9a-zA-Z](.[0-9a-zA-Z])+/?(.*)" ) )
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
