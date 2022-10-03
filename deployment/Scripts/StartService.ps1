param ($ServiceName)

Write-Host "Starting $($ServiceName) service"
$Service = Get-Service $ServiceName
Start-Service $Service
Write-Host "$($ServiceName) service started"