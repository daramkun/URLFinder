using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URLFinder.Controls
{
	public class PasteDetectableTextBox : TextBox
	{
		public event EventHandler Pasted;

		private const int WM_PASTE = 0x0302;
		protected override void WndProc ( ref Message m )
		{
			if ( m.Msg == WM_PASTE )
			{
				Pasted?.Invoke ( this, EventArgs.Empty );
				return;
			}

			base.WndProc ( ref m );
		}
	}
}
