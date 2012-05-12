COMMAND USAGE:

	aztable.exe -m <Mode> -s <Source> -d <Destination> [-p <Prefix>]

	-m <Mode>			command mode, valid options are: 
						backup: to back up tables from a source storage account
							to a destination back up location
						restore: to restore tables from xml files in a source 
							folder location to a destination storage account
	-s <Source>			source storage account or directory
	-d <Destination>	destination storage account or directory
	-p <Prefix>			to specifically backup only tables that have the
						specifed prefix

EXAMPLES:

	aztable.exe -m backup -s UseDevelopmentStorage=true -d C:\MyData -p ast

	aztable.ext -m restore -s C:\MyData -d UseDevelopmentStorage=true