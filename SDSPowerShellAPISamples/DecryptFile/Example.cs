using System;
using System.Collections.Generic;

using Stormshield.DataSecurity.Connector.File;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class DecryptFile
	{
		/// <summary>
		/// This example demonstrates the use of the Unprotect-SDSFile API
		/// It allows a user to decrypt one or more files encrypted with Stormshield Data File component
		/// </summary>
		/// <example>DecryptFile C:\Document.docx.sbox</example>
		/// <example>DecryptFile "C:\Document.docx.sbox,'C:\My Folder\Document.xlsx.sbox'"</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 1)
					throw new ArgumentException("Missing parameters");

				string sboxPathes = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Unprotect-SDSFile {0}", sboxPathes));
					if (objects == null || objects.Length != sboxPathes.Split(new char[] { ',' }).Length)
						throw new InvalidOperationException("Unprotect-SDSFile");

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
