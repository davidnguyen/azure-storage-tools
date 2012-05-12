using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureStorageTools
{
    /// <summary>
    /// Represents a command interface
    /// </summary>
    /// <typeparam name="TOptions">Command option type</typeparam>
    public interface ICommand<TOptions>
        where TOptions : CommandOptions
    {
        /// <summary>
        /// Runs command
        /// </summary>
        /// <param name="options">Command options</param>
        void Run(TOptions options);
    }
}
