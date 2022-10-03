param ($ServiceName)

Write-Host "Checking status of $($ServiceName) service"
$Service = Get-Service $ServiceName
if ($Service.Status -eq 'Stopped') {
    Write-Host "$($ServiceName) service not running"
}
else {
    Write-Host "Stopping $($ServiceName) service"
    Stop-Service $ServiceName
    Write-Host "$($ServiceName) service stopped"
}