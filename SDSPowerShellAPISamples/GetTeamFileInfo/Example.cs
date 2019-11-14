using System;

using Stormshield.DataSecurity.Connector.Team;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class GetTeamFileInfo
	{
		/// <summary>
		/// This example demonstrates the use of the Get-SDSTeamFile API
		/// </summary>
		/// <example>GetTeamFileInfo "C:\My Secured Folder\Document.docx"</example>
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

					objects = api.Execute(string.Format("Get-SDSTeamFile '{0}'", filePath));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Get-SDSTeamFile");

					FileInfoData fileInfoData = objects[0] as FileInfoData;
					Console.WriteLine(string.Format("Return:\n{0}", fileInfoData.FullName));
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
