using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageTools.Table
{
    using Commands;

    /// <summary>
    /// Represents the command line tool program entry
    /// </summary>
    class Program : CommandLineTool<TableCommandOptions>
    {
        /// <summary>
        /// Command map
        /// </summary>
        readonly Dictionary<string, Type> commandMap =
            new Dictionary<string, Type>()
                {
                    {"backup", typeof(BackupCommand)},
                    {"restore", typeof(RestoreCommand)}
                };

        /// <summary>
        /// Gets command map
        /// </summary>
        protected override Dictionary<string, Type> CommandMap
        {
            get { return commandMap; } 
        }

        /// <summary>
        /// Gets readme resource file name
        /// </summary>
        protected override string ReadmeResource
        {
            get { return "AzureStorageTools.Table.README.txt"; }
        }

        /// <summary>
        /// Program entry method
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            var program = new Program();
            program.ParseAndRun(args);

            #if DEBUG
            Console.ReadLine();
            #endif
        }
    }
}
