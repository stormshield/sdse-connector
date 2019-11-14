using System;
using System.Collections.Generic;

using Stormshield.DataSecurity.Connector.Common;
using Stormshield.DataSecurity.Connector.Team;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class NewTeamRule
	{
		/// <summary>
		/// This example demonstrates the use of the New-SDSTeamRule API
		/// </summary>
		/// <example>NewTeamRule "C:\My Secured Folder" alicesmith@mycompany.com</example>
		/// <example>NewTeamRule "C:\My Secured Folder" alicesmith@mycompany.com,jodiefisher@mycompany.com</example>
		/// <example>NewTeamRule "C:\My Secured Folder" alicesmith@mycompany.com,jodiefisher@mycompany.com,robertsmith@mycompany.com</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 2)
					throw new ArgumentException("Missing parameters");

				string vboxPath = args[0];
				string sizeInMegabytes = args[1];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					string folderPath = args[0];
					string coworkersEmailAddresses = args[1];

					object[] certificates = api.Execute(string.Format("Get-SDSCertificate -EmailAddress {0}", coworkersEmailAddresses));
					if (certificates == null || certificates.Length != coworkersEmailAddresses.Split(new char[] { ',' }).Length)
						throw new InvalidOperationException("One or more certificates not found");

					KeyValuePair<string, Object>[] parameters = new KeyValuePair<string, Object>[] 
					{
						new KeyValuePair<string, Object>("Path", folderPath),
						new KeyValuePair<string, Object>("Coworkers", certificates)
					};
					objects = api.Execute("New-SDSTeamRule", parameters);
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("New-SDSTeamRule");

					RuleInfoData ruleInfoData = objects[0] as RuleInfoData;
					Console.WriteLine(string.Format("Return:\n{0}", ruleInfoData.FullName));
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
