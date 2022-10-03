param ($AppFileLocation, $ArchiveLocation)

Write-Host 'Archiving current application files'
$CurrentTime = Get-Date -Format "yyyy-MM-dd-HHmmss"
$ArchiveFilePath = "$($ArchiveLocation)\applications-$($CurrentTime).zip"
Compress-Archive -Path "$($AppFileLocation)\*" -DestinationPath $ArchiveFilePath
Write-Host "Application files archived to $($ArchiveFilePath)"