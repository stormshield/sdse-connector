using System;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class DisconnectUser
	{
		/// <summary>
		/// This example demonstrates the use of the Disconnect-SDSUser API
		/// </summary>
		static int Main()
		{
			int returnCode = 0;

			try
			{
				using (API api = new API())
				{
					object[] objects = api.Execute("Disconnect-SDSUser");
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
