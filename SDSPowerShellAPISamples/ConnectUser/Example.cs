using System;

using Stormshield.DataSecurity.Connector.Kernel;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class ConnectUser
	{
		/// <summary>
		/// This example demonstrates the use of the Connect-SDSUser API
		/// </summary>
		/// <remarks>If no arguments are specified, the Stormshield Data Security login dialog appears</remarks>		
		/// <example>ConnectUser</example>
		/// <example>ConnectUser alicesmith p4ssw0rd</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				using (API api = new API())
				{
					string command = "Connect-SDSUser -Interactive";
					if (args.Length > 0)
					{
						string userId = args[0];
						string password = string.Empty;
						if (args.Length >= 2)
							password = args[1];

						command = string.Format("Connect-SDSUser '{0}' {1}", userId, password);
					}

					object[] objects = api.Execute(command);
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Connect-SDSUser");

					User user = objects[0] as User;
					Console.WriteLine(string.Format("Return:\n{0}", user.Name));						
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
