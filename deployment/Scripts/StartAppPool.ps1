param ($AppPoolName)

Write-Host "Starting $($AppPoolName) app pool"
Start-WebAppPool $AppPoolName
Write-Host "$($AppPoolName) app pool started"