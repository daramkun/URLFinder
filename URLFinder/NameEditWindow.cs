using Daramee.Winston.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URLFinder
{
	public partial class NameEditWindow : Form
	{
		OpenFolderDialog ofd = new OpenFolderDialog ();

		public NameEditWindow ()
		{
			InitializeComponent ();

			textBoxName.Text = CustomizedValue.WorkerName;
			textBoxWorkingPath.Text = CustomizedValue.WorkingDirectory;
			textBoxTemplatePath.Text = CustomizedValue.TemplateDirectory;
		}

		private void buttonOK_Click ( object sender, EventArgs e )
		{
			CustomizedValue.WorkerName = textBoxName.Text;
			CustomizedValue.WorkingDirectory = textBoxWorkingPath.Text;
			CustomizedValue.TemplateDirectory = textBoxTemplatePath.Text;
			DialogResult = DialogResult.OK;
		}

		private void textBoxWorkingPath_Click ( object sender, EventArgs e )
		{
			ofd.FileName = textBoxWorkingPath.Text;
			if ( ofd.ShowDialog () == false )
				return;
			textBoxWorkingPath.Text = ofd.FileName;
		}

		private void textBoxTemplatePath_Click ( object sender, EventArgs e )
		{
			ofd.FileName = textBoxTemplatePath.Text;
			if ( ofd.ShowDialog () == false )
				return;
			textBoxTemplatePath.Text = ofd.FileName;
		}
	}
}
