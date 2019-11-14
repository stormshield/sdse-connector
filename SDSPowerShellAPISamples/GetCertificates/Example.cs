using System;
using System.Text;

using Stormshield.DataSecurity.Connector.Common;

namespace Stormshield.DataSecurity.Connector.Samples
{
	class GetCertificates
	{
		/// <summary>
		/// This example demonstrates the use of the Get-SDSCertificate API
		/// </summary>
		/// <example>GetCertificates alicesmith@mycompany.com</example>
		/// <example>GetCertificates alicesmith@mycompany.com jodiefisher@mycompany</example>
		/// <example>GetCertificates alicesmith@mycompany.com jodiefisher@mycompany robertmiller@mycompany.com</example>
		static int Main(string[] args)
		{
			int returnCode = 0;

			try
			{
				if (args.Length == 0)
					throw new ArgumentException("Missing parameters");
				
				using (API api = new API())
				{
					object[] objects = api.Execute("Get-SDSUser");
					if (objects == null)
						throw new InvalidOperationException("No user connected");

					StringBuilder emailAddresses = new StringBuilder();
					foreach (string arg in args)
						emailAddresses.AppendFormat("{0},", arg);

					objects = api.Execute(string.Format("Get-SDSCertificate -EmailAddress {0}", emailAddresses.ToString().TrimEnd(new char[] { ',' })));
					if (objects == null || objects.Length != emailAddresses.ToString().Split(new char[] { ',' }).Length)
						throw new InvalidOperationException("One or more certificates not found");
					
					foreach (object o in objects)
					{
						X509Certificate certificate = o as X509Certificate;
						Console.WriteLine(string.Format("Return:\n{0}", certificate.Subject));
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
