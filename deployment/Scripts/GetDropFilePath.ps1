param ($DropFileLocation, $DropFilePattern)

Write-Host 'Finding drop files'
$DropFiles = Get-Childitem -Path "$($DropFileLocation)\*" -Include "$($DropFilePattern).zip"

$NumberOfDropFiles = ($DropFiles | Measure).Count
if($NumberOfDropFiles -eq 0) {
    Write-Host 'No drop files ready for deployment'
    return $null
}

Write-Host 'Drop files found ready for deployment'
Write-Host 'Retrieving latest drop file'
$LatestDropFile = $DropFiles | Sort-Object -Property LastWriteTime | Select-Object -Last 1
$LatestDropFileName = $LatestDropFile.Name
$LatestDropFilePath = "$($DropFileLocation)\$($LatestDropFileName)"
Write-Host "Latest drop file is $($LatestDropFilePath)"

return $LatestDropFilePath