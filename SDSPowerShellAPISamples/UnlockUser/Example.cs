using System;

using Stormshield.DataSecurity.Connector.Kernel;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class UnlockUser
	{
		/// <summary>
		/// This example demonstrates the use of the Unlock-SDSUser API
		/// </summary>
		/// <example>UnlockUser p4ssw0rd</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					string password = string.Empty;
					if (args.Length > 0)
						password = args[0];

					objects = api.Execute(string.Format("Unlock-SDSUser {0}", password));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Unlock-SDSUser");

					User user = objects[0] as User;
					Console.WriteLine(string.Format("User connected:\n{0}", user.Name));
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
