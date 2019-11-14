using System;
using System.Collections.Generic;

using Stormshield.DataSecurity.Connector.Common;
using Stormshield.DataSecurity.Connector.File;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class SetCoworkers
	{
		/// <summary>
		/// This example demonstrates the use of the Set-SDSFileCoworker API
		/// </summary>
		/// <remarks>All coworkers are replaced with the specified ones (author will not be removed)</remarks>
		/// <example>SetCoworkers "C:\My Folder\Document.docx.sbox" alicesmith@mycompany.com</example>
		/// <example>SetCoworkers "C:\My Folder\Document.docx.sbox" alicesmith@mycompany.com,jodiefisher@mycompany.com</example>
		/// <example>SetCoworkers "C:\My Folder\Document.docx.sbox" alicesmith@mycompany.com,jodiefisher@mycompany.com,robertsmith@mycompany.com</example>
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

					object[] certificates = api.Execute(string.Format("Get-SDSCertificate -EmailAddress {0}", emailAddresses));
					if (certificates == null || certificates.Length != emailAddresses.Split(new char[] { ',' }).Length)
						throw new InvalidOperationException("One or more certificates not found");

					KeyValuePair<string, Object>[] parameters = new KeyValuePair<string, Object>[] 
					{
						new KeyValuePair<string, Object>("Path", sboxPath),
						new KeyValuePair<string, Object>("Coworkers", certificates)
					};
					objects = api.Execute("Set-SDSFileCoworker", parameters);
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Set-SDSFileCoworker");

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
