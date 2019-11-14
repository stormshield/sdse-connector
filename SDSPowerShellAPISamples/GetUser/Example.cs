using System;

using Stormshield.DataSecurity.Connector.Kernel;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class GetUser
	{
		/// <summary>
		/// This example demonstrates the use of the Get-SDSUser API
		/// </summary>
		static int Main()
		{
			int returnCode = 0;

			try
			{
				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
					{
						Console.WriteLine("No user connected");
					}
					else if (objects.Length != 1)
					{
						throw new InvalidOperationException("Get-SDSUser");
					}
					else
					{
						User user = objects[0] as User;
						Console.WriteLine(string.Format("Return:\n{0}", user.Name));
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
