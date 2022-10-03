param ($DropFilePath, $AppFileLocation, $DropFilePattern)

Expand-Archive -Path $DropFilePath -DestinationPath $AppFileLocation
Move-Item -Path "$($AppFileLocation)\$DropFilePattern\*" -Destination $AppFileLocation
Remove-Item -Path "$($AppFileLocation)\$DropFilePattern"
Write-Host "New application files extracted to $($AppFileLocation)"