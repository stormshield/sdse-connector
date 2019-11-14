using System;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class LockUser
	{
		/// <summary>
		/// This example demonstrates the use of the Lock-SDSUser API
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
						throw new InvalidOperationException("No user connected");
					
					objects = api.Execute("Lock-SDSUser");
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
