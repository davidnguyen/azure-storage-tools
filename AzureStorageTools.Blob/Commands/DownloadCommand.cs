using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageTools.Blob.Commands
{
    public class DownloadCommand : ICommand<BlobCommandOptions>
    {
        public void Run(BlobCommandOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
