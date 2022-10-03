param ($AppPoolName)

Write-Host "Checking status of $($AppPoolName) app pool"
$AppPoolState = Get-WebAppPoolState $AppPoolName

if ($AppPoolState.Value -eq 'Stopped'){
	Write-Host "$($AppPoolName) app pool not running"
}
else {
	Write-Host "Stopping $($AppPoolName) app pool"
	Stop-WebAppPool $AppPoolName
	Write-Host "$($AppPoolName) app pool stopped"

	Write-Host "Sleep for 3 seconds to allow app pool to stop"
	Start-Sleep -Seconds 3
	Write-Host "End sleep"
}