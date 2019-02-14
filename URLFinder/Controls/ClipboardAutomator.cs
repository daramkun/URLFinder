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

namespace URLFinder.Controls
{
	[DefaultEvent ( "ClipboardChanged" )]
	public partial class ClipboardAutomator : UserControl
	{
		[DllImport ( "User32.dll" )]
		protected static extern int SetClipboardViewer ( int hWndNewViewer );

		[DllImport ( "User32.dll", CharSet = CharSet.Auto )]
		public static extern bool ChangeClipboardChain ( IntPtr hWndRemove, IntPtr hWndNewNext );

		[DllImport ( "user32.dll", CharSet = CharSet.Auto )]
		public static extern int SendMessage ( IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam );
		
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

				Debug.WriteLine ( "checkBoxUseAutomation.Checked == true" );

				if ( !( e.DataObject.GetData ( "Text" ) is string text ) )
					return;

				Debug.WriteLine ( "e.DataObject.GetData ( \"Text\" ) as string != null" );

				if ( checkBoxLineMerger.Checked )
				{
					if ( text.IndexOf ( '\n' ) >= 0 )
					{
						e.Changed = text.Replace ( "\n", " " ).Replace ( "  ", " " );
						Debug.WriteLine ( "text is Multiline" );
					}
				}
				if ( checkBoxDateRearrange.Checked )
				{
					if ( !Regex.IsMatch ( text, "[1-2][0-9][0-9][0-9]-[0-1][0-9]-[0-3][0-9]" ) )
					{
						if ( DateTime.TryParse ( text, out DateTime result ) )
							e.Changed = result.ToString ( "yyyy-MM-dd" );
						else if ( Regex.IsMatch ( text, "[0-1][0-9]-[0-3][0-9]" ) )
							e.Changed = $"{DateTime.Today.Year}-{text}";

						if ( e.Changed != null )
							Debug.WriteLine ( "text is DateTime" );
					}
				}
			};
		}

		private void CheckBoxUseAutomation_CheckedChanged ( object sender, EventArgs e )
		{
			groupBoxOutline.Enabled = checkBoxUseAutomation.Checked;
		}

		protected override void WndProc ( ref System.Windows.Forms.Message m )
		{
			// defined in winuser.h
			const int WM_DRAWCLIPBOARD = 0x308;
			const int WM_CHANGECBCHAIN = 0x030D;

			switch ( m.Msg )
			{
				case WM_DRAWCLIPBOARD:
					OnClipboardChanged ();
					SendMessage ( nextClipboardViewer, m.Msg, m.WParam, m.LParam );
					break;

				case WM_CHANGECBCHAIN:
					if ( m.WParam == nextClipboardViewer )
						nextClipboardViewer = m.LParam;
					else
						SendMessage ( nextClipboardViewer, m.Msg, m.WParam, m.LParam );
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
