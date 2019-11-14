using System;

using Stormshield.DataSecurity.Connector.VirtualDisk;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class DisableAutomaticMount
	{
		/// <summary>
		/// This example demonstrates the use of the Disable-SDSDiskAutomaticMount API
		/// </summary>
		/// <example>DisableAutomaticMount "C:\My Folder\disk.vbox"</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 1)
					throw new ArgumentException("Missing parameters");

				string vboxPath = args[0];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("Disable-SDSDiskAutomaticMount '{0}'", vboxPath));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Disable-SDSDiskAutomaticMount");

					Volume volume = objects[0] as Volume;
					Console.WriteLine(string.Format("Return:\n{0}", volume.AutomaticMount));
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
