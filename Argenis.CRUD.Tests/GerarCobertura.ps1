#Requisitos: 
#Instalar Coverlet --> dotnet tool install --global coverlet.console
#Instalar pacote nuget coverlet.msbuild no projeto
#Instalar ReportGenerator --> dotnet tool install -g dotnet-reportgenerator-globaltool

$Dir = Get-ChildItem
$ProjectName = $Dir | where {$_.extension -eq '.csproj'}
dotnet test $ProjectName.Name /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
Remove-Item -Path CoverageReport -Recurse
mkdir CoverageReport
reportgenerator -reports:"coverage.opencover.xml" -targetdir:"CoverageReport" -sourcedir:".."
Start-Process .\CoverageReport\index.htm
#Start-Sleep 10000
