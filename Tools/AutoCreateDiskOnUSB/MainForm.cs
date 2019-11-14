//
// Copyright (c) Stormshield 2017
// This sample code is provided "as is", without support and warranty of any kind.
// Use at your own risk.
//

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Text;

// Here, we declare that we are using Connector APIs.
using Stormshield.DataSecurity.Connector;
using Stormshield.DataSecurity.Connector.VirtualDisk;

/// <summary>
/// This sample program demonstrates how Connector APIs can be used to create
/// a Virtual Disk volume when a new USB key or external drive is plugged to the machine.
/// </summary>
/// <see cref="http://www.codeguru.com/columns/dotnet/detecting-usb-devices-using-c.html"/>
namespace SDConnectorAutoCreateDiskOnUSB
{
    public partial class MainForm : Form
    {
		#region Win32 declarations

		/// <summary>
		/// Declarations needed for WndProc override.
		/// </summary>
		/// <seealso cref="https://msdn.microsoft.com/fr-fr/library/windows/desktop/aa363480.aspx"/>

		private const int WM_DEVICECHANGE = 0x219;

		private const int DBT_DEVICEARRIVAL = 0x8000;
		private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
		private const int DBT_DEVTYP_VOLUME = 0x00000002;

		/// <summary>
		/// Windows procedure override to intercepts the "device changed" event.
		/// </summary>
		/// <seealso cref="https://msdn.microsoft.com/fr-fr/library/system.windows.forms.control.wndproc.aspx"/>
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			base.WndProc(ref m);

			switch (m.Msg)
			{
				case WM_DEVICECHANGE:
					switch ((int)m.WParam)
					{
						// A new device has been plugged into the machine.
						case DBT_DEVICEARRIVAL:
							// https://msdn.microsoft.com/en-us/library/aa363246.aspx
							int devType = Marshal.ReadInt32(m.LParam, 4);
							if (devType == DBT_DEVTYP_VOLUME)
							{
								DevBroadcastVolume volume = (DevBroadcastVolume)Marshal.PtrToStructure(m.LParam, typeof(DevBroadcastVolume));
								CreateAndMountDiskVolumeIfNecessary(volume.DriverLetter);
							}
							break;
						
						// A device has been removed. Nothing particular to do for this sample, just log the operation.
						case DBT_DEVICEREMOVECOMPLETE:
							AddLog("Device removed");
							break;
					}
					break;
			}
		}

		#endregion

		public MainForm()
        {
            InitializeComponent();
        }

		/// <summary>
		/// This function will be called throughout the code to add a line to logs displayed on the user interface.
		/// </summary>
		/// <param name="line"></param>
		private void AddLog(string line)
		{
			StringBuilder str = new StringBuilder();
			str.Append(line);
			str.Append(Environment.NewLine);

			textBox1.Text += str.ToString();
			textBox1.SelectionStart = textBox1.Text.Length - 1;
			textBox1.SelectionLength = 0;
		}

		/// <summary>
		/// Name of the Virtual Disk file that is created on the plugged device.
		/// </summary>
		/// <remarks>If this file already exists on the device, the Virtual Disk is just mounted to be used.</remarks>
		private string vboxFileName = "AutoCreate.vbox";

		/// <summary>
		/// Size of the Virtual Disk that is created on the plugged device.
		/// It represents a percentage of the available space on the device.
		/// </summary>
		private double vboxSizePercent = 0.001;

		/// <summary>
		/// This is the main function that creates the Virtual Disk on the device that is available behind a drive letter (in Windows Explorer).
		/// This is the only function that is using Connector APIs.
		/// </summary>
		/// <remarks>This function does not create a Virtual Disk if the device is not ready or if the VBOX file already exists.</remarks>
		/// <param name="driveLetter">Drive letter to access to the device (in Windows Explorer).</param>
		private void CreateAndMountDiskVolumeIfNecessary(char driveLetter)
		{
			try
			{
				// Inside this "using" block, we are going to use some Connector APIs.
				using (API api = new API())
				{
					// First of all, test if a user is connected to SDS (otherwise Virtual Disk can not be created nor mounted).
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
					{
						AddLog("No user connected to SDS");
					}
					else
					{
						// Here we are sure that a user is connected to its SDS account.

						// First we need to check is the drive is ready (within the meaning of Windows).
						DriveInfo driveInfo = new DriveInfo(driveLetter.ToString());
						if (driveInfo.IsReady)
						{
							// We also need to check that the drive does not represents a Virtual Disk
							// (otherwise, we would be creating Virtual Disk indefinitely).
							bool isVirtualDisk = true;
							try
							{
								// To check that the drive is not a Virtual Disk, we can call the Get-SDSDisk API and wait for an exception.
								objects = api.Execute(string.Format("Get-SDSDisk {0}:", driveLetter));
							}
							catch
							{
								isVirtualDisk = false;
							}

							if (!isVirtualDisk)
							{
								// Here we are sure that we are working on a drive that is not a Virtual Disk.

								AddLog(string.Format("New device '{0}:\\'", driveLetter));

								string vboxPath = Path.Combine(driveLetter + ":\\", vboxFileName);

								// If the VBOX file already exists, there is no need to create it again.
								bool createDisk = !File.Exists(vboxPath);
								if (createDisk)
								{
									int vboxSize = (int)((double)(driveInfo.AvailableFreeSpace / 1024 / 1024) * vboxSizePercent);

									AddLog(string.Format("Creating virtual disk ('{0} - {1}Mb')...", vboxPath, vboxSize));
									objects = api.Execute(string.Format("New-SDSDisk '{0}' -Size {1}", vboxPath, vboxSize));
									if (objects == null || objects.Length != 1)
										throw new InvalidOperationException("New-SDSDisk error");
								}

								// Now we mount the Virtual Disk to make it available to the user.
								AddLog("Mounting virtual disk...");
								objects = api.Execute(string.Format("Mount-SDSDisk '{0}'", vboxPath));
								if (objects == null || objects.Length != 1)
									throw new InvalidOperationException("Mount-SDSDisk error");

								Volume volume = objects[0] as Volume;

								// If the Virtual Disk has just been created, it needs to be formatted.
								if (createDisk)
								{
									AddLog(string.Format("Formating virtual disk '{0}:\\'...", volume.MountLetter));
									Format(volume.MountLetter.Letter);
								}

								AddLog(string.Format("Done. Virtual disk accessible in '{0}:\\'", volume.MountLetter));
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				textBox1.Text += ex.ToString() + Environment.NewLine;
			}
		}

		/// <summary>
		/// This function is used to format a drive in NTFS mode.
		/// </summary>
		/// <param name="driveLetter">Drive letter that represents the drive to be formatted (in Windows Explorer).</param>
		private void Format(char driveLetter)
		{
			// Here we are just writing a simple batch command in a temporary BAT file that will be executed.

			string batchPath = Path.GetTempFileName() + ".bat";
			try
			{
				File.WriteAllText(batchPath, string.Format("format {0}: /FS:NTFS /q /y", driveLetter));

				Process process = new Process();

				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.FileName = batchPath;
				process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

				process.Start();

				// BAT execution output is redirected to logs in the user interface.
				while (!process.StandardOutput.EndOfStream)
				{
					AddLog(process.StandardOutput.ReadLine());
				}

				process.WaitForExit();
			}
			finally
			{
				File.Delete(batchPath);
			}
		}
	}
}
