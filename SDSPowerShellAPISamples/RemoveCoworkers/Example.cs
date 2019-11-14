using System;

using Stormshield.DataSecurity.Connector.File;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class RemoveCoworkers
	{
		/// <summary>
		/// This example demonstrates the use of the Remove-SDSFileCoworker API
		/// </summary>
		/// <example>RemoveCoworkers "C:\My Folder\Document.docx.sbox" alicesmith@mycompany.com</example>
		/// <example>RemoveCoworkers "C:\My Folder\Document.docx.sbox" alicesmith@mycompany.com,jodiefisher@mycompany.com</example>
		/// <example>RemoveCoworkers "C:\My Folder\Document.docx.sbox" alicesmith@mycompany.com,jodiefisher@mycompany.com,robertsmith@mycompany.com</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 2)
					throw new ArgumentException("Missing parameters");

				string sboxPath = args[0];
				string emailAddresses = args[1];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Remove-SDSFileCoworker '{0}' -EmailAddress {1}", sboxPath, emailAddresses));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Remove-SDSFileCoworker");

					SecureFile secureFile = objects[0] as SecureFile;
					Console.WriteLine(string.Format("Return:\n{0}", secureFile.Path));
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
