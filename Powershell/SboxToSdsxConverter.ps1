try
{
	Connect-SDSUser -Interactive
}
catch
{
	#Already connected
}

try
{
	if ( $args.count -eq 0)
	{
		$current_dir = Get-Location 
		write-host 'Convert all sbox in current directory'
		$files_list = Get-ChildItem -Path "$current_dir\*" -Include "*.sbox" -Recurse
	}
	else 
	{
		write-host "Convert " + $($args.count) + " file(s) passed in arguments"
		$files_list = $args
	}

	for ( $i = 0; $i -lt $files_list.count; $i++ ) 
	{
		write-host "Converting: $($files_list[$i].Name)"
		$sbox_file = Get-SDSFile -Path $files_list[$i]
		$certs = $sbox_file.Certificates
		$clear_file = Unprotect-SDSFile -Path $files_list[$i]
		Protect-SDSFile -Path $clear_file.Path -Coworkers $certs | out-null
	}
}
catch
{
	write-host $_.Exception
}