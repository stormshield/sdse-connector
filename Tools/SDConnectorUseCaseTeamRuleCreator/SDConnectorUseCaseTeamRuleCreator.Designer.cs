namespace SDConnectorUseCaseTeamRuleCreator
{
	partial class SDConnectorUseCaseTeamRuleCreator
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDConnectorUseCaseTeamRuleCreator));
			this.btnBrowse = new System.Windows.Forms.Button();
			this.folderLabel = new System.Windows.Forms.Label();
			this.folder = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.bnDetectCoworkers = new System.Windows.Forms.Button();
			this.tbCoworkers = new System.Windows.Forms.TextBox();
			this.bnCreateTeamRule = new System.Windows.Forms.Button();
			this.labelCoworkers = new System.Windows.Forms.Label();
			this.labelProgress = new System.Windows.Forms.Label();
			this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
			this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// btnBrowse
			// 
			this.btnBrowse.BackColor = System.Drawing.SystemColors.Window;
			this.btnBrowse.BackgroundImage = global::SDConnectorUseCaseTeamRuleCreator.Properties.Resources.folder;
			this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnBrowse.FlatAppearance.BorderSize = 0;
			this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnBrowse.Location = new System.Drawing.Point(333, 34);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(14, 14);
			this.btnBrowse.TabIndex = 10;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = false;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// folderLabel
			// 
			this.folderLabel.AutoSize = true;
			this.folderLabel.Location = new System.Drawing.Point(9, 16);
			this.folderLabel.Name = "folderLabel";
			this.folderLabel.Size = new System.Drawing.Size(128, 13);
			this.folderLabel.TabIndex = 9;
			this.folderLabel.Text = "Folder path to create rule:";
			// 
			// folder
			// 
			this.folder.Location = new System.Drawing.Point(12, 32);
			this.folder.Name = "folder";
			this.folder.Size = new System.Drawing.Size(340, 20);
			this.folder.TabIndex = 8;
			// 
			// folderBrowserDialog
			// 
			this.folderBrowserDialog.ShowNewFolderButton = false;
			// 
			// bnDetectCoworkers
			// 
			this.bnDetectCoworkers.Location = new System.Drawing.Point(15, 197);
			this.bnDetectCoworkers.Name = "bnDetectCoworkers";
			this.bnDetectCoworkers.Size = new System.Drawing.Size(149, 23);
			this.bnDetectCoworkers.TabIndex = 11;
			this.bnDetectCoworkers.Text = "Auto-detect coworkers";
			this.bnDetectCoworkers.UseVisualStyleBackColor = true;
			this.bnDetectCoworkers.Click += new System.EventHandler(this.bnDetectCoworkers_Click);
			// 
			// tbCoworkers
			// 
			this.tbCoworkers.BackColor = System.Drawing.SystemColors.Window;
			this.tbCoworkers.Location = new System.Drawing.Point(12, 94);
			this.tbCoworkers.Multiline = true;
			this.tbCoworkers.Name = "tbCoworkers";
			this.tbCoworkers.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbCoworkers.Size = new System.Drawing.Size(340, 97);
			this.tbCoworkers.TabIndex = 12;
			// 
			// bnCreateTeamRule
			// 
			this.bnCreateTeamRule.Location = new System.Drawing.Point(402, 163);
			this.bnCreateTeamRule.Name = "bnCreateTeamRule";
			this.bnCreateTeamRule.Size = new System.Drawing.Size(112, 23);
			this.bnCreateTeamRule.TabIndex = 13;
			this.bnCreateTeamRule.Text = "Create Team rule";
			this.bnCreateTeamRule.UseVisualStyleBackColor = true;
			this.bnCreateTeamRule.Click += new System.EventHandler(this.bnCreateTeamRule_Click);
			// 
			// labelCoworkers
			// 
			this.labelCoworkers.AutoSize = true;
			this.labelCoworkers.Location = new System.Drawing.Point(12, 78);
			this.labelCoworkers.Name = "labelCoworkers";
			this.labelCoworkers.Size = new System.Drawing.Size(141, 13);
			this.labelCoworkers.TabIndex = 14;
			this.labelCoworkers.Text = "Coworkers e-mail addresses:";
			// 
			// labelProgress
			// 
			this.labelProgress.AutoSize = true;
			this.labelProgress.Location = new System.Drawing.Point(190, 202);
			this.labelProgress.Name = "labelProgress";
			this.labelProgress.Size = new System.Drawing.Size(61, 13);
			this.labelProgress.TabIndex = 17;
			this.labelProgress.Text = "<message>";
			// 
			// pictureBoxLoading
			// 
			this.pictureBoxLoading.Image = global::SDConnectorUseCaseTeamRuleCreator.Properties.Resources.loader;
			this.pictureBoxLoading.Location = new System.Drawing.Point(168, 201);
			this.pictureBoxLoading.Name = "pictureBoxLoading";
			this.pictureBoxLoading.Size = new System.Drawing.Size(16, 16);
			this.pictureBoxLoading.TabIndex = 16;
			this.pictureBoxLoading.TabStop = false;
			// 
			// pictureBoxIcon
			// 
			this.pictureBoxIcon.BackgroundImage = global::SDConnectorUseCaseTeamRuleCreator.Properties.Resources.icon;
			this.pictureBoxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBoxIcon.Location = new System.Drawing.Point(402, 45);
			this.pictureBoxIcon.Name = "pictureBoxIcon";
			this.pictureBoxIcon.Size = new System.Drawing.Size(112, 112);
			this.pictureBoxIcon.TabIndex = 15;
			this.pictureBoxIcon.TabStop = false;
			// 
			// SDConnectorUseCaseTeamRuleCreator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 232);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.labelProgress);
			this.Controls.Add(this.pictureBoxLoading);
			this.Controls.Add(this.pictureBoxIcon);
			this.Controls.Add(this.labelCoworkers);
			this.Controls.Add(this.bnCreateTeamRule);
			this.Controls.Add(this.tbCoworkers);
			this.Controls.Add(this.bnDetectCoworkers);
			this.Controls.Add(this.folderLabel);
			this.Controls.Add(this.folder);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 270);
			this.MinimumSize = new System.Drawing.Size(540, 270);
			this.Name = "SDConnectorUseCaseTeamRuleCreator";
			this.Text = "Stormshield Data Connector - Team Rule Creator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SDConnectorUseCaseTeamRuleCreator_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label folderLabel;
		private System.Windows.Forms.TextBox folder;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.Button bnDetectCoworkers;
		private System.Windows.Forms.TextBox tbCoworkers;
		private System.Windows.Forms.Button bnCreateTeamRule;
		private System.Windows.Forms.Label labelCoworkers;
		private System.Windows.Forms.PictureBox pictureBoxIcon;
		private System.Windows.Forms.PictureBox pictureBoxLoading;
		private System.Windows.Forms.Label labelProgress;
	}
}

