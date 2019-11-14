using System;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class RemoveTeamRule
	{
		/// <summary>
		/// This example demonstrates the use of the Remove-SDSTeamRule API
		/// </summary>
		/// <exexample>RemoveTeamRule "C:\My Secured Folder"</exexample>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 1)
					throw new ArgumentException("Missing parameters");

				string folderPath = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Remove-SDSTeamRule '{0}'", folderPath));
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
