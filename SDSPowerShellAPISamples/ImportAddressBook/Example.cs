using System;

using Stormshield.DataSecurity.Connector;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class ImportAddressBook
	{
		/// <summary>
		/// This example demonstrates the use of the Import-SDSAddressBook API
		/// </summary>
		/// <example>ImportAddressBook C:\AddressBook.p7b</example>
		/// <example>ImportAddressBook C:\AddressBook.p7z</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 1)
					throw new ArgumentException("Missing parameters");

				string p7xPath = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Import-SDSAddressBook '{0}'", p7xPath));
					// objects should be null
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
