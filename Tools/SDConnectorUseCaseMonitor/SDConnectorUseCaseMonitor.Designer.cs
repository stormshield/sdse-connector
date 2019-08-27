namespace SDConnectorUseCaseMonitor
{
	partial class SDConnectorUseCaseMonitor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDConnectorUseCaseMonitor));
			this.gbOptions = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.filter = new System.Windows.Forms.TextBox();
			this.lbFilter = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.folderLabel = new System.Windows.Forms.Label();
			this.folder = new System.Windows.Forms.TextBox();
			this.activateButton = new System.Windows.Forms.Button();
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.log = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.gbOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// gbOptions
			// 
			this.gbOptions.Controls.Add(this.label1);
			this.gbOptions.Controls.Add(this.filter);
			this.gbOptions.Controls.Add(this.lbFilter);
			this.gbOptions.Controls.Add(this.btnBrowse);
			this.gbOptions.Controls.Add(this.folderLabel);
			this.gbOptions.Controls.Add(this.folder);
			this.gbOptions.Location = new System.Drawing.Point(12, 12);
			this.gbOptions.Name = "gbOptions";
			this.gbOptions.Size = new System.Drawing.Size(355, 137);
			this.gbOptions.TabIndex = 4;
			this.gbOptions.TabStop = false;
			this.gbOptions.Text = "Parameters";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(287, 99);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 10;
			// 
			// filter
			// 
			this.filter.Location = new System.Drawing.Point(25, 96);
			this.filter.Name = "filter";
			this.filter.Size = new System.Drawing.Size(256, 20);
			this.filter.TabIndex = 9;
			// 
			// lbFilter
			// 
			this.lbFilter.AutoSize = true;
			this.lbFilter.Location = new System.Drawing.Point(22, 80);
			this.lbFilter.Name = "lbFilter";
			this.lbFilter.Size = new System.Drawing.Size(320, 13);
			this.lbFilter.TabIndex = 8;
			this.lbFilter.Text = "Encrypt only file names containing (leave empty to encrypt all files):";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(287, 43);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(28, 23);
			this.btnBrowse.TabIndex = 7;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// folderLabel
			// 
			this.folderLabel.AutoSize = true;
			this.folderLabel.Location = new System.Drawing.Point(22, 29);
			this.folderLabel.Name = "folderLabel";
			this.folderLabel.Size = new System.Drawing.Size(112, 13);
			this.folderLabel.TabIndex = 4;
			this.folderLabel.Text = "Folder path to monitor:";
			// 
			// folder
			// 
			this.folder.Location = new System.Drawing.Point(25, 45);
			this.folder.Name = "folder";
			this.folder.Size = new System.Drawing.Size(256, 20);
			this.folder.TabIndex = 3;
			// 
			// activateButton
			// 
			this.activateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.activateButton.Location = new System.Drawing.Point(372, 125);
			this.activateButton.Name = "activateButton";
			this.activateButton.Size = new System.Drawing.Size(101, 24);
			this.activateButton.TabIndex = 5;
			this.activateButton.Text = "Enable monitoring";
			this.activateButton.UseVisualStyleBackColor = true;
			this.activateButton.Click += new System.EventHandler(this.activateButton_Click);
			// 
			// log
			// 
			this.log.BackColor = System.Drawing.SystemColors.Window;
			this.log.Location = new System.Drawing.Point(12, 155);
			this.log.Multiline = true;
			this.log.Name = "log";
			this.log.ReadOnly = true;
			this.log.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.log.Size = new System.Drawing.Size(461, 114);
			this.log.TabIndex = 6;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::SDConnectorUseCaseMonitor.Properties.Resources.icon;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox1.Location = new System.Drawing.Point(373, 19);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 100);
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// SDConnectorUseCaseMonitor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 280);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.log);
			this.Controls.Add(this.gbOptions);
			this.Controls.Add(this.activateButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SDConnectorUseCaseMonitor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Stormshield Data Connector - Monitor";
			this.gbOptions.ResumeLayout(false);
			this.gbOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox gbOptions;
		private System.Windows.Forms.Button activateButton;
		private System.Windows.Forms.Label folderLabel;
		private System.Windows.Forms.TextBox folder;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.TextBox filter;
		private System.Windows.Forms.Label lbFilter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox log;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

