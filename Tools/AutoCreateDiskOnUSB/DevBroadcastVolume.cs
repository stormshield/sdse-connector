//
// Copyright (c) Stormshield 2017
// This sample code is provided "as is", without support and warranty of any kind.
// Use at your own risk.
//

using System;
using System.Runtime.InteropServices;

namespace SDConnectorAutoCreateDiskOnUSB
{
	/// <summary>
	/// This class corresponds to the DEV_BROADCAST_VOLUME in Win32 APIs.
	/// It is needed for the DBT_DEVICEARRIVAL event.
	/// </summary>
	/// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa363249.aspx"/>
	[StructLayout(LayoutKind.Sequential)]
	class DevBroadcastVolume
	{
		public int Size = 0;
		public int DeviceType = 0;
		public int Reserved = 0;
		public int UnitMask = 0;
		public Int16 Flags = 0;

		/// <summary>
		/// This property has been added.
		/// It is simply a tool to get the drive letter behind the logical volume.
		/// </summary>
		public char DriverLetter
		{
			get
			{
				string binaryMask = Convert.ToString(this.UnitMask, 2);
				int s = binaryMask.Length - binaryMask.IndexOf('1') - 1;
				char letter = (char)('A' + s);
				return letter;
			}
		}
	}
}
