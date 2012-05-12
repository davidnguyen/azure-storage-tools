using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Xml.Linq;
using System.Data.Services.Client;

namespace AzureStorageTools.Table
{
    /// <summary>
    /// Represents a generic table storage entity
    /// </summary>
    public class GenericEntity : TableServiceEntity
    {
        /// <summary>
        /// Data service name space
        /// </summary>
        const string DataServiceNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices";

        /// <summary>
        /// Data service metadata name space
        /// </summary>
        const string DataServiceMetadataNamespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

        /// <summary>
        /// Partition key property name
        /// </summary>
        public const string PartitionKeyName = "PartitionKey";

        /// <summary>
        /// Row key property name
        /// </summary>
        public const string RowKeyName = "RowKey";

        /// <summary>
        /// Creates a new instance of GenericEntity class
        /// </summary>
        public GenericEntity()
        {
            Properties = new Dictionary<string, EntityProperty>();
        }

        /// <summary>
        /// Gets entity properties dictionary
        /// </summary>
        public Dictionary<string, EntityProperty> Properties { get; private set; }

        /// <summary>
        /// Handles entity reading event. Serialises generic entity properties into entity xml.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Reading writing entity event argument</param>
        public static void ReadingEntity(object sender, ReadingWritingEntityEventArgs e)
        {
            XNamespace d = DataServiceNamespace;
            XNamespace m = DataServiceMetadataNamespace;

            var entity = e.Entity as GenericEntity;
            var xProperties = e.Data.Descendants(m + "properties");

            foreach (var xProp in xProperties.Elements())
            {
                var name = xProp.Name.LocalName;
                var xType = xProp.Attribute(m + "type");
                var type = xType != null ? xType.Value : String.Empty;
                var value = xProp.Value;
                entity.Properties.Add(xProp.Name.LocalName, new EntityProperty { Type = type, Value = value });
            }
        }

        /// <summary>
        /// Handles entity writing event. Deserialises entity xml properties into generic entity properties.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Reading writing entity event argument</param>
        public static void WritingEntity(object sender, ReadingWritingEntityEventArgs e)
        {
            XNamespace d = DataServiceNamespace;
            XNamespace m = DataServiceMetadataNamespace;
            var properties = e.Data.Descendants(m + "properties").First();
            var entity = e.Entity as GenericEntity;

            if (entity != null)
            {
                foreach (string propertyName in entity.Properties.Keys)
                {
                    var entityProperty = entity.Properties[propertyName] as EntityProperty;
                    XElement property = String.IsNullOrEmpty(entityProperty.Type)
                        ? new XElement(d + propertyName, entityProperty.Value)
                        : new XElement(d + propertyName, 
                            new XAttribute(m + "type", entityProperty.Type), 
                            entityProperty.Value);
                    properties.Add(property);
                }
            }
        }
    }
}
