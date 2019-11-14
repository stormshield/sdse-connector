using System;

using Stormshield.DataSecurity.Connector.VirtualDisk;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class NewDisk
	{
		/// <summary>
		/// This example demonstrates the use of the New-SDSDisk API
		/// </summary>
		/// <remarks>The created Virtual Disk volume is not mounted nor formatted in this example</remarks>
		/// <example>NewDisk "C:\My Folder\disk.vbox" 50</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length != 2)
					throw new ArgumentException("Missing parameters");

				string vboxPath = args[0];
				string sizeInMegabytes = args[1];

				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					objects = api.Execute(string.Format("New-SDSDisk '{0}' -Size {1}", vboxPath, sizeInMegabytes));
					if (objects == null || objects.Length != 1)
						throw new InvalidOperationException("New-SDSDisk");

					Volume volume = objects[0] as Volume;
					Console.WriteLine(string.Format("Return:\n{0}\n{1}", volume.FullName, volume.Size));
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
