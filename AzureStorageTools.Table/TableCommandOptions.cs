using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace AzureStorageTools.Table
{
    public class TableCommandOptions : CommandOptions
    {
        [Option("p", null, Required = false)]
        public string TableNamePrefix = "";
    }
}
