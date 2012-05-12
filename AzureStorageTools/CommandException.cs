using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureStorageTools
{
    /// <summary>
    /// Represents a command line program exception
    /// </summary>
    public class CommandException : Exception
    {
        /// <summary>
        /// Instantiates a new instance of CommandException class
        /// </summary>
        /// <param name="message">Exception message</param>
        public CommandException(string message)
            : base(message)
        {
        }
    }
}
