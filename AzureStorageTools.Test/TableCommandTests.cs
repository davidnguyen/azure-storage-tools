using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureStorageTools.Table;
using AzureStorageTools.Table.Commands;

namespace AzureStorageTools.Test
{
    [TestClass]
    public class TableCommandTests
    {
        const string DevelopmentStorage = "UseDevelopmentStorage=true";
        const string DataLocation = "D:\\Lab\\Social Matrix\\Source\\tool-projects\\AzureStorageTools\\AzureStorageTools.Table\\data\\restore";
        const string BackupLocation = "D:\\Lab\\Social Matrix\\Source\\tool-projects\\AzureStorageTools\\AzureStorageTools.Table\\data\\backup";

        [TestMethod]
        public void TableRestoreTest()
        {
            var restoreCommand = new RestoreCommand();
            var options = new CommandOptions
            {
                Mode = "restore",
                Source = DataLocation,
                Destination = DevelopmentStorage
            };
            restoreCommand.Run(options);
        }

        [TestMethod]
        public void TableBackupTest()
        {
            var backupCommand = new BackupCommand();
            var options = new TableCommandOptions
            {
                Mode = "backup",
                Source = DevelopmentStorage,
                Destination = BackupLocation,
                TableNamePrefix = "azTest"
            };
            backupCommand.Run(options);
        }
    }
}
