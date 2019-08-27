using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SDConnectorUseCaseEngine;

namespace SDConnectorUseCaseNewEMail
{
	public partial class SDConnectorUseCaseNewEMail : Form
	{
		public SDConnectorUseCaseNewEMail()
		{
			InitializeComponent();
		}
		
		private void bnSendMail_Click(object sender, EventArgs e)
		{
			bnSendMail.Enabled = false;

			try
			{
				string[] recipients = tbRecipients.Text.Split(new char[] { ',' });
				string subject = tbSubject.Text;
				string content = tbContent.Text;
				bool encrypt = cbEncrypt.Checked;
				bool sign = cbSign.Checked;

				Engine engine = new Engine();

				bool okToSend = true;

				if (okToSend && recipients.Length == 0)
				{
					MessageBox.Show(this, "No recipient defined", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					okToSend = false;
				}

				if (okToSend && (encrypt || sign))
				{
					if (!engine.UserIsConnected())
					{
						MessageBox.Show(this, "A user must be connected to Stormshield Data Security", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						okToSend = false;
					}
				}

				if (okToSend)
				{
					engine.SendMail(recipients, subject, content, encrypt, sign);
					MessageBox.Show(this, "Mail sent successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			bnSendMail.Enabled = true;
		}
	}
}
