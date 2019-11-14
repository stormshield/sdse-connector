using System;
using System.Collections.Generic;

using Stormshield.DataSecurity.Connector.Team;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class UnprotectTeam
	{
		/// <summary>
		/// This example demonstrates the use of the Unprotect-SDSTeam API
		/// </summary>
		/// <param name="args">args[0] = Path</param>
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

					objects = api.Execute(string.Format("Unprotect-SDSTeam '{0}' -Force", folderPath));
					// in this case, objects == null means there was no files in that folder
					if (objects != null)
					{
						foreach (object o in objects)
						{
							OperationStatus status = o as OperationStatus;
							Console.WriteLine(string.Format("Return:\n{0}: {1}", status.FileInfoData.FullName, status.Status));
						}
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
