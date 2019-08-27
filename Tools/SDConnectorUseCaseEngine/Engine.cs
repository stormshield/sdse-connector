using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;

using Stormshield.DataSecurity.Connector.Common;
using Stormshield.DataSecurity.Connector.Kernel;
using Stormshield.DataSecurity.Connector.AddressBook;
using Stormshield.DataSecurity.Connector.File;
using Microsoft.Win32;

namespace SDConnectorUseCaseEngine
{
	public class Engine
	{
		public Engine()
		{
		}

		public bool UserIsConnected()
		{
			bool userIsConnected = false;

			using (Stormshield.DataSecurity.Connector.API api = new Stormshield.DataSecurity.Connector.API())
			{
				object[] objects = api.Execute("Get-SDSUser");
				if (objects != null)
				{
					User user = objects[0] as User;
					userIsConnected = !user.Locked;
				}
			}

			return userIsConnected;
		}

		public string[] EncryptFilesWithStormshieldDataFile(string[] filePaths, string[] emailAddresses)
		{
			List<string> encryptedFilePaths = new List<string>();

			string recipients = CSharpArrayToCmdletList(emailAddresses);

			using (Stormshield.DataSecurity.Connector.API api = new Stormshield.DataSecurity.Connector.API())
			{
				object[] objects = api.Execute(string.Format("Get-SDSCertificate -EmailAddress {0}", recipients));
				if (objects == null || objects.Length != emailAddresses.Length)
				{
					throw new InvalidOperationException("Certains certificats ne sont pas trouvés");
				}
				else
				{
					KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("-Path", filePaths),
						new KeyValuePair<string, object>("-Coworkers", objects)
					};
					objects = api.Execute("Protect-SDSFile", parameters);
					if (objects != null)
					{
						foreach (object item in objects)
						{
							SecureFile secureFile = item as SecureFile;
							encryptedFilePaths.Add(secureFile.Path);
						}
					}
				}
			}

			return encryptedFilePaths.ToArray();
		}

		public string[] EncryptFileWithStormshieldDataFile(string filePath)
		{
			List<string> encryptedFilePaths = new List<string>();

			using (Stormshield.DataSecurity.Connector.API api = new Stormshield.DataSecurity.Connector.API())
			{
				object[] objects = api.Execute(string.Format("Protect-SDSFile '{0}'", filePath));
				if (objects != null)
				{
					foreach (object item in objects)
					{
						SecureFile secureFile = item as SecureFile;
						encryptedFilePaths.Add(secureFile.Path);
					}
				}
			}

			return encryptedFilePaths.ToArray();
		}

		public void CreateNewMailWithEncryptedAttachments(string[] recipientsEmailAddresses, string[] attachedFiles)
		{
			Outlook.Application _application = null;
			Outlook.MailItem mailItem = null;

			try
			{
				_application = new Outlook.Application();

				mailItem = (Outlook.MailItem)_application.CreateItem(Outlook.OlItemType.olMailItem);

				mailItem.To = GetRecipientString(recipientsEmailAddresses);

				string[] encryptedPaths = EncryptFilesWithStormshieldDataFile(attachedFiles, recipientsEmailAddresses);
				foreach (string encryptedPath in encryptedPaths)
					mailItem.Attachments.Add(encryptedPath, Outlook.OlAttachmentType.olByValue);

				mailItem.Display(true);
			}
			finally
			{
				if (mailItem != null)
					Marshal.ReleaseComObject(mailItem);
				if (_application != null)
					Marshal.ReleaseComObject(_application);
			}
		}
		private static bool KeyExists(RegistryHive hKey, string keyPath, RegistryView registryView)
		{
			bool exists = false;
			using (RegistryKey baseKey = RegistryKey.OpenBaseKey(hKey, registryView))
			{
				using (RegistryKey subKey = baseKey.OpenSubKey(keyPath))
				{
					exists = (subKey != null ? true : false);
				}
			}
			return exists;
		}

		public void SetEnableScripting(bool enable)
		{
			string keyPath = @"SOFTWARE\ARKOON\Security BOX Enterprise\Properties\Mail";
			RegistryView registryView = RegistryView.Default;

			// On essaye d'abord dans la partie de la base de registres native au binaire
			// (avec un binaire 32 bits sur un OS 64 bits, ce sera la branche Wow6432Node).
			// Si on ne trouve pas, on recommence la recherche dans la partie 64 bits.
			// (sur un OS 32 bits, cela revient à utiliser Registry32, ce qui fait qu'on fait deux fois la même
			// opération... tant pis, on ne va pas tester la plateforme de l'OS ici).
			bool exists = KeyExists(RegistryHive.LocalMachine, keyPath, registryView);
			if (!exists)
			{
				registryView = RegistryView.Registry64;
				exists = KeyExists(RegistryHive.LocalMachine, keyPath, RegistryView.Registry64);
			}

			// si on a trouvé la cle on peut modifier sa valeur
			if (exists)
			{
				using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
				{
					using (RegistryKey subKey = baseKey.OpenSubKey(keyPath, true))
					{
						subKey.SetValue("EnableScripting", (enable ? 1 : 0));
					}
				}
			}
			else
			{
				throw new InvalidOperationException("EnableScripting resigtry value not found");
			}
		}
		public void SendMail(string[] recipients, string subject, string content, bool encrypt, bool sign)
		{
			Outlook.Application _application = null;
			Outlook.MailItem mailItem = null;

			try
			{
				SetEnableScripting(true);

				_application = new Outlook.Application();

				mailItem = (Outlook.MailItem)_application.CreateItem(Outlook.OlItemType.olMailItem);

				mailItem.To = GetRecipientString(recipients);
				mailItem.Subject = subject;
				mailItem.Body = content;
				mailItem.BodyFormat = Outlook.OlBodyFormat.olFormatPlain;

				if (encrypt)
					EncryptMail(mailItem);
				if (sign)
					SignMail(mailItem);

				((Outlook._MailItem)mailItem).Send();
			}
			finally
			{
				if (mailItem != null)
					Marshal.ReleaseComObject(mailItem);
				if (_application != null)
					Marshal.ReleaseComObject(_application);

				SetEnableScripting(false);
			}
		}

