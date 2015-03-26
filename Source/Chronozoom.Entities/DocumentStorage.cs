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
        private static readonly string CollectionTimelineName = "Timeline";
        private DocumentClient client;

        public DocumentStorage() 
        {
            // Create a new instance of the DocumentClient
            client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);

            connectToDatabase();
            getDocumentCollection(CollectionTimelineName);
        }

        private Database connectToDatabase() {
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
                // Something went wrong. Warn the user.
                throw new NotImplementedException();
            }

            return database;
        }

        /***
         * Check to see if a Document collection exists and if not, create one. 
         **/
        private DocumentCollection getDocumentCollection(String collectionName)
        {
            // Check to verify a document with the id
            DocumentCollection documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                new DocumentCollection
                {
                    Id = collectionName
                });
            return documentCollection;
        }


        public List<dynamic> getFeaturedContent()
        {
            const string query = @"SELECT * FROM Timeline WHERE Featured = true";
            DocumentCollection documentCollection = getDocumentCollection(CollectionTimelineName);
            var featuredTimelines = client.CreateDocumentQuery(documentCollection.DocumentsLink, query);

            foreach (var timeline in featuredTimelines)
            {
                Console.WriteLine("\tRead {0} from SQL", timeline);
            }

            return featuredTimelines.ToList();
        }
    }
}
