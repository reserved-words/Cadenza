param ($DropFileLocation, $DropFilePattern)

Write-Host "Deleting drop files from $($DropFileLocation)"
Remove-Item -Path "$($DropFileLocation)\*" -Include $DropFilePattern
Write-Host 'Drop files deleted'