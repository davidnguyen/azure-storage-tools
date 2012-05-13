using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using CommandLine;
using System.Diagnostics;

namespace AzureStorageTools
{
    /// <summary>
    /// Serves as the base class for command line tool entry classes
    /// </summary>
    /// <typeparam name="TOptions">The type of command options</typeparam>
    public abstract class CommandLineTool<TOptions>
        where TOptions : CommandOptions
    {
        /// <summary>
        /// Gets command map dictionary
        /// </summary>
        protected abstract Dictionary<string, Type> CommandMap { get; }

        /// <summary>
        /// Gets command line tool read me resource file
        /// </summary>
        protected abstract string ReadmeResource { get; }

        /// <summary>
        /// Parse command arguments into options and run
        /// </summary>
        /// <param name="args"></param>
        public void ParseAndRun(string[] args)
        {
            var options = Activator.CreateInstance<TOptions>(); //default(TOptions);
            var commandLineParser = new CommandLineParser();
            if (commandLineParser.ParseArguments(args, options))
            {
                if (CommandMap.ContainsKey(options.Mode))
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    var commandTypeName = "Command";
                    try
                    {
                        var commandType = CommandMap[options.Mode];
                        commandTypeName = commandType.Name;
                        Console.WriteLine("Command '{0}' begins...", commandTypeName);
                        var command = Activator.CreateInstance(commandType) as ICommand<TOptions>;
                        command.Run(options);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An exception occurred while running command: " + ex.ToString());
                    }
                    sw.Stop();
                    Console.WriteLine("Command '{0}' completed in {1}ms", commandTypeName, sw.ElapsedMilliseconds);
                }
                else
                {
                    // Command mode does not exist
                    Console.WriteLine("Invalid command mode");
                    this.PrintReadme();
                }
            }
            else
            {
                // Command arguments parsing error
                Console.WriteLine("Unable to parse command arguments");
                this.PrintReadme();
            }
        }

        /// <summary>
        /// Prints out read me resource file content
        /// </summary>
        private void PrintReadme()
        {
            using (Stream stream = Assembly.GetEntryAssembly().GetManifestResourceStream(ReadmeResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                Console.WriteLine();
                Console.WriteLine(result);
            }
        }
    }
}
