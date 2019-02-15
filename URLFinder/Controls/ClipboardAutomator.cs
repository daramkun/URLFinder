using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;
using URLFinder.Utilities;
using URLFinder.Processors;
using URLFinder.Finder;

namespace URLFinder.Controls
{
	[DefaultEvent ( "ClipboardChanged" )]
	public partial class ClipboardAutomator : UserControl
	{
		[DllImport ( "user32.dll" )]
		protected static extern int SetClipboardViewer ( int hWndNewViewer );
		[DllImport ( "user32.dll" )]
		public static extern bool ChangeClipboardChain ( IntPtr hWndRemove, IntPtr hWndNewNext );
		[DllImport ( "user32.dll" )]
		public static extern int SendMessage ( IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam );
		[DllImport ( "user32.dll" )]
		private static extern bool RegisterHotKey ( IntPtr hWnd, int id, uint fsModifiers, uint vk );
		[DllImport ( "user32.dll" )]
		private static extern bool UnregisterHotKey ( IntPtr hWnd, int id );

		IntPtr nextClipboardViewer;

		public event EventHandler<ClipboardChangedEventArgs> ClipboardChanged;

		public bool UseClipboardAutomation
		{
			get { return checkBoxUseAutomation.Checked; }
			set { checkBoxUseAutomation.Checked = value; }
		}

		public ClipboardAutomator ()
		{
			InitializeComponent ();

			nextClipboardViewer = ( IntPtr ) SetClipboardViewer ( ( int ) this.Handle );

			ClipboardChanged += ( sender, e ) =>
			{
				Debug.WriteLine ( "ClipboardChanged ()" );

				if ( !checkBoxUseAutomation.Checked )
					return;

				if ( !( e.DataObject.GetData ( "Text" ) is string text ) )
					return;

				text = text.Trim ();

				text = Regex.Replace ( text, "(\\[출처\\] .* \\(.*\\) \\|작성자 .*)$", "" );
				text = Regex.Replace ( text, "(\\[출처\\] .*\n\\[링크\\] .*)$", "" );

				if ( checkBoxLineMerger.Checked )
				{
					if ( text.IndexOf ( '\n' ) >= 0 )
					{
						e.Changed = Regex.Replace ( Regex.Replace ( text, "[\r\n]+", " " ), "( [ ]+)", "" );
						FinderLog.Log ( "클립보드 내용이 여러 줄로 구성되어 병합함" );
					}
				}
				if ( checkBoxDateRearrange.Checked )
				{
					if ( !Regex.IsMatch ( text.Trim (), "^[1-2][09][0-9][0-9]-[0-1][0-9]-[0-3][0-9]$" ) )
					{
						if ( DateTime.TryParse ( text, out DateTime result ) )
						{
							e.Changed = result.ToString ( "yyyy-MM-dd" );
							FinderLog.Log ( "클립보드 내용이 날짜여서 yyyy-MM-dd로 재구성함" );
						}
						else if ( Regex.IsMatch ( text, "[0-1][0-9]-[0-3][0-9]" ) )
						{
							e.Changed = $"{DateTime.Today.Year}-{text}";
							FinderLog.Log ( "클립보드 내용이 날짜여서 yyyy-MM-dd로 재구성함" );
						}
					}
					else
						FinderLog.Log ( "클립보드 내용이 날짜이지만 이미 yyyy-MM-dd임" );
				}
				if ( checkBoxFixURL.Checked && ExcelIndexer.SharedExcelIndexer != null )
				{
					if ( Regex.IsMatch ( text, "^https?://[a-zA-Z0-9가-힣./?&=%#_:\\-\\\\]+$" ) )
					{
						BaseProcessor processor = ProcessorFinder.FindProcessor ( text );
						e.Changed = processor.ConvertUrl ( text );
						if ( e.Changed != text )
							FinderLog.Log ( "클립보드 내용이 URL로 확인되어 교정함" );
						else
						{
							e.Changed = null;
							FinderLog.Log ( "클립보드 내용이 URL이지만 교정할 필요가 없음" );
						}
					}
				}
			};

			RegisterHotKey ( Handle, 0, 1, ( int ) Keys.C );
		}

		~ClipboardAutomator ()
		{
			UnregisterHotKey ( Handle, 0 );
		}

		private void CheckBoxUseAutomation_CheckedChanged ( object sender, EventArgs e )
		{
			groupBoxOutline.Enabled = checkBoxUseAutomation.Checked;
		}

		protected override void WndProc ( ref System.Windows.Forms.Message m )
		{
			const int WM_DRAWCLIPBOARD = 0x308;
			const int WM_CHANGECBCHAIN = 0x030D;
			const int WM_HOTKEY = 0x0312;

			switch ( m.Msg )
			{
				case WM_DRAWCLIPBOARD:
					{
						OnClipboardChanged ();
						SendMessage ( nextClipboardViewer, m.Msg, m.WParam, m.LParam );
					}
					break;

				case WM_CHANGECBCHAIN:
					{
						if ( m.WParam == nextClipboardViewer )
							nextClipboardViewer = m.LParam;
						else
							SendMessage ( nextClipboardViewer, m.Msg, m.WParam, m.LParam );
					}
					break;

				case WM_HOTKEY:
					{
						Keys key = ( Keys ) ( ( ( int ) m.LParam >> 16 ) & 0xFFFF );
						int modifier = ( int ) m.LParam & 0xFFFF;

						if ( modifier == 1 && key == Keys.C )
							checkBoxUseAutomation.Checked = !checkBoxUseAutomation.Checked;
					}
					break;

				default:
					base.WndProc ( ref m );
					break;
			}
		}

		protected virtual void OnClipboardChanged ()
		{
			try
			{
				IDataObject iData = Clipboard.GetDataObject ();
				var e = new ClipboardChangedEventArgs ( iData );
				ClipboardChanged?.Invoke ( this, e );

				if ( e.Changed != null )
					Clipboard.SetText ( e.Changed );
			}
			catch ( Exception e )
			{
				Debug.WriteLine ( e );
			}
		}
	}

	public class ClipboardChangedEventArgs : EventArgs
	{
		public readonly IDataObject DataObject;
		public string Changed;

		public ClipboardChangedEventArgs ( IDataObject dataObject )
		{
			DataObject = dataObject;
			Changed = null;
		}
	}
}
