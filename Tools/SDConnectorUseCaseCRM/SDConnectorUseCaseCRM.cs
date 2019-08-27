using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using SDConnectorUseCaseEngine;

namespace SDConnectorUseCaseCRM
{
	public partial class SDSPowerShellAPICRM : Form
	{
		public SDSPowerShellAPICRM()
		{
			InitializeComponent();
		}
		
		private void fileList_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void fileList_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			foreach (string file in files)
			{
				ListViewItem item = new ListViewItem(file);
				fileList.Items.Add(item);
			}
		}

		private void createMail_Click(object sender, EventArgs e)
		{
			createMail.Enabled = false;

			try
			{
				bool okToCreateMail = true;

				string[] recipientsEmailAddresses = recipientList.Text.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

				Engine engine = new Engine();

				if (okToCreateMail && recipientsEmailAddresses.Length == 0)
				{
					MessageBox.Show(this, "No recipient defined", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					okToCreateMail = false;
				}

				if (okToCreateMail && !engine.UserIsConnected())
				{
					MessageBox.Show(this, "A user must be connected to Stormshield Data Security", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					okToCreateMail = false;
				}

				if (okToCreateMail)
				{
					List<string> attachedFiles = new List<string>();
					foreach (ListViewItem item in fileList.Items)
						attachedFiles.Add(item.Text);

					engine.CreateNewMailWithEncryptedAttachments(recipientsEmailAddresses, attachedFiles.ToArray());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			createMail.Enabled = true;
		}
	}
}
