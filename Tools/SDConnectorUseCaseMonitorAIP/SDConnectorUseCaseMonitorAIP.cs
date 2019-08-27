using System;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SDConnectorUseCaseEngine;

using System.Management.Automation;

namespace SDConnectorUseCaseMonitorAIP
{
    public partial class SDConnectorUseCaseMonitorAIP : Form
	{
		private Engine _engine = new Engine();
		private FileSystemWatcher _fileSystemWatcher = null;

		public SDConnectorUseCaseMonitorAIP()
		{
			InitializeComponent();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				folder.Text = folderBrowserDialog.SelectedPath;
			}
		}

		void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
		{
			if (_fileSystemWatcher != null)
			{
                try
                {
                    bool encrypt = true;

                    string filterText = filter.Text.ToLower();

                    if (Path.GetExtension(e.FullPath) == ".sbox")
                        encrypt = false;
                    else if (e.FullPath.Contains("~SBoxFile"))
                        encrypt = false;
                    PowerShell ps = PowerShell.Create();
                    ps.AddCommand("Get-AIPFileStatus").AddParameter("-Path", e.FullPath).Invoke();
                    Collection<PSObject> result = ps.Invoke();

                    if (filterText.Length > 0 && result.Count > 0)
                    {
                        PSObject psobj = result[0];
                        bool IsLabeled = bool.Parse(psobj.Members["IsLabeled"].Value.ToString());

                        if (IsLabeled)
                        {
                            string labelName = psobj.Members["MainLabelName"].Value.ToString();
                            if (!labelName.ToLower().Contains(filterText.ToLower()))
                            {
                                encrypt = false;
                            }
                        }
                        else
                        {
                            encrypt = false;
                        }
                    }

                    if (encrypt)
                    {
                        this.Invoke((MethodInvoker)delegate { log.Text += string.Format("Encrypting '{0}'{1}", e.FullPath, Environment.NewLine); });
                        _engine.EncryptFileWithStormshieldDataFile(e.FullPath);
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate { MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); });
                }
			}
		}

		private void activateButton_Click(object sender, EventArgs e)
		{
			if (_fileSystemWatcher == null)
			{
                try
                {
                    bool okToActivate = true;
                    string folderPath = folder.Text.Trim(new char[] { '"' });
                    if (okToActivate && !Directory.Exists(folderPath))
                    {
                        MessageBox.Show(this, "Cannot find specified folder", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        okToActivate = false;
                    }

                    Engine engine = new Engine();
                    if (okToActivate && !engine.UserIsConnected())
                    {
                        MessageBox.Show(this, "A user must be connected to Stormshield Data Security", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        okToActivate = false;
                    }

                    if (okToActivate)
                    {
                        _fileSystemWatcher = new FileSystemWatcher(folderPath);
                        _fileSystemWatcher.EnableRaisingEvents = true;
                        _fileSystemWatcher.Created += fileSystemWatcher_Created;

                        activateButton.Text = "Disable monitoring";
                        folder.Enabled = false;
                        filter.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
			}
			else
			{
				_fileSystemWatcher.EnableRaisingEvents = false;
				_fileSystemWatcher.Created -= fileSystemWatcher_Created;
				_fileSystemWatcher = null;

				activateButton.Text = "Enable monitoring";
				folder.Enabled = true;
				filter.Enabled = true;
			}
		}
	}
}
