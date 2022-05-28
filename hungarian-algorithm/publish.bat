dotnet publish .\example-generator\example-generator.csproj -r win-x64 -c Release -o publish -p:DebugType=None -p:DebugSymbols=false -p:PublishReadyToRunShowWarnings=true -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true
PAUSE
