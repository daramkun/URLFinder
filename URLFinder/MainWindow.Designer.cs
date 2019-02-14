namespace URLFinder
{
	partial class MainWindow
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageURLFind = new System.Windows.Forms.TabPage();
			this.treeViewLog = new System.Windows.Forms.TreeView();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.tabPageMergeLines = new System.Windows.Forms.TabPage();
			this.buttonMergedCopy = new System.Windows.Forms.Button();
			this.textBoxLineMerge = new System.Windows.Forms.TextBox();
			this.tabPageSplitURLTitle = new System.Windows.Forms.TabPage();
			this.tableLayoutPanelSplit = new System.Windows.Forms.TableLayoutPanel();
			this.textBoxSplittedBaseURL = new System.Windows.Forms.TextBox();
			this.textBoxSplittedSiteName = new System.Windows.Forms.TextBox();
			this.textBoxSplittedTitle = new System.Windows.Forms.TextBox();
			this.buttonSplit = new System.Windows.Forms.Button();
			this.textBoxBeforeSplit = new System.Windows.Forms.TextBox();
			this.textBoxSplittedURL = new System.Windows.Forms.TextBox();
			this.tabPageManagement = new System.Windows.Forms.TabPage();
			this.groupBoxSettings = new System.Windows.Forms.GroupBox();
			this.buttonUnregisterStartProgram = new System.Windows.Forms.Button();
			this.buttonRegisterStartProgramWithStartupUtility = new System.Windows.Forms.Button();
			this.buttonRegisterStartProgram = new System.Windows.Forms.Button();
			this.groupBoxKeywords = new System.Windows.Forms.GroupBox();
			this.tabControlKeywords = new System.Windows.Forms.TabControl();
			this.tabPageMondayKeywords = new System.Windows.Forms.TabPage();
			this.textBoxKeywordMonday = new System.Windows.Forms.TextBox();
			this.tabPageTuesdayKeywords = new System.Windows.Forms.TabPage();
			this.textBoxKeywordTuesday = new System.Windows.Forms.TextBox();
			this.tabPageWednesdayKeywords = new System.Windows.Forms.TabPage();
			this.textBoxKeywordWednesday = new System.Windows.Forms.TextBox();
			this.tabPageThursdayKeywords = new System.Windows.Forms.TabPage();
			this.textBoxKeywordThursday = new System.Windows.Forms.TextBox();
			this.tabPageFridayKeywords = new System.Windows.Forms.TabPage();
			this.textBoxKeywordFriday = new System.Windows.Forms.TextBox();
			this.tabPageAdditionalKeywords = new System.Windows.Forms.TabPage();
			this.textBoxKeywordAdditional = new System.Windows.Forms.TextBox();
			this.groupBoxManagementByDate = new System.Windows.Forms.GroupBox();
			this.buttonManagementArchiving = new System.Windows.Forms.Button();
			this.buttonManagementOpenPDFs = new System.Windows.Forms.Button();
			this.buttonManagementOpenXLSX = new System.Windows.Forms.Button();
			this.buttonManagementOpenHWP = new System.Windows.Forms.Button();
			this.buttonManagementOpenFolder = new System.Windows.Forms.Button();
			this.monthCalendar = new System.Windows.Forms.MonthCalendar();
			this.textBoxSearch = new URLFinder.Controls.PasteDetectableTextBox();
			this.clipboardAutomator = new URLFinder.Controls.ClipboardAutomator();
			this.tabControl.SuspendLayout();
			this.tabPageURLFind.SuspendLayout();
			this.tabPageMergeLines.SuspendLayout();
			this.tabPageSplitURLTitle.SuspendLayout();
			this.tableLayoutPanelSplit.SuspendLayout();
			this.tabPageManagement.SuspendLayout();
			this.groupBoxSettings.SuspendLayout();
			this.groupBoxKeywords.SuspendLayout();
			this.tabControlKeywords.SuspendLayout();
			this.tabPageMondayKeywords.SuspendLayout();
			this.tabPageTuesdayKeywords.SuspendLayout();
			this.tabPageWednesdayKeywords.SuspendLayout();
			this.tabPageThursdayKeywords.SuspendLayout();
			this.tabPageFridayKeywords.SuspendLayout();
			this.tabPageAdditionalKeywords.SuspendLayout();
			this.groupBoxManagementByDate.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tabPageURLFind);
			this.tabControl.Controls.Add(this.tabPageMergeLines);
			this.tabControl.Controls.Add(this.tabPageSplitURLTitle);
			this.tabControl.Controls.Add(this.tabPageManagement);
			this.tabControl.Enabled = false;
			this.tabControl.HotTrack = true;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl.Name = "tabControl";
			this.tabControl.Padding = new System.Drawing.Point(0, 0);
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(597, 608);
			this.tabControl.TabIndex = 0;
			// 
			// tabPageURLFind
			// 
			this.tabPageURLFind.Controls.Add(this.treeViewLog);
			this.tabPageURLFind.Controls.Add(this.buttonSearch);
			this.tabPageURLFind.Controls.Add(this.textBoxSearch);
			this.tabPageURLFind.Location = new System.Drawing.Point(4, 24);
			this.tabPageURLFind.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPageURLFind.Name = "tabPageURLFind";
			this.tabPageURLFind.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPageURLFind.Size = new System.Drawing.Size(589, 580);
			this.tabPageURLFind.TabIndex = 0;
			this.tabPageURLFind.Text = "URL 검색";
			this.tabPageURLFind.UseVisualStyleBackColor = true;
			// 
			// treeViewLog
			// 
			this.treeViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.treeViewLog.FullRowSelect = true;
			this.treeViewLog.Location = new System.Drawing.Point(8, 36);
			this.treeViewLog.Name = "treeViewLog";
			this.treeViewLog.ShowRootLines = false;
			this.treeViewLog.Size = new System.Drawing.Size(571, 528);
			this.treeViewLog.TabIndex = 2;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSearch.Location = new System.Drawing.Point(504, 7);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(75, 23);
			this.buttonSearch.TabIndex = 1;
			this.buttonSearch.Text = "검색";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
			// 
			// tabPageMergeLines
			// 
			this.tabPageMergeLines.Controls.Add(this.buttonMergedCopy);
			this.tabPageMergeLines.Controls.Add(this.textBoxLineMerge);
			this.tabPageMergeLines.Location = new System.Drawing.Point(4, 24);
			this.tabPageMergeLines.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPageMergeLines.Name = "tabPageMergeLines";
			this.tabPageMergeLines.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPageMergeLines.Size = new System.Drawing.Size(589, 580);
			this.tabPageMergeLines.TabIndex = 1;
			this.tabPageMergeLines.Text = "줄 병합";
			this.tabPageMergeLines.UseVisualStyleBackColor = true;
			// 
			// buttonMergedCopy
			// 
			this.buttonMergedCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonMergedCopy.Location = new System.Drawing.Point(8, 548);
			this.buttonMergedCopy.Name = "buttonMergedCopy";
			this.buttonMergedCopy.Size = new System.Drawing.Size(572, 23);
			this.buttonMergedCopy.TabIndex = 1;
			this.buttonMergedCopy.Text = "복사";
			this.buttonMergedCopy.UseVisualStyleBackColor = true;
			this.buttonMergedCopy.Click += new System.EventHandler(this.ButtonMergedCopy_Click);
			// 
			// textBoxLineMerge
			// 
			this.textBoxLineMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLineMerge.Location = new System.Drawing.Point(8, 7);
			this.textBoxLineMerge.Multiline = true;
			this.textBoxLineMerge.Name = "textBoxLineMerge";
			this.textBoxLineMerge.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLineMerge.Size = new System.Drawing.Size(572, 535);
			this.textBoxLineMerge.TabIndex = 0;
			// 
			// tabPageSplitURLTitle
			// 
			this.tabPageSplitURLTitle.Controls.Add(this.tableLayoutPanelSplit);
			this.tabPageSplitURLTitle.Location = new System.Drawing.Point(4, 24);
			this.tabPageSplitURLTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPageSplitURLTitle.Name = "tabPageSplitURLTitle";
			this.tabPageSplitURLTitle.Size = new System.Drawing.Size(589, 580);
			this.tabPageSplitURLTitle.TabIndex = 2;
			this.tabPageSplitURLTitle.Text = "URL/제목 분리";
			this.tabPageSplitURLTitle.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelSplit
			// 
			this.tableLayoutPanelSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanelSplit.ColumnCount = 3;
			this.tableLayoutPanelSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanelSplit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanelSplit.Controls.Add(this.textBoxSplittedBaseURL, 2, 3);
			this.tableLayoutPanelSplit.Controls.Add(this.textBoxSplittedSiteName, 2, 2);
			this.tableLayoutPanelSplit.Controls.Add(this.textBoxSplittedTitle, 2, 1);
			this.tableLayoutPanelSplit.Controls.Add(this.buttonSplit, 1, 0);
			this.tableLayoutPanelSplit.Controls.Add(this.textBoxBeforeSplit, 0, 0);
			this.tableLayoutPanelSplit.Controls.Add(this.textBoxSplittedURL, 2, 0);
			this.tableLayoutPanelSplit.Location = new System.Drawing.Point(6, 6);
			this.tableLayoutPanelSplit.Name = "tableLayoutPanelSplit";
			this.tableLayoutPanelSplit.RowCount = 4;
			this.tableLayoutPanelSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelSplit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanelSplit.Size = new System.Drawing.Size(578, 568);
			this.tableLayoutPanelSplit.TabIndex = 0;
			// 
			// textBoxSplittedBaseURL
			// 
			this.textBoxSplittedBaseURL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSplittedBaseURL.Location = new System.Drawing.Point(305, 429);
			this.textBoxSplittedBaseURL.Multiline = true;
			this.textBoxSplittedBaseURL.Name = "textBoxSplittedBaseURL";
			this.textBoxSplittedBaseURL.ReadOnly = true;
			this.textBoxSplittedBaseURL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxSplittedBaseURL.Size = new System.Drawing.Size(270, 136);
			this.textBoxSplittedBaseURL.TabIndex = 5;
			this.textBoxSplittedBaseURL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxClickToSelectAll_MouseClick);
			// 
			// textBoxSplittedSiteName
			// 
			this.textBoxSplittedSiteName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSplittedSiteName.Location = new System.Drawing.Point(305, 287);
			this.textBoxSplittedSiteName.Multiline = true;
			this.textBoxSplittedSiteName.Name = "textBoxSplittedSiteName";
			this.textBoxSplittedSiteName.ReadOnly = true;
			this.textBoxSplittedSiteName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxSplittedSiteName.Size = new System.Drawing.Size(270, 136);
			this.textBoxSplittedSiteName.TabIndex = 4;
			this.textBoxSplittedSiteName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxClickToSelectAll_MouseClick);
			// 
			// textBoxSplittedTitle
			// 
			this.textBoxSplittedTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSplittedTitle.Location = new System.Drawing.Point(305, 145);
			this.textBoxSplittedTitle.Multiline = true;
			this.textBoxSplittedTitle.Name = "textBoxSplittedTitle";
			this.textBoxSplittedTitle.ReadOnly = true;
			this.textBoxSplittedTitle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxSplittedTitle.Size = new System.Drawing.Size(270, 136);
			this.textBoxSplittedTitle.TabIndex = 3;
			this.textBoxSplittedTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxClickToSelectAll_MouseClick);
			// 
			// buttonSplit
			// 
			this.buttonSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSplit.Location = new System.Drawing.Point(279, 3);
			this.buttonSplit.Name = "buttonSplit";
			this.tableLayoutPanelSplit.SetRowSpan(this.buttonSplit, 4);
			this.buttonSplit.Size = new System.Drawing.Size(20, 562);
			this.buttonSplit.TabIndex = 0;
			this.buttonSplit.Text = ">";
			this.buttonSplit.UseVisualStyleBackColor = true;
			this.buttonSplit.Click += new System.EventHandler(this.ButtonSplit_Click);
			// 
			// textBoxBeforeSplit
			// 
			this.textBoxBeforeSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxBeforeSplit.Location = new System.Drawing.Point(3, 3);
			this.textBoxBeforeSplit.Multiline = true;
			this.textBoxBeforeSplit.Name = "textBoxBeforeSplit";
			this.tableLayoutPanelSplit.SetRowSpan(this.textBoxBeforeSplit, 4);
			this.textBoxBeforeSplit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxBeforeSplit.Size = new System.Drawing.Size(270, 562);
			this.textBoxBeforeSplit.TabIndex = 1;
			// 
			// textBoxSplittedURL
			// 
			this.textBoxSplittedURL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSplittedURL.Location = new System.Drawing.Point(305, 3);
			this.textBoxSplittedURL.Multiline = true;
			this.textBoxSplittedURL.Name = "textBoxSplittedURL";
			this.textBoxSplittedURL.ReadOnly = true;
			this.textBoxSplittedURL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxSplittedURL.Size = new System.Drawing.Size(270, 136);
			this.textBoxSplittedURL.TabIndex = 2;
			this.textBoxSplittedURL.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxClickToSelectAll_MouseClick);
			// 
			// tabPageManagement
			// 
			this.tabPageManagement.Controls.Add(this.groupBoxSettings);
			this.tabPageManagement.Controls.Add(this.groupBoxKeywords);
			this.tabPageManagement.Controls.Add(this.groupBoxManagementByDate);
			this.tabPageManagement.Location = new System.Drawing.Point(4, 24);
			this.tabPageManagement.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tabPageManagement.Name = "tabPageManagement";
			this.tabPageManagement.Size = new System.Drawing.Size(589, 580);
			this.tabPageManagement.TabIndex = 3;
			this.tabPageManagement.Text = "관리";
			this.tabPageManagement.UseVisualStyleBackColor = true;
			// 
			// groupBoxSettings
			// 
			this.groupBoxSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxSettings.Controls.Add(this.clipboardAutomator);
			this.groupBoxSettings.Controls.Add(this.buttonUnregisterStartProgram);
			this.groupBoxSettings.Controls.Add(this.buttonRegisterStartProgramWithStartupUtility);
			this.groupBoxSettings.Controls.Add(this.buttonRegisterStartProgram);
			this.groupBoxSettings.Location = new System.Drawing.Point(353, 237);
			this.groupBoxSettings.Name = "groupBoxSettings";
			this.groupBoxSettings.Size = new System.Drawing.Size(227, 334);
			this.groupBoxSettings.TabIndex = 3;
			this.groupBoxSettings.TabStop = false;
			this.groupBoxSettings.Text = "설정";
			// 
			// buttonUnregisterStartProgram
			// 
			this.buttonUnregisterStartProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUnregisterStartProgram.Location = new System.Drawing.Point(12, 80);
			this.buttonUnregisterStartProgram.Name = "buttonUnregisterStartProgram";
			this.buttonUnregisterStartProgram.Size = new System.Drawing.Size(199, 23);
			this.buttonUnregisterStartProgram.TabIndex = 5;
			this.buttonUnregisterStartProgram.Text = "시작 프로그램 등록 해제";
			this.buttonUnregisterStartProgram.UseVisualStyleBackColor = true;
			// 
			// buttonRegisterStartProgramWithStartupUtility
			// 
			this.buttonRegisterStartProgramWithStartupUtility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRegisterStartProgramWithStartupUtility.Location = new System.Drawing.Point(12, 51);
			this.buttonRegisterStartProgramWithStartupUtility.Name = "buttonRegisterStartProgramWithStartupUtility";
			this.buttonRegisterStartProgramWithStartupUtility.Size = new System.Drawing.Size(199, 23);
			this.buttonRegisterStartProgramWithStartupUtility.TabIndex = 4;
			this.buttonRegisterStartProgramWithStartupUtility.Text = "작업 준비 가동 시작 프로그램 등록";
			this.buttonRegisterStartProgramWithStartupUtility.UseVisualStyleBackColor = true;
			// 
			// buttonRegisterStartProgram
			// 
			this.buttonRegisterStartProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRegisterStartProgram.Location = new System.Drawing.Point(12, 22);
			this.buttonRegisterStartProgram.Name = "buttonRegisterStartProgram";
			this.buttonRegisterStartProgram.Size = new System.Drawing.Size(199, 23);
			this.buttonRegisterStartProgram.TabIndex = 3;
			this.buttonRegisterStartProgram.Text = "시작 프로그램 등록";
			this.buttonRegisterStartProgram.UseVisualStyleBackColor = true;
			// 
			// groupBoxKeywords
			// 
			this.groupBoxKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxKeywords.Controls.Add(this.tabControlKeywords);
			this.groupBoxKeywords.Location = new System.Drawing.Point(8, 237);
			this.groupBoxKeywords.Name = "groupBoxKeywords";
			this.groupBoxKeywords.Size = new System.Drawing.Size(339, 334);
			this.groupBoxKeywords.TabIndex = 2;
			this.groupBoxKeywords.TabStop = false;
			this.groupBoxKeywords.Text = "검색어 관리";
			// 
			// tabControlKeywords
			// 
			this.tabControlKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControlKeywords.Controls.Add(this.tabPageMondayKeywords);
			this.tabControlKeywords.Controls.Add(this.tabPageTuesdayKeywords);
			this.tabControlKeywords.Controls.Add(this.tabPageWednesdayKeywords);
			this.tabControlKeywords.Controls.Add(this.tabPageThursdayKeywords);
			this.tabControlKeywords.Controls.Add(this.tabPageFridayKeywords);
			this.tabControlKeywords.Controls.Add(this.tabPageAdditionalKeywords);
			this.tabControlKeywords.Location = new System.Drawing.Point(12, 22);
			this.tabControlKeywords.Name = "tabControlKeywords";
			this.tabControlKeywords.SelectedIndex = 0;
			this.tabControlKeywords.Size = new System.Drawing.Size(319, 300);
			this.tabControlKeywords.TabIndex = 0;
			// 
			// tabPageMondayKeywords
			// 
			this.tabPageMondayKeywords.Controls.Add(this.textBoxKeywordMonday);
			this.tabPageMondayKeywords.Location = new System.Drawing.Point(4, 24);
			this.tabPageMondayKeywords.Name = "tabPageMondayKeywords";
			this.tabPageMondayKeywords.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMondayKeywords.Size = new System.Drawing.Size(311, 272);
			this.tabPageMondayKeywords.TabIndex = 0;
			this.tabPageMondayKeywords.Text = "월";
			this.tabPageMondayKeywords.UseVisualStyleBackColor = true;
			// 
			// textBoxKeywordMonday
			// 
			this.textBoxKeywordMonday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKeywordMonday.Location = new System.Drawing.Point(6, 6);
			this.textBoxKeywordMonday.Multiline = true;
			this.textBoxKeywordMonday.Name = "textBoxKeywordMonday";
			this.textBoxKeywordMonday.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxKeywordMonday.Size = new System.Drawing.Size(299, 258);
			this.textBoxKeywordMonday.TabIndex = 0;
			// 
			// tabPageTuesdayKeywords
			// 
			this.tabPageTuesdayKeywords.Controls.Add(this.textBoxKeywordTuesday);
			this.tabPageTuesdayKeywords.Location = new System.Drawing.Point(4, 24);
			this.tabPageTuesdayKeywords.Name = "tabPageTuesdayKeywords";
			this.tabPageTuesdayKeywords.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTuesdayKeywords.Size = new System.Drawing.Size(312, 272);
			this.tabPageTuesdayKeywords.TabIndex = 1;
			this.tabPageTuesdayKeywords.Text = "화";
			this.tabPageTuesdayKeywords.UseVisualStyleBackColor = true;
			// 
			// textBoxKeywordTuesday
			// 
			this.textBoxKeywordTuesday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKeywordTuesday.Location = new System.Drawing.Point(6, 6);
			this.textBoxKeywordTuesday.Multiline = true;
			this.textBoxKeywordTuesday.Name = "textBoxKeywordTuesday";
			this.textBoxKeywordTuesday.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxKeywordTuesday.Size = new System.Drawing.Size(300, 264);
			this.textBoxKeywordTuesday.TabIndex = 1;
			// 
			// tabPageWednesdayKeywords
			// 
			this.tabPageWednesdayKeywords.Controls.Add(this.textBoxKeywordWednesday);
			this.tabPageWednesdayKeywords.Location = new System.Drawing.Point(4, 24);
			this.tabPageWednesdayKeywords.Name = "tabPageWednesdayKeywords";
			this.tabPageWednesdayKeywords.Size = new System.Drawing.Size(312, 272);
			this.tabPageWednesdayKeywords.TabIndex = 2;
			this.tabPageWednesdayKeywords.Text = "수";
			this.tabPageWednesdayKeywords.UseVisualStyleBackColor = true;
			// 
			// textBoxKeywordWednesday
			// 
			this.textBoxKeywordWednesday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKeywordWednesday.Location = new System.Drawing.Point(6, 6);
			this.textBoxKeywordWednesday.Multiline = true;
			this.textBoxKeywordWednesday.Name = "textBoxKeywordWednesday";
			this.textBoxKeywordWednesday.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxKeywordWednesday.Size = new System.Drawing.Size(300, 264);
			this.textBoxKeywordWednesday.TabIndex = 1;
			// 
			// tabPageThursdayKeywords
			// 
			this.tabPageThursdayKeywords.Controls.Add(this.textBoxKeywordThursday);
			this.tabPageThursdayKeywords.Location = new System.Drawing.Point(4, 24);
			this.tabPageThursdayKeywords.Name = "tabPageThursdayKeywords";
			this.tabPageThursdayKeywords.Size = new System.Drawing.Size(312, 272);
			this.tabPageThursdayKeywords.TabIndex = 3;
			this.tabPageThursdayKeywords.Text = "목";
			this.tabPageThursdayKeywords.UseVisualStyleBackColor = true;
			// 
			// textBoxKeywordThursday
			// 
			this.textBoxKeywordThursday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKeywordThursday.Location = new System.Drawing.Point(6, 6);
			this.textBoxKeywordThursday.Multiline = true;
			this.textBoxKeywordThursday.Name = "textBoxKeywordThursday";
			this.textBoxKeywordThursday.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxKeywordThursday.Size = new System.Drawing.Size(300, 264);
			this.textBoxKeywordThursday.TabIndex = 1;
			// 
			// tabPageFridayKeywords
			// 
			this.tabPageFridayKeywords.Controls.Add(this.textBoxKeywordFriday);
			this.tabPageFridayKeywords.Location = new System.Drawing.Point(4, 24);
			this.tabPageFridayKeywords.Name = "tabPageFridayKeywords";
			this.tabPageFridayKeywords.Size = new System.Drawing.Size(312, 272);
			this.tabPageFridayKeywords.TabIndex = 4;
			this.tabPageFridayKeywords.Text = "금";
			this.tabPageFridayKeywords.UseVisualStyleBackColor = true;
			// 
			// textBoxKeywordFriday
			// 
			this.textBoxKeywordFriday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKeywordFriday.Location = new System.Drawing.Point(6, 6);
			this.textBoxKeywordFriday.Multiline = true;
			this.textBoxKeywordFriday.Name = "textBoxKeywordFriday";
			this.textBoxKeywordFriday.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxKeywordFriday.Size = new System.Drawing.Size(300, 264);
			this.textBoxKeywordFriday.TabIndex = 1;
			// 
			// tabPageAdditionalKeywords
			// 
			this.tabPageAdditionalKeywords.Controls.Add(this.textBoxKeywordAdditional);
			this.tabPageAdditionalKeywords.Location = new System.Drawing.Point(4, 24);
			this.tabPageAdditionalKeywords.Name = "tabPageAdditionalKeywords";
			this.tabPageAdditionalKeywords.Size = new System.Drawing.Size(312, 272);
			this.tabPageAdditionalKeywords.TabIndex = 5;
			this.tabPageAdditionalKeywords.Text = "추가 검색어";
			this.tabPageAdditionalKeywords.UseVisualStyleBackColor = true;
			// 
			// textBoxKeywordAdditional
			// 
			this.textBoxKeywordAdditional.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxKeywordAdditional.Location = new System.Drawing.Point(6, 6);
			this.textBoxKeywordAdditional.Multiline = true;
			this.textBoxKeywordAdditional.Name = "textBoxKeywordAdditional";
			this.textBoxKeywordAdditional.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxKeywordAdditional.Size = new System.Drawing.Size(300, 264);
			this.textBoxKeywordAdditional.TabIndex = 1;
			// 
			// groupBoxManagementByDate
			// 
			this.groupBoxManagementByDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxManagementByDate.Controls.Add(this.buttonManagementArchiving);
			this.groupBoxManagementByDate.Controls.Add(this.buttonManagementOpenPDFs);
			this.groupBoxManagementByDate.Controls.Add(this.buttonManagementOpenXLSX);
			this.groupBoxManagementByDate.Controls.Add(this.buttonManagementOpenHWP);
			this.groupBoxManagementByDate.Controls.Add(this.buttonManagementOpenFolder);
			this.groupBoxManagementByDate.Controls.Add(this.monthCalendar);
			this.groupBoxManagementByDate.Location = new System.Drawing.Point(8, 14);
			this.groupBoxManagementByDate.Name = "groupBoxManagementByDate";
			this.groupBoxManagementByDate.Size = new System.Drawing.Size(572, 207);
			this.groupBoxManagementByDate.TabIndex = 1;
			this.groupBoxManagementByDate.TabStop = false;
			this.groupBoxManagementByDate.Text = "날짜별 관리";
			// 
			// buttonManagementArchiving
			// 
			this.buttonManagementArchiving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonManagementArchiving.Location = new System.Drawing.Point(240, 164);
			this.buttonManagementArchiving.Name = "buttonManagementArchiving";
			this.buttonManagementArchiving.Size = new System.Drawing.Size(316, 24);
			this.buttonManagementArchiving.TabIndex = 5;
			this.buttonManagementArchiving.Text = "압축하기";
			this.buttonManagementArchiving.UseVisualStyleBackColor = true;
			this.buttonManagementArchiving.Click += new System.EventHandler(this.ButtonManagementArchiving_Click);
			// 
			// buttonManagementOpenPDFs
			// 
			this.buttonManagementOpenPDFs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonManagementOpenPDFs.Location = new System.Drawing.Point(240, 130);
			this.buttonManagementOpenPDFs.Name = "buttonManagementOpenPDFs";
			this.buttonManagementOpenPDFs.Size = new System.Drawing.Size(316, 24);
			this.buttonManagementOpenPDFs.TabIndex = 4;
			this.buttonManagementOpenPDFs.Text = "PDF 파일 열기";
			this.buttonManagementOpenPDFs.UseVisualStyleBackColor = true;
			this.buttonManagementOpenPDFs.Click += new System.EventHandler(this.ButtonManagementOpenPDFs_Click);
			// 
			// buttonManagementOpenXLSX
			// 
			this.buttonManagementOpenXLSX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonManagementOpenXLSX.Location = new System.Drawing.Point(240, 96);
			this.buttonManagementOpenXLSX.Name = "buttonManagementOpenXLSX";
			this.buttonManagementOpenXLSX.Size = new System.Drawing.Size(316, 24);
			this.buttonManagementOpenXLSX.TabIndex = 3;
			this.buttonManagementOpenXLSX.Text = "엑셀 파일 열기";
			this.buttonManagementOpenXLSX.UseVisualStyleBackColor = true;
			this.buttonManagementOpenXLSX.Click += new System.EventHandler(this.ButtonManagementOpenXLSX_Click);
			// 
			// buttonManagementOpenHWP
			// 
			this.buttonManagementOpenHWP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonManagementOpenHWP.Location = new System.Drawing.Point(240, 62);
			this.buttonManagementOpenHWP.Name = "buttonManagementOpenHWP";
			this.buttonManagementOpenHWP.Size = new System.Drawing.Size(316, 24);
			this.buttonManagementOpenHWP.TabIndex = 2;
			this.buttonManagementOpenHWP.Text = "아래아 한글 파일 열기";
			this.buttonManagementOpenHWP.UseVisualStyleBackColor = true;
			this.buttonManagementOpenHWP.Click += new System.EventHandler(this.ButtonManagementOpenHWP_Click);
			// 
			// buttonManagementOpenFolder
			// 
			this.buttonManagementOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonManagementOpenFolder.Location = new System.Drawing.Point(240, 28);
			this.buttonManagementOpenFolder.Name = "buttonManagementOpenFolder";
			this.buttonManagementOpenFolder.Size = new System.Drawing.Size(316, 24);
			this.buttonManagementOpenFolder.TabIndex = 1;
			this.buttonManagementOpenFolder.Text = "폴더 열기";
			this.buttonManagementOpenFolder.UseVisualStyleBackColor = true;
			this.buttonManagementOpenFolder.Click += new System.EventHandler(this.ButtonManagementOpenFolder_Click);
			// 
			// monthCalendar
			// 
			this.monthCalendar.Location = new System.Drawing.Point(12, 28);
			this.monthCalendar.MaxSelectionCount = 1;
			this.monthCalendar.MinDate = new System.DateTime(2018, 1, 2, 0, 0, 0, 0);
			this.monthCalendar.Name = "monthCalendar";
			this.monthCalendar.TabIndex = 0;
			this.monthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendar_DateSelected);
			// 
			// textBoxSearch
			// 
			this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSearch.Location = new System.Drawing.Point(8, 7);
			this.textBoxSearch.Name = "textBoxSearch";
			this.textBoxSearch.Size = new System.Drawing.Size(490, 23);
			this.textBoxSearch.TabIndex = 0;
			this.textBoxSearch.Pasted += new System.EventHandler(this.TextBoxSearch_Pasted);
			this.textBoxSearch.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TextBoxClickToSelectAll_MouseClick);
			// 
			// clipboardAutomator
			// 
			this.clipboardAutomator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.clipboardAutomator.Location = new System.Drawing.Point(11, 109);
			this.clipboardAutomator.Name = "clipboardAutomator";
			this.clipboardAutomator.Size = new System.Drawing.Size(200, 100);
			this.clipboardAutomator.TabIndex = 6;
			// 
			// MainWindow
			// 
			this.AcceptButton = this.buttonSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(596, 607);
			this.Controls.Add(this.tabControl);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(506, 526);
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "URL 검색기";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
			this.Shown += new System.EventHandler(this.MainWindow_Shown);
			this.tabControl.ResumeLayout(false);
			this.tabPageURLFind.ResumeLayout(false);
			this.tabPageURLFind.PerformLayout();
			this.tabPageMergeLines.ResumeLayout(false);
			this.tabPageMergeLines.PerformLayout();
			this.tabPageSplitURLTitle.ResumeLayout(false);
			this.tableLayoutPanelSplit.ResumeLayout(false);
			this.tableLayoutPanelSplit.PerformLayout();
			this.tabPageManagement.ResumeLayout(false);
			this.groupBoxSettings.ResumeLayout(false);
			this.groupBoxKeywords.ResumeLayout(false);
			this.tabControlKeywords.ResumeLayout(false);
			this.tabPageMondayKeywords.ResumeLayout(false);
			this.tabPageMondayKeywords.PerformLayout();
			this.tabPageTuesdayKeywords.ResumeLayout(false);
			this.tabPageTuesdayKeywords.PerformLayout();
			this.tabPageWednesdayKeywords.ResumeLayout(false);
			this.tabPageWednesdayKeywords.PerformLayout();
			this.tabPageThursdayKeywords.ResumeLayout(false);
			this.tabPageThursdayKeywords.PerformLayout();
			this.tabPageFridayKeywords.ResumeLayout(false);
			this.tabPageFridayKeywords.PerformLayout();
			this.tabPageAdditionalKeywords.ResumeLayout(false);
			this.tabPageAdditionalKeywords.PerformLayout();
			this.groupBoxManagementByDate.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageURLFind;
		private System.Windows.Forms.TabPage tabPageMergeLines;
		private System.Windows.Forms.TabPage tabPageSplitURLTitle;
		private System.Windows.Forms.TabPage tabPageManagement;
		private System.Windows.Forms.Button buttonSearch;
		private URLFinder.Controls.PasteDetectableTextBox textBoxSearch;
		private System.Windows.Forms.TreeView treeViewLog;
		private System.Windows.Forms.Button buttonMergedCopy;
		private System.Windows.Forms.TextBox textBoxLineMerge;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSplit;
		private System.Windows.Forms.Button buttonSplit;
		private System.Windows.Forms.TextBox textBoxBeforeSplit;
		private System.Windows.Forms.TextBox textBoxSplittedBaseURL;
		private System.Windows.Forms.TextBox textBoxSplittedSiteName;
		private System.Windows.Forms.TextBox textBoxSplittedTitle;
		private System.Windows.Forms.TextBox textBoxSplittedURL;
		private System.Windows.Forms.MonthCalendar monthCalendar;
		private System.Windows.Forms.GroupBox groupBoxManagementByDate;
		private System.Windows.Forms.Button buttonManagementOpenPDFs;
		private System.Windows.Forms.Button buttonManagementOpenXLSX;
		private System.Windows.Forms.Button buttonManagementOpenHWP;
		private System.Windows.Forms.Button buttonManagementOpenFolder;
		private System.Windows.Forms.GroupBox groupBoxKeywords;
		private System.Windows.Forms.Button buttonManagementArchiving;
		private System.Windows.Forms.TabControl tabControlKeywords;
		private System.Windows.Forms.TabPage tabPageMondayKeywords;
		private System.Windows.Forms.TabPage tabPageTuesdayKeywords;
		private System.Windows.Forms.TabPage tabPageWednesdayKeywords;
		private System.Windows.Forms.TabPage tabPageThursdayKeywords;
		private System.Windows.Forms.TabPage tabPageFridayKeywords;
		private System.Windows.Forms.TabPage tabPageAdditionalKeywords;
		private System.Windows.Forms.TextBox textBoxKeywordMonday;
		private System.Windows.Forms.TextBox textBoxKeywordTuesday;
		private System.Windows.Forms.TextBox textBoxKeywordWednesday;
		private System.Windows.Forms.TextBox textBoxKeywordThursday;
		private System.Windows.Forms.TextBox textBoxKeywordFriday;
		private System.Windows.Forms.TextBox textBoxKeywordAdditional;
		private System.Windows.Forms.GroupBox groupBoxSettings;
		private System.Windows.Forms.Button buttonUnregisterStartProgram;
		private System.Windows.Forms.Button buttonRegisterStartProgramWithStartupUtility;
		private System.Windows.Forms.Button buttonRegisterStartProgram;
		private URLFinder.Controls.ClipboardAutomator clipboardAutomator;
	}
}

