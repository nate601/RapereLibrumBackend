makeBuilds:
	dotnet publish -c release -r win-x64
	dotnet publish -c release -r linux-x64
	dotnet publish -c release -r osx-x64
	7z a bin/releaseArchives/win-x64 bin/release/netcoreapp2.2/win-x64/*
	7z a bin/releaseArchives/linux-x64 bin/release/netcoreapp2.2/linux-x64/*
	7z a bin/releaseArchives/osx-x64 bin/release/netcoreapp2.2/osx-x64/*
	
clean:
	dotnet clean -c debug
	dotnet clean -c release
	rm -rf /bin 
	rm -rf /obj