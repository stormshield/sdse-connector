namespace SDConnectorUseCaseNewEMail
{
	partial class SDConnectorUseCaseNewEMail
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDConnectorUseCaseNewEMail));
			this.bnSendMail = new System.Windows.Forms.Button();
			this.cbEncrypt = new System.Windows.Forms.CheckBox();
			this.cbSign = new System.Windows.Forms.CheckBox();
			this.tbContent = new System.Windows.Forms.TextBox();
			this.tbSubject = new System.Windows.Forms.TextBox();
			this.lbSubject = new System.Windows.Forms.Label();
			this.lbRecipients = new System.Windows.Forms.Label();
			this.tbRecipients = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// bnSendMail
			// 
			this.bnSendMail.Location = new System.Drawing.Point(11, 68);
			this.bnSendMail.Name = "bnSendMail";
			this.bnSendMail.Size = new System.Drawing.Size(56, 24);
			this.bnSendMail.TabIndex = 5;
			this.bnSendMail.Text = "Send";
			this.bnSendMail.UseVisualStyleBackColor = true;
			this.bnSendMail.Click += new System.EventHandler(this.bnSendMail_Click);
			// 
			// cbEncrypt
			// 
			this.cbEncrypt.AutoSize = true;
			this.cbEncrypt.Location = new System.Drawing.Point(125, 73);
			this.cbEncrypt.Name = "cbEncrypt";
			this.cbEncrypt.Size = new System.Drawing.Size(62, 17);
			this.cbEncrypt.TabIndex = 3;
			this.cbEncrypt.Text = "Encrypt";
			this.cbEncrypt.UseVisualStyleBackColor = true;
			// 
			// cbSign
			// 
			this.cbSign.AutoSize = true;
			this.cbSign.Location = new System.Drawing.Point(193, 73);
			this.cbSign.Name = "cbSign";
			this.cbSign.Size = new System.Drawing.Size(47, 17);
			this.cbSign.TabIndex = 4;
			this.cbSign.Text = "Sign";
			this.cbSign.UseVisualStyleBackColor = true;
			// 
			// tbContent
			// 
			this.tbContent.Location = new System.Drawing.Point(11, 98);
			this.tbContent.Multiline = true;
			this.tbContent.Name = "tbContent";
			this.tbContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbContent.Size = new System.Drawing.Size(468, 151);
			this.tbContent.TabIndex = 2;
			// 
			// tbSubject
			// 
			this.tbSubject.Location = new System.Drawing.Point(125, 37);
			this.tbSubject.Name = "tbSubject";
			this.tbSubject.Size = new System.Drawing.Size(354, 20);
			this.tbSubject.TabIndex = 1;
			// 
			// lbSubject
			// 
			this.lbSubject.AutoSize = true;
			this.lbSubject.Location = new System.Drawing.Point(73, 40);
			this.lbSubject.Name = "lbSubject";
			this.lbSubject.Size = new System.Drawing.Size(46, 13);
			this.lbSubject.TabIndex = 13;
			this.lbSubject.Text = "Subject:";
			// 
			// lbRecipients
			// 
			this.lbRecipients.AutoSize = true;
			this.lbRecipients.Location = new System.Drawing.Point(96, 15);
			this.lbRecipients.Name = "lbRecipients";
			this.lbRecipients.Size = new System.Drawing.Size(23, 13);
			this.lbRecipients.TabIndex = 12;
			this.lbRecipients.Text = "To:";
			// 
			// tbRecipients
			// 
			this.tbRecipients.Location = new System.Drawing.Point(125, 12);
			this.tbRecipients.Name = "tbRecipients";
			this.tbRecipients.Size = new System.Drawing.Size(354, 20);
			this.tbRecipients.TabIndex = 0;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackgroundImage = global::SDConnectorUseCaseNewEMail.Properties.Resources.icon;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.pictureBox1.Location = new System.Drawing.Point(12, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(55, 55);
			this.pictureBox1.TabIndex = 14;
			this.pictureBox1.TabStop = false;
			// 
			// SDConnectorUseCaseNewEMail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(491, 261);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.tbContent);
			this.Controls.Add(this.tbSubject);
			this.Controls.Add(this.lbSubject);
			this.Controls.Add(this.lbRecipients);
			this.Controls.Add(this.tbRecipients);
			this.Controls.Add(this.cbSign);
			this.Controls.Add(this.cbEncrypt);
			this.Controls.Add(this.bnSendMail);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "SDConnectorUseCaseNewEMail";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Stormshield Data Connector - New Mail";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bnSendMail;
		private System.Windows.Forms.CheckBox cbEncrypt;
		private System.Windows.Forms.CheckBox cbSign;
		private System.Windows.Forms.TextBox tbContent;
		private System.Windows.Forms.TextBox tbSubject;
		private System.Windows.Forms.Label lbSubject;
		private System.Windows.Forms.Label lbRecipients;
		private System.Windows.Forms.TextBox tbRecipients;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}

