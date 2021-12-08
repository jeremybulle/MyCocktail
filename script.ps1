function MoveCoverageFiles{
    New-Item -Path "./" -Name "CoverageFiles" -ItemType "directory"
    $CoverageFiles = Get-ChildItem -Path *\TestResults\*\coverage.cobertura.xml -Recurse
    foreach ($file in $CoverageFiles) {
        #extract id
        $path = Split-Path -Path $file -Parent
        $path = $path.Split("\")
        $id = $path[$path.Count -1]

        #move file
        Move-Item -Path $testFile.FullName -Destination ./CoverageFiles
        
        #retrieve file moved
        $movedFile = Get-Item ./CoverageFiles\coverage.cobertura.xml

        #rename file
        Rename-Item $movedFile -NewName "$($id).$($movedFile.Name)"
    }
}

# $toto= Get-ChildItem -Path *\TestResults\*\coverage.cobertura.xml -Recurse

# foreach ($val in $toto) {
#     $val | Select-Object -Property *
#     echo $val.FullName
#     Write-Output  $val | Format-List -Property *
# }

# foreach ($val in $toto) {
#     $path = Split-Path -Path $val -Parent
#     $path = $path.Split("\")
#     echo $path[$path.Count -1]
# }

# New-Item -Path "./" -Name "TestDir" -ItemType "directory" -Force
# $testFile = Get-Item ./Test.txt
# Move-Item -Path $testFile.FullName -Destination ./TestDir
# $testFile = Get-Item ./TestDir\Test.txt
# Rename-Item $testFile -NewName "$($testFile.BaseName)-Renamed.txt"

# - name : run tests and regroup coverage files
#         shell: powershell
#         run: |
#           dotnet test --collect:"XPlat Code Coverage" -p:CollectCoverage=true -p:CoverletOutputFormat=opencover

#           $CoverageFiles = Get-ChildItem -Path *\TestResults\*\coverage.cobertura.xml -Recurse

#           foreach ($file in $CoverageFiles) {
#             #extract id
#             $path = Split-Path -Path $file -Parent
#             $path = $path.Split("\")
#             $id = $path[$path.Count -1]

#             #move file
#             Move-Item -Path $file.FullName -Destination ./CoverageFiles
            
#             #retrieve file moved
#             $movedFile = Get-Item ./CoverageFiles\coverage.cobertura.xml

#             #rename file
#             Rename-Item $movedFile -NewName "$($id).$($movedFile.Name)"
#           }