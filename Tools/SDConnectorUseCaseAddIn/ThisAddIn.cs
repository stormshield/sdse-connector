using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;

using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

using SDConnectorUseCaseEngine;

namespace SDSPowerShellAPIAddIn
{
	public partial class ThisAddIn
	{
		private void ThisAddIn_Startup(object sender, System.EventArgs e)
		{
			this.Application.ItemSend += Application_ItemSend;
		}

		private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
		{
		}

		#region VSTO generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InternalStartup()
		{
			this.Startup += new System.EventHandler(ThisAddIn_Startup);
			this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
		}

		#endregion

		void Application_ItemSend(object Item, ref bool Cancel)
        {
            if (Item is Outlook.MailItem)
            {
                Outlook.MailItem mailItem = Item as Outlook.MailItem;
                if (mailItem.Attachments.Count > 0)
                {
                    DialogResult answer = MessageBox.Show("Encrypt attached files with Stormshield Data File?", "Stormshield", MessageBoxButtons.YesNo);
                    if (answer == DialogResult.Yes)
                    {
						try
						{
							bool okToEncrypt = true;

							Engine engine = new Engine();

							if (okToEncrypt && !engine.UserIsConnected())
							{
								MessageBox.Show("A user must be connected to Stormshield Data Security", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								okToEncrypt = false;
							}

							if (okToEncrypt)
							{
								List<string> recipients = new List<string>();
								foreach (Outlook.Recipient recipient in mailItem.Recipients)
									recipients.Add(recipient.Address);

								List<string> newAttachments = new List<string>();
								List<string> attachmentsName = new List<string>();

								foreach (Outlook.Attachment attachment in mailItem.Attachments)
								{
									string path = Path.GetTempFileName();
									string newPath = Path.Combine(Path.GetDirectoryName(path), attachment.FileName);
									if (File.Exists(newPath))
										File.Delete(newPath);
									File.Move(path, newPath);
									path = newPath;

									attachment.SaveAsFile(path);
									attachmentsName.Add(attachment.FileName);

									string[] compressedPath = engine.EncryptFilesWithStormshieldDataFile(new string[] { path }, recipients.ToArray());
									File.Delete(path);

									newAttachments.Add(compressedPath[0]);
								}

								int count = mailItem.Attachments.Count;
								for (int i = count; i >= 0; i--)
									mailItem.Attachments.Remove(i);

								int ind = 0;
								foreach (string path in newAttachments)
								{
									mailItem.Attachments.Add(path, Outlook.OlAttachmentType.olByValue, 1, attachmentsName[ind]);
									ind++;
								}
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString());
							Cancel = true;
						}
                    }
                }
            }
        }
	}
}
