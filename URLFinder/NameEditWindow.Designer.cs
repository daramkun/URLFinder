namespace URLFinder
{
	partial class NameEditWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxWorkingPath = new System.Windows.Forms.TextBox();
			this.textBoxTemplatePath = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(63, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(33, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "이름:";
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(102, 22);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(198, 21);
			this.textBoxName.TabIndex = 1;
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(116, 131);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(93, 25);
			this.buttonOK.TabIndex = 2;
			this.buttonOK.Text = "확인";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(35, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "작업 경로:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(23, 93);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "템플릿 경로:";
			// 
			// textBoxWorkingPath
			// 
			this.textBoxWorkingPath.Location = new System.Drawing.Point(102, 56);
			this.textBoxWorkingPath.Name = "textBoxWorkingPath";
			this.textBoxWorkingPath.ReadOnly = true;
			this.textBoxWorkingPath.Size = new System.Drawing.Size(198, 21);
			this.textBoxWorkingPath.TabIndex = 5;
			this.textBoxWorkingPath.Click += new System.EventHandler(this.textBoxWorkingPath_Click);
			// 
			// textBoxTemplatePath
			// 
			this.textBoxTemplatePath.Location = new System.Drawing.Point(102, 90);
			this.textBoxTemplatePath.Name = "textBoxTemplatePath";
			this.textBoxTemplatePath.ReadOnly = true;
			this.textBoxTemplatePath.Size = new System.Drawing.Size(198, 21);
			this.textBoxTemplatePath.TabIndex = 6;
			this.textBoxTemplatePath.Click += new System.EventHandler(this.textBoxTemplatePath_Click);
			// 
			// NameEditWindow
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(326, 168);
			this.Controls.Add(this.textBoxTemplatePath);
			this.Controls.Add(this.textBoxWorkingPath);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NameEditWindow";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "이름 입력";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxWorkingPath;
		private System.Windows.Forms.TextBox textBoxTemplatePath;
	}
}