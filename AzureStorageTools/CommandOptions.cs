using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;

namespace AzureStorageTools
{
    /// <summary>
    /// Represents the most common command options object used in azure storage tools for blob and table
    /// </summary>
    public class CommandOptions
    {
        /// <summary>
        /// Command mode
        /// </summary>
        [Option("m", null, Required = false)]
        public string Mode = "";

        /// <summary>
        /// Data source
        /// </summary>
        [Option("s", null, Required = false)]
        public string Source = "";

        /// <summary>
        /// Data destination
        /// </summary>
        [Option("d", null, Required = false)]
        public string Destination = "";
    }
}
