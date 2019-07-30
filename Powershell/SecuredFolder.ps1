$securedFolder = Join-Path ([Environment]::GetFolderPath('Desktop')) 'Secured'

[Reflection.Assembly]::LoadWithPartialName('System.Windows.Forms')

try
{
	Connect-SDSUser -Interactive
	
	$report = ''
	
	if (-not (Test-Path -Path "$securedFolder"))
	{
		New-Item -Path "$securedFolder" -Type Directory | Out-Null
		$report += ("Folder '$securedFolder' has been created." + [Environment]::NewLine)
	}
	
	try
	{
		$rule = Get-SDSTeamRule -Path "$securedFolder"
	}
	catch [Stormshield.DataSecurity.Connector.Team.RuleNeedUpdateException]
	{
	}
	if ($rule -ne $null -and $rule.Secured -eq $false)
	{
		New-SDSTeamRule -Path "$securedFolder"
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
