using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Configuration;

using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Net.NetworkInformation;
using System.Management;
using System.Security.AccessControl;
using System.DirectoryServices.ActiveDirectory;

namespace SDConnectorUseCaseTeamRuleCreator
{
	class Engine
	{
		private PrincipalContext _domainGroupContext = null;

		private Dictionary<string, string> _domainUsers = new Dictionary<string, string>();
		private Dictionary<string, GroupPrincipal> _domainGroups = new Dictionary<string, GroupPrincipal>();
		private Dictionary<string, string> _localUsers = new Dictionary<string, string>();

		private List<string> _groupExceptions = new List<string>();

		public BackgroundWorker Worker { get; set; }

		public void Initialize()
		{
			this.Worker = null;
			string[] exceptions = ConfigurationManager.AppSettings["groupException"].Split('|');
			foreach (string exception in exceptions)
				_groupExceptions.Add(exception.ToLower());
		}

		public void Terminate()
		{
			if (_domainGroupContext != null)
				_domainGroupContext.Dispose();
		}

		private void FetchDomainUsers()
		{
			if (this.Worker != null)
				this.Worker.ReportProgress(0, "Fetching domain users");

			string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
			using (PrincipalContext context = new PrincipalContext(ContextType.Domain, domainName))
			{
				using (PrincipalSearcher searcher = new PrincipalSearcher(new UserPrincipal(context)))
				{
					foreach (var result in searcher.FindAll())
					{
						DirectoryEntry directoryEntry = result.GetUnderlyingObject() as DirectoryEntry;
						string userName = directoryEntry.Properties["samAccountName"].Value.ToString();
						string mail = null;
						if (directoryEntry.Properties["mail"].Count > 0)
						{
							mail = directoryEntry.Properties["mail"].Value.ToString();
							_domainUsers.Add(userName.ToLower(), mail.ToLower());
						}
					}
				}
			}
		}

		private void FetchDomainGroups()
		{
			if (this.Worker != null)
				this.Worker.ReportProgress(0, "Fetching groups");

			string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
			_domainGroupContext = new PrincipalContext(ContextType.Domain, domainName);

			using (PrincipalSearcher searcher = new PrincipalSearcher(new GroupPrincipal(_domainGroupContext)))
			{
				foreach (GroupPrincipal result in searcher.FindAll())
				{
					_domainGroups.Add(result.SamAccountName.ToLower(), result);
				}
			}
		}

		private void FetchLocalUsers()
		{
			if (this.Worker != null)
				this.Worker.ReportProgress(0, "Fetching local users");

			string machineName = Environment.MachineName;
			SelectQuery selectQuery = new SelectQuery("Win32_UserAccount", string.Format("Domain='{0}'", machineName));
			ManagementObjectSearcher managementSearcher = new ManagementObjectSearcher(selectQuery);
			foreach (ManagementObject managementObject in managementSearcher.Get())
			{
				string name = managementObject["Name"] as string;
				string description = (managementObject["Description"] as string).ToLower();
				int mailStartIndex = description.IndexOf("(mail=");
				if (mailStartIndex != -1)
				{
					int mailEndIndex = description.IndexOf(")", mailStartIndex);
					if (mailEndIndex != -1)
					{
						mailStartIndex += 6;
						string mail = description.Substring(mailStartIndex, mailEndIndex - mailStartIndex);
						_localUsers.Add(name.ToLower(), mail);
					}
				}
			}
		}

		private void ExpandGroupNames(GroupPrincipal group, ref List<string> parentGroups, ref List<string> names)
		{
			if (_groupExceptions.Contains(group.SamAccountName.ToLower()))
				return;

			try
			{
				string groupName = group.SamAccountName;
				int memberCount = group.Members.Count;

				int i = 0;
				foreach (Principal member in group.Members)
				{
					if (this.Worker.CancellationPending)
						break;

					if (member.StructuralObjectClass != null)
					{
						if (member.StructuralObjectClass.CompareTo("group") == 0)
						{
							parentGroups.Add(group.SamAccountName);
							ExpandGroupNames(member as GroupPrincipal, ref parentGroups, ref names);
						}
						else
						{
							names.Add(member.SamAccountName.ToLower());
						}
					}

					if (this.Worker != null)
					{
						StringBuilder parentStr = new StringBuilder();
						foreach (string parent in parentGroups)
							parentStr.AppendFormat("{0}/", parent);
						this.Worker.ReportProgress(0, string.Format("[{0}{1}] - {2}/{3}", parentStr.ToString(), groupName, i + 1, memberCount));
					}

					i++;
				}
			}
			catch (PrincipalOperationException)
			{
			}
		}

		private List<string> GetACLNames(string folderPath)
		{
			List<string> names = new List<string>();

			DirectorySecurity security = Directory.GetAccessControl(folderPath);
			AuthorizationRuleCollection acl = security.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
			foreach (FileSystemAccessRule ace in acl)
			{
				if (this.Worker.CancellationPending)
					break;

				string name = ace.IdentityReference.Value;
				string[] fragments = name.Split(new char[] { '\\' });
				if (fragments.Length == 2)
					name = fragments[1];

				// http://www.codeproject.com/Reference/871338/AccessControl-FileSystemRights-Permissions-Table

				FileSystemRights rights = ace.FileSystemRights;
				if (rights.HasFlag(FileSystemRights.Write) ||
					rights.HasFlag(FileSystemRights.CreateFiles) ||
					rights.HasFlag(FileSystemRights.CreateDirectories) ||
					rights.HasFlag(FileSystemRights.Read) ||
					rights.HasFlag(FileSystemRights.ExecuteFile) ||
					rights.HasFlag(FileSystemRights.Traverse) ||
					rights.HasFlag(FileSystemRights.Delete))
				{
					if (this.Worker != null)
						this.Worker.ReportProgress(0, name);

					name = name.ToLower();

					if (_domainGroups.ContainsKey(name))
					{
						List<string> groupNames = new List<string>();
						List<string> parentGroups = new List<string>();
						ExpandGroupNames(_domainGroups[name], ref parentGroups, ref groupNames);
						names.AddRange(groupNames);
					}
					else
					{
						names.Add(name);
					}
				}
			}

			return names;
		}

		public Dictionary<string, string> FetchUsers(string folderPath)
		{
			Dictionary<string, string> users = new Dictionary<string, string>();

			if (_domainUsers.Count == 0)
				FetchDomainUsers();
			if (_localUsers.Count == 0)
				FetchLocalUsers();
			if (_domainGroups.Count == 0)
				FetchDomainGroups();

			List<string> names = GetACLNames(folderPath);

			foreach (string name in names)
			{
				if (this.Worker.CancellationPending)
					break;

				string email = null;

				if (_localUsers.ContainsKey(name))
					email = _localUsers[name];
				else if (_domainUsers.ContainsKey(name))
					email = _domainUsers[name];

				if (email != null && !users.ContainsKey(name))
					users.Add(name, email);
			}

			return users;
		}
	}
}
