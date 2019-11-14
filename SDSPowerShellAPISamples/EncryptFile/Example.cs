using System;
using System.Collections.Generic;

using Stormshield.DataSecurity.Connector.File;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class EncryptFile
	{
		/// <summary>
		/// This example demonstrates the use of the Protect-SDSFile API
		/// It allows a user to encrypt a file with Stormshield Data File component.
		/// </summary>
		/// <example>EncryptFile C:\Document.docx</example>
		/// <example>EncryptFile C:\Document.docx alice.smith@mycompany.com</example>
		/// <example>EncryptFile C:\Document.docx alice.smith@mycompany.com,robert.miller@mycompany.com</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length == 0)
					throw new ArgumentException("Missing parameters");

				string filePath = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					if (args.Length == 1)
					{
						// with no email addresses, the file is encrypted for the currently connected user
						//objects = api.Execute(string.Format("Protect-SDSFile '{0}'", filePath));
                        objects = api.Execute("Protect-SDSFile C:\\a\\a.docx");
					}
					else
					{
						string emailAddresses = args[1];

						object[] certificates = api.Execute(string.Format("Get-SDSCertificate -EmailAddress {0}", emailAddresses));
						if (certificates == null || certificates.Length != emailAddresses.Split(new char[] { ',' }).Length)
							throw new InvalidOperationException("One or more certificates not found");

						KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>("-Path", filePath),
							new KeyValuePair<string, object>("-Coworkers", certificates)
						};
						objects = api.Execute("Protect-SDSFile", parameters);
					}

					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Protect-SDSFile");

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
