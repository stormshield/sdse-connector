using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDConnectorUseCaseTeamRuleCreator
{
	public partial class ListDialog : Form
	{
		public ListDialog(string title, List<string> lines)
		{
			InitializeComponent();

			this.Text = title;

			foreach (string line in lines)
			{
				textBoxEmails.Text += line;
				textBoxEmails.Text += Environment.NewLine;
			}
		}

		private void CertificatesNotFoundDialog_Load(object sender, EventArgs e)
		{
			textBoxEmails.Select(0, 1);
		}
	}
}
