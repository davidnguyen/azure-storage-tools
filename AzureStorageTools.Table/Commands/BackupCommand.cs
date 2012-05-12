using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Data.Services.Client;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace AzureStorageTools.Table.Commands
{
    /// <summary>
    /// Backups tables from a storage account (source)
    /// to a local directory (destination) in xml files
    /// </summary>
    public class BackupCommand : ICommand<TableCommandOptions>
    {
        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="options">Command options</param>
        public void Run(TableCommandOptions options)
        {
            var sourceAccount = CloudStorageAccount.Parse(options.Source);
            var tableClient = sourceAccount.CreateCloudTableClient();
            var tableContext = tableClient.GetDataServiceContext();

            var tables = tableClient.ListTables();
            tables = !String.IsNullOrEmpty(options.TableNamePrefix)
                ? tables.Where(t => t.StartsWith(options.TableNamePrefix))
                : tables;

            tableContext.ReadingEntity += new EventHandler<ReadingWritingEntityEventArgs>(GenericEntity.ReadingEntity);

            Directory.CreateDirectory(options.Destination);

            foreach (var table in tables)
            {
                var query = tableContext.CreateQuery<GenericEntity>(table);
                var entities = query.ToArray();

                var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(table,
                        entities.Select(e => new XElement("entity",
                            e.Properties.Select(p =>
                            {
                                return String.IsNullOrEmpty(p.Value.Type)
                                    ? new XElement(p.Key, p.Value.Value)
                                    : new XElement(p.Key,
                                        new XAttribute("type", p.Value.Type),
                                        p.Value.Value);
                            })
                        ))
                    )
                );

                doc.Save(options.Destination + "\\" + table + ".xml");
            }
        }
    }
}
