generate-examples: build-generator-linux
	./generate-examples.sh

build-program-linux:
	dotnet publish ./program/program.csproj -r linux-x64 -c Release -o publish -p:DebugType=None -p:DebugSymbols=false -p:PublishReadyToRunShowWarnings=true -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true -p:IncludeNativeLibrariesForSelfExtract=true

build-generator-linux:
	dotnet publish ./example-generator/example-generator.csproj -r linux-x64 -c Release -o publish -p:DebugType=None -p:DebugSymbols=false -p:PublishReadyToRunShowWarnings=true -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true -p:IncludeNativeLibrariesForSelfExtract=true

.PHONY: generate-examples build-program-linux build-generator-linux
