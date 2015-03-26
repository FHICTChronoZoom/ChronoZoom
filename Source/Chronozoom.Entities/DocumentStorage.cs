using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add DocumentDB references
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Services;
using Newtonsoft.Json;

namespace Chronozoom.Entities
{
    class DocumentStorage
    {
        private static readonly string EndpointUrl = "DocumentDB endpoint";
        private static readonly string AuthorizationKey = "DocumentDB Authorization key";
        private static readonly string DatabaseName = "ChronoZoom";

        public DocumentStorage() 
        {
            // Create a new instance of the DocumentClient
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);

            // Check to verify a database with the id=ChronoZoom does not exist
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();

            // If there is no database, create one.
            if (database == null)
            {
                // Create a database
                database = await client.CreateDatabaseAsync(
                    new Database
                    {
                        Id = DatabaseName
                    });
            }
            else 
            {
                Warn("database");
            }

            // Check to verify a document with the id
        }

    }

    internal sealed class Timeline
    {
        public String name { get; set; }
        public Guid id { get; set; }

    }


}
