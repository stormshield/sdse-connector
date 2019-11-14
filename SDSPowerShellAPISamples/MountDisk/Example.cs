using System;

using Stormshield.DataSecurity.Connector.VirtualDisk;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class MountDisk
	{
		/// <summary>
		/// This example demonstrates the use of the Mount-SDSDisk API
		/// </summary>
		/// <example>GetDisk "C:\My Folder\disk.vbox"</example>
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

					objects = api.Execute(string.Format("Mount-SDSDisk '{0}'", vboxPath));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("Mount-SDSDisk");

					Volume volume = objects[0] as Volume;
					Console.WriteLine(string.Format("Return:\n{0}", volume.FullName));
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
