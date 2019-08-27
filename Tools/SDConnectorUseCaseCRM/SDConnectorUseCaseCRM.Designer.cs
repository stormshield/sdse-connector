namespace SDConnectorUseCaseCRM
{
	partial class SDSPowerShellAPICRM
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDSPowerShellAPICRM));
			this.fileList = new System.Windows.Forms.ListView();
			this.filePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.recipientList = new System.Windows.Forms.TextBox();
			this.createMail = new System.Windows.Forms.Button();
			this.recipientsLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// fileList
			// 
			this.fileList.AllowDrop = true;
			this.fileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.filePath});
			this.fileList.Location = new System.Drawing.Point(12, 12);
			this.fileList.Name = "fileList";
			this.fileList.Size = new System.Drawing.Size(388, 103);
			this.fileList.TabIndex = 0;
			this.fileList.UseCompatibleStateImageBehavior = false;
			this.fileList.View = System.Windows.Forms.View.Details;
			this.fileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.fileList_DragDrop);
			this.fileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.fileList_DragEnter);
			// 
			// filePath
			// 
			this.filePath.Text = "Fichiers";
			this.filePath.Width = 325;
			// 
			// recipientList
			// 
			this.recipientList.Location = new System.Drawing.Point(12, 143);
			this.recipientList.Multiline = true;
			this.recipientList.Name = "recipientList";
			this.recipientList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.recipientList.Size = new System.Drawing.Size(387, 92);
			this.recipientList.TabIndex = 1;
			// 
			// createMail
			// 
			this.createMail.Location = new System.Drawing.Point(140, 250);
			this.createMail.Name = "createMail";
			this.createMail.Size = new System.Drawing.Size(127, 24);
			this.createMail.TabIndex = 2;
			this.createMail.Text = "Créer le mail";
			this.createMail.UseVisualStyleBackColor = true;
			this.createMail.Click += new System.EventHandler(this.createMail_Click);
			// 
			// recipientsLabel
			// 
			this.recipientsLabel.AutoSize = true;
			this.recipientsLabel.Location = new System.Drawing.Point(12, 127);
			this.recipientsLabel.Name = "recipientsLabel";
			this.recipientsLabel.Size = new System.Drawing.Size(111, 13);
			this.recipientsLabel.TabIndex = 3;
			this.recipientsLabel.Text = "Liste des destinataires";
			// 
			// SDSPowerShellAPICRM
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(412, 288);
			this.Controls.Add(this.recipientsLabel);
			this.Controls.Add(this.createMail);
			this.Controls.Add(this.recipientList);
			this.Controls.Add(this.fileList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SDSPowerShellAPICRM";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Stormshield Data Security PowerShell API CRM";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView fileList;
		private System.Windows.Forms.TextBox recipientList;
		private System.Windows.Forms.Button createMail;
		private System.Windows.Forms.ColumnHeader filePath;
		private System.Windows.Forms.Label recipientsLabel;
	}
}

