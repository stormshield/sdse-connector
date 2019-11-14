using System;

using Stormshield.DataSecurity.Connector.Team;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class GetTeamRule
	{
		/// <summary>
		/// This example demonstrates the use of the Get-SDSTeamRule API
		/// </summary>
		/// <example>GetTeamRule "C:\My Secured Folder"</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length == 0)
					throw new ArgumentException("Missing parameters");

				string folderPath = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Get-SDSTeamRule '{0}'", folderPath));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Get-SDSTeamRule");

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
