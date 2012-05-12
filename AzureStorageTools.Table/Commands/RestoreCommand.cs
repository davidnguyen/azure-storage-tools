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
    public class RestoreCommand : ICommand<CommandOptions>
    {
        public void Run(CommandOptions options)
        {
            var sourceXmlFiles = Directory.GetFiles(options.Source);
            var destinationStorageAccount = CloudStorageAccount.Parse(options.Destination);
            var tableClient = destinationStorageAccount.CreateCloudTableClient();
            var tableContext = tableClient.GetDataServiceContext();
            tableContext.WritingEntity += new EventHandler<ReadingWritingEntityEventArgs>(GenericEntity.WritingEntity);

            foreach (var file in sourceXmlFiles)
            {
                var doc = XDocument.Load(file);
                var tableName = doc.Root.Name.LocalName;
                var entityElements = doc.Root.Elements("entity");

                tableClient.DeleteTableIfExist(tableName);
                tableClient.CreateTable(tableName);

                foreach (var entityElement in entityElements)
                {
                    var entity = new GenericEntity();

                    entity.PartitionKey = entityElement.Element(GenericEntity.PartitionKeyName).Value;
                    entity.RowKey = entityElement.Element(GenericEntity.RowKeyName).Value;

                    var otherEntityPropertyElements = entityElement.Elements().Where(e => 
                        e.Name.LocalName != GenericEntity.PartitionKeyName 
                        && e.Name.LocalName != GenericEntity.RowKeyName);

                    foreach (var propertyElement in otherEntityPropertyElements)
                    {
                        entity.Properties.Add(propertyElement.Name.LocalName,
                            new EntityProperty
                            {
                                Value = propertyElement.Value,
                                Type = propertyElement.Attribute("type") != null 
                                    ? propertyElement.Attribute("type").Value 
                                    : String.Empty
                            });
                    }

                    tableContext.AddObject(tableName, entity);
                }

                tableContext.SaveChanges();
            }
        }
    }
}
