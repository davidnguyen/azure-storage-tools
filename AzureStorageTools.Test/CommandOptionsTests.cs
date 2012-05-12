using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureStorageTools.Test
{
    [TestClass]
    public class CommandOptionsTests
    {
        [TestMethod]
        public void CommandOptions_Lack_Of_Arguments_Test()
        {
            var options = new CommandOptions();
            var parser = new CommandLine.CommandLineParser();
            var parsed = parser.ParseArguments(new[] { "-m", "-s"}, options);
            Assert.AreEqual(false, parsed);
        }

        [TestMethod]
        public void CommandOptions_Mode_Source_Destination_Test()
        {
            var options = new CommandOptions();
            var parser = new CommandLine.CommandLineParser();
            parser.ParseArguments(new[] { "-m", "backup", "-s", "mySource", "-d", "myDestination" }, options);
            Assert.AreEqual("backup", options.Mode);
            Assert.AreEqual("mySource", options.Source);
            Assert.AreEqual("myDestination", options.Destination);
        }
    }
}
