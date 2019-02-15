namespace URLFinder.Controls
{
	partial class ClipboardAutomator
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

		#region 구성 요소 디자이너에서 생성한 코드

		/// <summary> 
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent ()
		{
			this.groupBoxOutline = new System.Windows.Forms.GroupBox();
			this.checkBoxFixURL = new System.Windows.Forms.CheckBox();
			this.checkBoxDateRearrange = new System.Windows.Forms.CheckBox();
			this.checkBoxLineMerger = new System.Windows.Forms.CheckBox();
			this.checkBoxUseAutomation = new System.Windows.Forms.CheckBox();
			this.groupBoxOutline.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxOutline
			// 
			this.groupBoxOutline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxOutline.Controls.Add(this.checkBoxFixURL);
			this.groupBoxOutline.Controls.Add(this.checkBoxDateRearrange);
			this.groupBoxOutline.Controls.Add(this.checkBoxLineMerger);
			this.groupBoxOutline.Location = new System.Drawing.Point(3, 3);
			this.groupBoxOutline.Name = "groupBoxOutline";
			this.groupBoxOutline.Size = new System.Drawing.Size(194, 94);
			this.groupBoxOutline.TabIndex = 0;
			this.groupBoxOutline.TabStop = false;
			// 
			// checkBoxFixURL
			// 
			this.checkBoxFixURL.AutoSize = true;
			this.checkBoxFixURL.Checked = true;
			this.checkBoxFixURL.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxFixURL.Location = new System.Drawing.Point(6, 66);
			this.checkBoxFixURL.Name = "checkBoxFixURL";
			this.checkBoxFixURL.Size = new System.Drawing.Size(75, 16);
			this.checkBoxFixURL.TabIndex = 2;
			this.checkBoxFixURL.Text = "URL 교정";
			this.checkBoxFixURL.UseVisualStyleBackColor = true;
			// 
			// checkBoxDateRearrange
			// 
			this.checkBoxDateRearrange.AutoSize = true;
			this.checkBoxDateRearrange.Checked = true;
			this.checkBoxDateRearrange.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxDateRearrange.Location = new System.Drawing.Point(6, 44);
			this.checkBoxDateRearrange.Name = "checkBoxDateRearrange";
			this.checkBoxDateRearrange.Size = new System.Drawing.Size(186, 16);
			this.checkBoxDateRearrange.TabIndex = 1;
			this.checkBoxDateRearrange.Text = "날짜를 YYYY-MM-DD로 변환";
			this.checkBoxDateRearrange.UseVisualStyleBackColor = true;
			// 
			// checkBoxLineMerger
			// 
			this.checkBoxLineMerger.AutoSize = true;
			this.checkBoxLineMerger.Checked = true;
			this.checkBoxLineMerger.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxLineMerger.Location = new System.Drawing.Point(6, 22);
			this.checkBoxLineMerger.Name = "checkBoxLineMerger";
			this.checkBoxLineMerger.Size = new System.Drawing.Size(160, 16);
			this.checkBoxLineMerger.TabIndex = 0;
			this.checkBoxLineMerger.Text = "텍스트 줄을 한 줄로 병합";
			this.checkBoxLineMerger.UseVisualStyleBackColor = true;
			// 
			// checkBoxUseAutomation
			// 
			this.checkBoxUseAutomation.AutoSize = true;
			this.checkBoxUseAutomation.Checked = true;
			this.checkBoxUseAutomation.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxUseAutomation.Location = new System.Drawing.Point(8, 3);
			this.checkBoxUseAutomation.Name = "checkBoxUseAutomation";
			this.checkBoxUseAutomation.Size = new System.Drawing.Size(140, 16);
			this.checkBoxUseAutomation.TabIndex = 1;
			this.checkBoxUseAutomation.Text = "클립보드 자동화 사용";
			this.checkBoxUseAutomation.UseVisualStyleBackColor = true;
			this.checkBoxUseAutomation.CheckedChanged += new System.EventHandler(this.CheckBoxUseAutomation_CheckedChanged);
			// 
			// ClipboardAutomator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxUseAutomation);
			this.Controls.Add(this.groupBoxOutline);
			this.MaximumSize = new System.Drawing.Size(200, 124);
			this.MinimumSize = new System.Drawing.Size(200, 124);
			this.Name = "ClipboardAutomator";
			this.Size = new System.Drawing.Size(200, 124);
			this.groupBoxOutline.ResumeLayout(false);
			this.groupBoxOutline.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxOutline;
		private System.Windows.Forms.CheckBox checkBoxUseAutomation;
		private System.Windows.Forms.CheckBox checkBoxLineMerger;
		private System.Windows.Forms.CheckBox checkBoxDateRearrange;
		private System.Windows.Forms.CheckBox checkBoxFixURL;
	}
}
