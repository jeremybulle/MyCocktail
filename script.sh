function test(){
    mkdir TestCp
    for path in $@
    do
        id=$"${path#*TestResults/}"
        filname="$id".coverage.cobertura.xml
        echo $filname
        cp $path/coverage.cobertura.xml ./TestCp/$filname
    done
}

test $(find . -name 'coverage.cobertura.xml' -printf '%h\n')