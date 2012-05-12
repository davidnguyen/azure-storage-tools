using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureStorageTools.Blob
{
    /// <summary>
    /// Represents the command line tool program entry
    /// </summary>
    class Program : CommandLineTool<BlobCommandOptions>
    {
        /// <summary>
        /// Command map
        /// </summary>
        readonly Dictionary<string, Type> commandMap =
            new Dictionary<string, Type>()
                {
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
            get { return "AzureStorageTools.Blob.README.txt"; }
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
