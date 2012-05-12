using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureStorageTools.Blob;
using AzureStorageTools.Blob.Commands;

namespace AzureStorageTools.Test
{
    /// <summary>
    /// Summary description for BlobCommandsTests
    /// </summary>
    [TestClass]
    public class BlobCommandsTests
    {
        const string DevelopmentStorage = "UseDevelopmentStorage=true";
        const string DataLocation = "D:\\Lab\\Social Matrix\\Source\\tool-projects\\AzureStorageTools\\AzureStorageTools.Blob\\SampleFiles";

        [TestMethod]
        public void TestMethod1()
        {
            var uploadCommand = new UploadCommand();
            var options = new BlobCommandOptions
            {
                Mode = "upload",
                Source = DataLocation,
                Destination = DevelopmentStorage,
                Container = "sample-files"
            };
            uploadCommand.Run(options);
        }
    }
}
