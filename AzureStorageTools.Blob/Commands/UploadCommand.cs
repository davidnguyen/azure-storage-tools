using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services.Client;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageTools.Blob.Commands
{
    public class UploadCommand : ICommand<BlobCommandOptions>
    {
        public void Run(BlobCommandOptions options)
        {
            var account = CloudStorageAccount.Parse(options.Destination);
            var client = account.CreateCloudBlobClient();

            var container = client.GetContainerReference(options.Container);
            var per = new BlobContainerPermissions();
            per.PublicAccess = BlobContainerPublicAccessType.Blob;
            container.CreateIfNotExist();
            container.SetPermissions(per);

            var fileCount = 0;

            var files = Directory.GetFiles(options.Source, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                var fileSubDir = fileInfo.DirectoryName.Remove(0, Path.GetFullPath(options.Source).Length)
                    .Split("\\".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var fileSubDirNames = fileSubDir.Length > 0
                    ? fileSubDir.Aggregate((s1, s2) => String.Format("{0}/{1}", s1, s2))
                    : String.Empty;

                var blobName = String.Format("{0}{1}{2}",
                    !String.IsNullOrEmpty(options.Path) ? options.Path + "/" : "",
                    fileSubDirNames.Length > 0 ? String.Format("{0}/", fileSubDirNames) : "",
                    fileInfo.Name);

                var blob = container.GetBlobReference(blobName);

                blob.Properties.ContentType = MimeTypes.FromFileName(file);
                blob.UploadFile(file);
                fileCount += 1;
            }
            

            Console.WriteLine("\n{0} file(s) uploaded successfully\n", fileCount);
        }
    }
}
