using Stormshield.DataSecurity.Connector.File;
using System;
using System.Text;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class MultipleGetFile
	{
		/// <summary>
		/// This example demonstrates the use of the Get-SDSFile API
		/// </summary>
		/// <example>MultipleGetFile "C:\My Folder\Document.docx.sbox" "C:\My Folder\Document.xlsx.sbox" "C:\My Folder\Document.pdf.sbox"</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length == 0)
					throw new ArgumentException("Missing parameters");

				StringBuilder sboxPathes = new StringBuilder();
				foreach (string arg in args)
					sboxPathes.AppendFormat("'{0}',", arg);

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Get-SDSFile {0}", sboxPathes.ToString().TrimEnd(new char[] { ',' })));
					if (objects == null || objects.Length != sboxPathes.ToString().Split(new char[] { ',' }).Length)
						throw new InvalidOperationException("Get-SDSFile");

					foreach (object o in objects)
					{
						SecureFile secureFile = o as SecureFile;
						Console.WriteLine(string.Format("Return:\n{0}", secureFile.Path));
					}
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex);
				returnCode = 2;
			}

			return returnCode;
		}
	}
}
