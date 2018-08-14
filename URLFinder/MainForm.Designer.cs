namespace URLFinder
{
	partial class MainForm
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent ()
		{
			System.Windows.Forms.TabControl tabControl1;
			System.Windows.Forms.Label labelMainSite;
			System.Windows.Forms.Label labelSearchPattern;
			System.Windows.Forms.Label labelSearchPathes;
			System.Windows.Forms.Label labelFindingURL;
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.textBoxFilePatterns = new System.Windows.Forms.TextBox();
			this.textBoxFindingPaths = new System.Windows.Forms.TextBox();
			this.listViewFind = new System.Windows.Forms.ListView();
			this.columnHeaderLog = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.buttonFind = new System.Windows.Forms.Button();
			this.textBoxManagedSite = new System.Windows.Forms.TextBox();
			this.buttonFixUrl = new System.Windows.Forms.Button();
			this.textBoxFind = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.buttonMergeLine = new System.Windows.Forms.Button();
			this.textBoxMultiline = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxURLTitle = new System.Windows.Forms.TextBox();
			this.textBoxURL = new System.Windows.Forms.TextBox();
			this.textBoxTitle = new System.Windows.Forms.TextBox();
			this.buttonSplitURLTitle = new System.Windows.Forms.Button();
			this.textBoxWebSite = new System.Windows.Forms.TextBox();
			this.textBoxManagedUrl = new System.Windows.Forms.TextBox();
			tabControl1 = new System.Windows.Forms.TabControl();
			labelMainSite = new System.Windows.Forms.Label();
			labelSearchPattern = new System.Windows.Forms.Label();
			labelSearchPathes = new System.Windows.Forms.Label();
			labelFindingURL = new System.Windows.Forms.Label();
			tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			tabControl1.Controls.Add(this.tabPage1);
			tabControl1.Controls.Add(this.tabPage2);
			tabControl1.Controls.Add(this.tabPage3);
			tabControl1.Location = new System.Drawing.Point(5, 5);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new System.Drawing.Size(704, 605);
			tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(labelMainSite);
			this.tabPage1.Controls.Add(this.textBoxFilePatterns);
			this.tabPage1.Controls.Add(labelSearchPattern);
			this.tabPage1.Controls.Add(this.textBoxFindingPaths);
			this.tabPage1.Controls.Add(labelSearchPathes);
			this.tabPage1.Controls.Add(this.listViewFind);
			this.tabPage1.Controls.Add(this.buttonFind);
			this.tabPage1.Controls.Add(this.textBoxManagedSite);
			this.tabPage1.Controls.Add(this.buttonFixUrl);
			this.tabPage1.Controls.Add(this.textBoxFind);
			this.tabPage1.Controls.Add(labelFindingURL);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(696, 579);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "URL검색";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// labelMainSite
			// 
			labelMainSite.AutoSize = true;
			labelMainSite.Location = new System.Drawing.Point(11, 44);
			labelMainSite.Name = "labelMainSite";
			labelMainSite.Size = new System.Drawing.Size(69, 12);
			labelMainSite.TabIndex = 27;
			labelMainSite.Text = "관리 사이트";
			// 
			// textBoxFilePatterns
			// 
			this.textBoxFilePatterns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFilePatterns.Location = new System.Drawing.Point(74, 548);
			this.textBoxFilePatterns.Name = "textBoxFilePatterns";
			this.textBoxFilePatterns.Size = new System.Drawing.Size(612, 21);
			this.textBoxFilePatterns.TabIndex = 7;
			this.textBoxFilePatterns.TextChanged += new System.EventHandler(this.TextBoxFilePatterns_TextChanged);
			// 
			// labelSearchPattern
			// 
			labelSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			labelSearchPattern.AutoSize = true;
			labelSearchPattern.Location = new System.Drawing.Point(11, 551);
			labelSearchPattern.Name = "labelSearchPattern";
			labelSearchPattern.Size = new System.Drawing.Size(57, 12);
			labelSearchPattern.TabIndex = 24;
			labelSearchPattern.Text = "검색 패턴";
			// 
			// textBoxFindingPaths
			// 
			this.textBoxFindingPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFindingPaths.Location = new System.Drawing.Point(74, 516);
			this.textBoxFindingPaths.Name = "textBoxFindingPaths";
			this.textBoxFindingPaths.Size = new System.Drawing.Size(612, 21);
			this.textBoxFindingPaths.TabIndex = 6;
			this.textBoxFindingPaths.TextChanged += new System.EventHandler(this.TextBoxFindingPaths_TextChanged);
			// 
			// labelSearchPathes
			// 
			labelSearchPathes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			labelSearchPathes.AutoSize = true;
			labelSearchPathes.Location = new System.Drawing.Point(11, 519);
			labelSearchPathes.Name = "labelSearchPathes";
			labelSearchPathes.Size = new System.Drawing.Size(57, 12);
			labelSearchPathes.TabIndex = 22;
			labelSearchPathes.Text = "검색 경로";
			// 
			// listViewFind
			// 
			this.listViewFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listViewFind.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLog});
			this.listViewFind.LabelWrap = false;
			this.listViewFind.Location = new System.Drawing.Point(13, 100);
			this.listViewFind.MultiSelect = false;
			this.listViewFind.Name = "listViewFind";
			this.listViewFind.Size = new System.Drawing.Size(673, 401);
			this.listViewFind.TabIndex = 5;
			this.listViewFind.UseCompatibleStateImageBehavior = false;
			this.listViewFind.View = System.Windows.Forms.View.Details;
			// 
			// columnHeaderLog
			// 
			this.columnHeaderLog.Text = "로그";
			this.columnHeaderLog.Width = 560;
			// 
			// buttonFind
			// 
			this.buttonFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFind.Location = new System.Drawing.Point(13, 68);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.Size = new System.Drawing.Size(561, 23);
			this.buttonFind.TabIndex = 3;
			this.buttonFind.Text = "찾기";
			this.buttonFind.UseVisualStyleBackColor = true;
			this.buttonFind.Click += new System.EventHandler(this.ButtonFind_Click);
			// 
			// textBoxManagedSite
			// 
			this.textBoxManagedSite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxManagedSite.Location = new System.Drawing.Point(85, 41);
			this.textBoxManagedSite.Name = "textBoxManagedSite";
			this.textBoxManagedSite.ReadOnly = true;
			this.textBoxManagedSite.Size = new System.Drawing.Size(601, 21);
			this.textBoxManagedSite.TabIndex = 2;
			// 
			// buttonFixUrl
			// 
			this.buttonFixUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFixUrl.Location = new System.Drawing.Point(580, 68);
			this.buttonFixUrl.Name = "buttonFixUrl";
			this.buttonFixUrl.Size = new System.Drawing.Size(106, 23);
			this.buttonFixUrl.TabIndex = 4;
			this.buttonFixUrl.Text = "URL 단순 교정";
			this.buttonFixUrl.UseVisualStyleBackColor = true;
			this.buttonFixUrl.Click += new System.EventHandler(this.ButtonFixUrl_Click);
			// 
			// textBoxFind
			// 
			this.textBoxFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFind.Location = new System.Drawing.Point(85, 12);
			this.textBoxFind.Name = "textBoxFind";
			this.textBoxFind.Size = new System.Drawing.Size(601, 21);
			this.textBoxFind.TabIndex = 1;
			// 
			// labelFindingURL
			// 
			labelFindingURL.AutoSize = true;
			labelFindingURL.Location = new System.Drawing.Point(11, 15);
			labelFindingURL.Name = "labelFindingURL";
			labelFindingURL.Size = new System.Drawing.Size(68, 12);
			labelFindingURL.TabIndex = 18;
			labelFindingURL.Text = "검색할 URL";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.buttonMergeLine);
			this.tabPage2.Controls.Add(this.textBoxMultiline);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(696, 579);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "줄 병합";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// buttonMergeLine
			// 
			this.buttonMergeLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonMergeLine.Location = new System.Drawing.Point(6, 550);
			this.buttonMergeLine.Name = "buttonMergeLine";
			this.buttonMergeLine.Size = new System.Drawing.Size(684, 23);
			this.buttonMergeLine.TabIndex = 2;
			this.buttonMergeLine.Text = "한 줄로 병합된 텍스트 복사";
			this.buttonMergeLine.UseVisualStyleBackColor = true;
			this.buttonMergeLine.Click += new System.EventHandler(this.ButtonMergeLine_Click);
			// 
			// textBoxMultiline
			// 
			this.textBoxMultiline.AcceptsReturn = true;
			this.textBoxMultiline.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxMultiline.Location = new System.Drawing.Point(6, 6);
			this.textBoxMultiline.Multiline = true;
			this.textBoxMultiline.Name = "textBoxMultiline";
			this.textBoxMultiline.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMultiline.Size = new System.Drawing.Size(684, 538);
			this.textBoxMultiline.TabIndex = 1;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.tableLayoutPanel1);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(696, 579);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "URL제목 분리";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.textBoxURLTitle, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxURL, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxTitle, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.buttonSplitURLTitle, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxWebSite, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.textBoxManagedUrl, 2, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(696, 579);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// textBoxURLTitle
			// 
			this.textBoxURLTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxURLTitle.Location = new System.Drawing.Point(3, 3);
			this.textBoxURLTitle.Multiline = true;
			this.textBoxURLTitle.Name = "textBoxURLTitle";
			this.tableLayoutPanel1.SetRowSpan(this.textBoxURLTitle, 4);
			this.textBoxURLTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxURLTitle.Size = new System.Drawing.Size(330, 573);
			this.textBoxURLTitle.TabIndex = 0;
			// 
			// textBoxURL
			// 
			this.textBoxURL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxURL.Location = new System.Drawing.Point(363, 3);
			this.textBoxURL.Multiline = true;
			this.textBoxURL.Name = "textBoxURL";
			this.textBoxURL.ReadOnly = true;
			this.textBoxURL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxURL.Size = new System.Drawing.Size(330, 138);
			this.textBoxURL.TabIndex = 1;
			this.textBoxURL.Click += new System.EventHandler(this.TextBoxURL_Click);
			// 
			// textBoxTitle
			// 
			this.textBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxTitle.Location = new System.Drawing.Point(363, 147);
			this.textBoxTitle.Multiline = true;
			this.textBoxTitle.Name = "textBoxTitle";
			this.textBoxTitle.ReadOnly = true;
			this.textBoxTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxTitle.Size = new System.Drawing.Size(330, 138);
			this.textBoxTitle.TabIndex = 2;
			this.textBoxTitle.Click += new System.EventHandler(this.TextBoxTitle_Click);
			// 
			// buttonSplitURLTitle
			// 
			this.buttonSplitURLTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonSplitURLTitle.Location = new System.Drawing.Point(339, 3);
			this.buttonSplitURLTitle.Name = "buttonSplitURLTitle";
			this.tableLayoutPanel1.SetRowSpan(this.buttonSplitURLTitle, 4);
			this.buttonSplitURLTitle.Size = new System.Drawing.Size(18, 573);
			this.buttonSplitURLTitle.TabIndex = 3;
			this.buttonSplitURLTitle.Text = ">";
			this.buttonSplitURLTitle.UseVisualStyleBackColor = true;
			this.buttonSplitURLTitle.Click += new System.EventHandler(this.ButtonSplitURLTitle_Click);
			// 
			// textBoxWebSite
			// 
			this.textBoxWebSite.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxWebSite.Location = new System.Drawing.Point(363, 291);
			this.textBoxWebSite.Multiline = true;
			this.textBoxWebSite.Name = "textBoxWebSite";
			this.textBoxWebSite.ReadOnly = true;
			this.textBoxWebSite.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxWebSite.Size = new System.Drawing.Size(330, 138);
			this.textBoxWebSite.TabIndex = 4;
			this.textBoxWebSite.Click += new System.EventHandler(this.TextBoxWebSite_Click);
			// 
			// textBoxManagedUrl
			// 
			this.textBoxManagedUrl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxManagedUrl.Location = new System.Drawing.Point(363, 435);
			this.textBoxManagedUrl.Multiline = true;
			this.textBoxManagedUrl.Name = "textBoxManagedUrl";
			this.textBoxManagedUrl.ReadOnly = true;
			this.textBoxManagedUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxManagedUrl.Size = new System.Drawing.Size(330, 141);
			this.textBoxManagedUrl.TabIndex = 5;
			this.textBoxManagedUrl.Click += new System.EventHandler(this.TextBoxManagedUrl_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(712, 613);
			this.Controls.Add(tabControl1);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "URL 검색기";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
			tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TextBox textBoxFind;
		private System.Windows.Forms.Button buttonFixUrl;
		private System.Windows.Forms.TextBox textBoxManagedSite;
		private System.Windows.Forms.Button buttonFind;
		private System.Windows.Forms.ListView listViewFind;
		private System.Windows.Forms.TextBox textBoxFindingPaths;
		private System.Windows.Forms.TextBox textBoxFilePatterns;
		private System.Windows.Forms.ColumnHeader columnHeaderLog;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox textBoxMultiline;
		private System.Windows.Forms.Button buttonMergeLine;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox textBoxURLTitle;
		private System.Windows.Forms.TextBox textBoxURL;
		private System.Windows.Forms.TextBox textBoxTitle;
		private System.Windows.Forms.Button buttonSplitURLTitle;
		private System.Windows.Forms.TextBox textBoxWebSite;
		private System.Windows.Forms.TextBox textBoxManagedUrl;
	}
}

