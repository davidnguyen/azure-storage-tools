using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace AzureStorageTools.Blob
{
    public class BlobCommandOptions : CommandOptions
    {
        [Option("p", null, Required = false)]
        public string Path = "";

        [Option("c", null, Required = false)]
        public string Container = "";
    }
}
