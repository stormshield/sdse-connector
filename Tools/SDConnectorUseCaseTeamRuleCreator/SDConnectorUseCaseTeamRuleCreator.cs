using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using Stormshield.DataSecurity.Connector.Common;

using SDConnectorUseCaseEngine;

namespace SDConnectorUseCaseTeamRuleCreator
{
	public partial class SDConnectorUseCaseTeamRuleCreator : Form
	{
		private Engine _engine = new Engine();
		private BackgroundWorker _backgroundWorker = new BackgroundWorker();

		private Dictionary<string, Tuple<string, X509Certificate>> _coworkers = new Dictionary<string, Tuple<string, X509Certificate>>();
		private List<string> _notFoundCertificates = new List<string>();

		public SDConnectorUseCaseTeamRuleCreator()
		{
			InitializeComponent();

			InitializeWorkInProgress(WorkInProgress.AutoDetectCoworkers);
			TerminateWorkInProgress();

			_engine.Initialize();

			_backgroundWorker.WorkerReportsProgress = true;
			_backgroundWorker.WorkerSupportsCancellation = true;
			_backgroundWorker.DoWork += BackgroundWorkerDoWork;
			_backgroundWorker.ProgressChanged += BackgroundWorkerProgressChanged;
			_backgroundWorker.RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
		}

		private void SDConnectorUseCaseTeamRuleCreator_FormClosing(object sender, FormClosingEventArgs e)
		{
			_engine.Terminate();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				folder.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private string originalAutoDetectString = string.Empty;

		private enum WorkInProgress
		{
			AutoDetectCoworkers,
			CreateRule
		};

		private void InitializeWorkInProgress(WorkInProgress progress)
		{
			originalAutoDetectString = bnDetectCoworkers.Text;

			folder.Enabled = false;
			btnBrowse.Enabled = false;
			bnCreateTeamRule.Enabled = false;

			if (progress == WorkInProgress.AutoDetectCoworkers)
			{
				tbCoworkers.Text = string.Empty;
				bnDetectCoworkers.Text = "Cancel";

				pictureBoxLoading.Visible = true;
				pictureBoxLoading.Enabled = true;
				labelProgress.Visible = true;
				labelProgress.Text = string.Empty;
			}
		}

		private void TerminateWorkInProgress()
		{
			folder.Enabled = true;
			btnBrowse.Enabled = true;
			bnDetectCoworkers.Text = originalAutoDetectString;
			bnCreateTeamRule.Enabled = true;
			pictureBoxLoading.Visible = false;
			pictureBoxLoading.Enabled = false;
			labelProgress.Visible = false;
		}

		private void bnDetectCoworkers_Click(object sender, EventArgs e)
		{
			if (_backgroundWorker.IsBusy)
			{
				if (_backgroundWorker.WorkerSupportsCancellation)
				{
					DialogResult answer = MessageBox.Show("Really cancel auto-detection?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (answer == DialogResult.Yes)
						_backgroundWorker.CancelAsync();
				}
			}
			else
			{
				try
				{
					bool okToDetect = true;

					string folderPath = folder.Text.Trim(new char[] { '"' });
					if (okToDetect && !Directory.Exists(folderPath))
					{
						MessageBox.Show(this, "Cannot find specified folder", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						okToDetect = false;
					}

					SDConnectorUseCaseEngine.Engine sdsEngine = new SDConnectorUseCaseEngine.Engine();

					if (okToDetect && !sdsEngine.UserIsConnected())
					{
						MessageBox.Show(this, "A user must be connected to Stormshield Data Security", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						okToDetect = false;
					}

					if (okToDetect)
					{
						InitializeWorkInProgress(WorkInProgress.AutoDetectCoworkers);
						_backgroundWorker.RunWorkerAsync(folderPath);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			string folderPath = e.Argument as string;

			_engine.Worker = worker;

			_coworkers.Clear();
			_notFoundCertificates.Clear();

			SDConnectorUseCaseEngine.Engine sdsEngine = new SDConnectorUseCaseEngine.Engine();

			Dictionary<string, string> users = _engine.FetchUsers(folderPath);
			
			int i = 0;
			foreach (KeyValuePair<string, string> user in users)
			{
				if (worker.CancellationPending)
				{
					e.Cancel = true;
					break;
				}

				string name = user.Key;
				string email = user.Value;

				X509Certificate certificate = sdsEngine.GetCertificate(email);
				if (certificate == null)
					_notFoundCertificates.Add(email);
				else
					_coworkers.Add(name, Tuple.Create<string, X509Certificate>(email, certificate));

				worker.ReportProgress(0, string.Format("Checking certificates - {0}/{1}", i+1, users.Count));

				i++;
			}
		}

		void BackgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			labelProgress.Text = e.UserState as string;
		}

		void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			TerminateWorkInProgress();

			if (e.Error != null)
			{
				MessageBox.Show(this, e.Error.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (!e.Cancelled)
			{
				if (_notFoundCertificates.Count > 0)
				{
					ListDialog dialog = new ListDialog("Certificates not found in address book", _notFoundCertificates);
					dialog.ShowDialog();
				}

				tbCoworkers.Text = string.Empty;
				foreach (KeyValuePair<string, Tuple<string, X509Certificate>> coworker in _coworkers)
					tbCoworkers.Text += string.Format("{0} : {1}{2}", coworker.Key, coworker.Value.Item1, Environment.NewLine);
			}
		}

		private void bnCreateTeamRule_Click(object sender, EventArgs e)
		{
			InitializeWorkInProgress(WorkInProgress.CreateRule);

			try
			{
				bool okToCreate = true;

				string folderPath = folder.Text;
				if (okToCreate && !Directory.Exists(folderPath))
				{
					MessageBox.Show(this, "Cannot find specified folder", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					okToCreate = false;
				}

				SDConnectorUseCaseEngine.Engine sdsEngine = new SDConnectorUseCaseEngine.Engine();

				if (okToCreate && !sdsEngine.UserIsConnected())
				{
					MessageBox.Show(this, "A user must be connected to Stormshield Data Security", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					okToCreate = false;
				}

				if (okToCreate)
				{
					List<X509Certificate> certificates = new List<X509Certificate>();
					foreach (KeyValuePair<string, Tuple<string, X509Certificate>> coworker in _coworkers)
						certificates.Add(coworker.Value.Item2);

					sdsEngine.CreateTeamRule(folderPath, certificates.ToArray());
					MessageBox.Show(this, "Rule created successfully", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			TerminateWorkInProgress();
		}
	}
}
