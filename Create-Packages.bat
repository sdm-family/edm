@echo off

mkdir nuget-packages

cd Edm
nuget pack Edm.fsproj -Build -Symbols
copy /Y *.nupkg ..\nuget-packages\

cd ..\Edm.Writer.EPPlus
nuget restore -PackagesDirectory ..\packages
nuget pack Edm.Writer.EPPlus.fsproj -Build -Symbols
copy /Y *.nupkg ..\nuget-packages\

cd ..
