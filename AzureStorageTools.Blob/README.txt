COMMAND USAGE:

	azblob.exe -m <Mode> -s <Source> -d <Destination>

	-m <Mode>			command mode, valid options are
						upload: 
							to upload from a source local directory to a
							destination storage account
						download: 
							to download from a source storage account to
							a destination local directory
	-s <Source>			source storage account or directory
	-d <Destination>	destination storage account or directory

EXAMPLES:

	azblob.exe -m upload -s C:\MyData -d UseDevelopmentStorage=true

	azblob.ext -m download -s UseDevelopmentStorage=true -d C:\MyData