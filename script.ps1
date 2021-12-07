function MoveCoverageFiles{
    param ( [string[]]$FullPaths )

    foreach ($Parameter in $ParameterName) {
        id=$"${path#*TestResults/}"
        filname="$id".coverage.cobertura.xml
        Write-Output $filname
    }
}

$toto= Get-ChildItem -Path *\TestResults\*\coverage.cobertura.xml -Recurse

foreach ($val in $toto) {
    $val | Select-Object -Property *
    echo $val.FullName
}