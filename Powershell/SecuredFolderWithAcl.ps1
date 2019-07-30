
$securedFolder = 'C:\Users\jocelyn.krystlik\Downloads\Difenso'
[Reflection.Assembly]::LoadWithPartialName('System.Windows.Forms')

try
{
    try
    {
	    Connect-SDSUser -Interactive
    }
    catch
    {
        #Already connected
    }
	
	$report = ''
	
	If (-not (Test-Path -Path "$securedFolder"))
	{
		New-Item -Path "$securedFolder" -Type Directory | Out-Null
		$report += ("Folder '$securedFolder' has been created." + [Environment]::NewLine)
	}

    $ACLs = Get-Acl "$securedFolder" | ForEach-Object { $_.Access }
    $emails = ""

    Foreach ($ACL in $ACLs)
    {
        try
        {
            $option = [System.StringSplitOptions]::RemoveEmptyEntries
            $accountName = $ACL.IdentityReference.ToString().split("\", $option)
            If ($accountName.Count -eq 1)
            {
                $accountName = $accountName[0]
            }
            Else 
            {
                $accountName = $accountName[1]
            }

            $searcher = [adsisearcher]"(samaccountname=$accountName)"
            $email = $searcher.FindOne().Properties.mail

            If ($email.Length -gt 0)
            {
                $emails += "," + $email
            }
            Else
            {
				#Local group
				$members = Get-LocalGroupMember -Group $accountName
				
				If ($members.Length -gt 0)
				{
					$members = $members.Name
					
					Foreach ($member in $members)
					{
						$accountName = $member.ToString().split("\")
						If ($accountName.Count -eq 1)
						{
							$accountName = $accountName[0]
						}
						Else 
						{
							$accountName = $accountName[1]
						}
						$searcher = [adsisearcher]"(samaccountname=$accountName)"
						$email = $searcher.FindOne().Properties.mail
						
						If ($email.Length -gt 0)
						{
							$emails += "," + $email
						}
						Else
						{
							#error
						}
					}
				}
				Else
				{
					#AD group
					$members = Get-ADGroupMember -Identity $accountName -Recursive | Get-ADUser -Properties Mail | Select-Object Mail | ForEach-Object { $_.mail }
                    $emails += "," + [system.String]::Join(",", $members)
				}
            }
        }
        catch
        {
            #Error
        }
    }

	try
	{
        $option = [System.StringSplitOptions]::RemoveEmptyEntries
        $emails = $emails.Split(",", $option)
        $certs = Get-SDSCertificate -EmailAddress $emails
		$rule = Get-SDSTeamRule -Path "$securedFolder"
	}
	catch [Stormshield.DataSecurity.Connector.Team.RuleNeedUpdateException]
	{
	}
	if ($rule -ne $null -and $rule.Secured -eq $false)
	{
        #Foreach ($cert in $certs)
        #{
            #Echo $cert
            $teamRule = New-SDSTeamRule -Path "$securedFolder" -Coworkers $certs
            #Remove-SDSTeamRule -Path "$securedFolder"
        #}
		$report += ("Rule has been created on folder '$securedFolder'." + [Environment]::NewLine)
	}
	
	Protect-SDSTeam -Path "$securedFolder"
	$report += ("Folder '$securedFolder' has been protected." + [Environment]::NewLine)
	
	[Windows.Forms.MessageBox]::Show($report)
}
catch
{
	[Windows.Forms.MessageBox]::Show($_.Exception)
}
