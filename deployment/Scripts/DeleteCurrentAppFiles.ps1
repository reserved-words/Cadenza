param ($AppFileLocation)

Write-Host 'Deleting current application files'
Remove-Item "$($AppFileLocation)\*" -Recurse
Write-Host 'Current application files deleted'