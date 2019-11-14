using System;
using System.IO;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class ExportAddressBook
	{
		/// <summary>
		/// This example demonstrates the use of the Export-SDSAddressBook API
		/// It allows a user to export all certificates of its address book to a .p7b file, including trusted chain, groups and contacts
		/// </summary>
		/// <example>ExportAddressBook C:\AddressBook.p7b</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 1)
					throw new ArgumentException("Missing parameters");

				string p7bPath = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Export-SDSAddressBook '{0}' -ExportContactsAndGroups -ExportAncestry", p7bPath));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Export-SDSAddressBook");

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