		public X509Certificate GetCertificate(string emailAddress)
		{
			X509Certificate certificate = null;

			using (Stormshield.DataSecurity.Connector.API api = new Stormshield.DataSecurity.Connector.API())
			{
				object[] objects = api.Execute(string.Format("Get-SDSCertificate -EmailAddress {0}", emailAddress));
				if (objects != null)
				{
					certificate = objects[0] as X509Certificate;
				}
			}

			return certificate;
		}

		public X509Certificate[] GetCertificates(string[] emailAddresses)
		{
			X509Certificate[] certificates = null;

			using (Stormshield.DataSecurity.Connector.API api = new Stormshield.DataSecurity.Connector.API())
			{
				string emailAddressArg = CSharpArrayToCmdletList(emailAddresses);
				object[] objects = api.Execute(string.Format("Get-SDSCertificate -EmailAddress {0}", emailAddressArg));
				if (objects != null)
				{
					certificates = Array.ConvertAll<object, X509Certificate>(objects, o => o as X509Certificate);
				}
			}

			return certificates;
		}

		/// <summary>
		/// Création d'une règle Team sur un dossier
		/// </summary>
		/// <param name="folderPath">Chemin complet du dossier</param>
		/// <param name="certificates">Liste des adresses email des collaborateurs</param>
		/// <remarks>
		/// L'utilisateur connecté à SDS est ajouté automatiquement dans la règle en tant que propriétaire.
		/// Les autres utilisateurs (paramètre 'certificates') sont ajoutés comme simples collaborateurs.
		/// </remarks>
		public void CreateTeamRule(string folderPath, X509Certificate[] certificates)
		{
			using (Stormshield.DataSecurity.Connector.API api = new Stormshield.DataSecurity.Connector.API())
			{
				KeyValuePair<string, object>[] parameters = new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("-Path", folderPath),
					new KeyValuePair<string, object>("-Coworkers", certificates)
				};
				object[] objects = api.Execute("New-SDSTeamRule", parameters);
				if (objects == null || objects.Length == 0)
					throw new InvalidOperationException();
			}
		}

		#region Private

		private string GetRecipientString(string[] emailAddresses)
		{
			StringBuilder sbRecipients = new StringBuilder();
			foreach (string emailAddress in emailAddresses)
				sbRecipients.AppendFormat("{0}; ", emailAddress);
			return sbRecipients.ToString();
		}

		private static void SignMail(Outlook.MailItem mail)
		{
			Outlook.UserProperties userProperties = null;
			Outlook.UserProperty userProperty = null;
			try
			{
				userProperties = mail.UserProperties;
				userProperty = userProperties.Add("SDSSign", Outlook.OlUserPropertyType.olYesNo, false, 1);
				userProperty.Value = true;
			}
			finally
			{
				if (userProperty != null)
					Marshal.ReleaseComObject(userProperty);
				if (userProperties != null)
					Marshal.ReleaseComObject(userProperties);
			}
		}

		private static void EncryptMail(Outlook.MailItem mail)
		{
			Outlook.UserProperties userProperties = null;
			Outlook.UserProperty userProperty = null;
			try
			{
				userProperties = mail.UserProperties;
				userProperty = userProperties.Add("SDSEncrypt", Outlook.OlUserPropertyType.olYesNo, false, 1);
				userProperty.Value = true;
			}
			finally
			{
				if (userProperty != null)
					Marshal.ReleaseComObject(userProperty);
				if (userProperties != null)
					Marshal.ReleaseComObject(userProperties);
			}
		}

		private string CSharpListToCmdletList(List<string> csharpList)
		{
			return CSharpArrayToCmdletList(csharpList.ToArray());
		}

		private string CSharpArrayToCmdletList(string[] csharpArray)
		{
			StringBuilder sb = new StringBuilder();
			foreach (string element in csharpArray)
				sb.AppendFormat("{0},", element);
			string cmdletList = sb.ToString().TrimEnd(new char[] { ',' });
			return cmdletList;
		}

		#endregion Private
	}
}
