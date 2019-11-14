using System;
using System.IO;

using Stormshield.DataSecurity.Connector;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class BackupAddressBook
	{
		/// <summary>
		/// This example demonstrates the use of the Backup-SDSAddressBook API
		/// It allows a user to save its whole address book content in a .p7z file, including personalized data related to certificates
		/// </summary>
		/// <example>BackupAddressBook C:\AddressBook.p7z</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 1)
					throw new ArgumentException("Missing parameters");

				string p7zPath = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Backup-SDSAddressBook -Path '{0}'", p7zPath));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Backup-SDSAddressBook");

					FileInfo fileInfo = objects[0] as FileInfo;
					Console.WriteLine(string.Format("Return:\n{0}", fileInfo));
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
