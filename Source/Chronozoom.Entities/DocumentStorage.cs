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
    public class DocumentStorage
    {
        private static readonly string EndpointUrl = "https://fhict-chronozoom.documents.azure.com:443/";
        private static readonly string AuthorizationKey = "vdGlYrsL1r7FZEqte4yDhWhBTxm8SUdwF7+EfkOh3mU+AINP30T6BihDCayNlJ3opI9GqzqobKnIGoeJwtuBJQ==";
        private static readonly string DatabaseName = "ChronoZoom";
        private static readonly string CollectionTimelineName = "Timeline";
        private static readonly string baseCollectionsUserName = "ChronoZoom";
        private DocumentClient client;
        private Database database;
        private DocumentCollection currentCollection;

        public DocumentStorage() 
        {
            // Create a new instance of the DocumentClient
            client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);

            connectToDatabase();
            getDocumentCollection(CollectionTimelineName);
            createDocument();
        }

        private async void connectToDatabase() {
            // Check to verify a database with the id=ChronoZoom does not exist
            database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();

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
        }

        /***
         * Check to see if a Document collection exists and if not, create one. 
         **/
        private DocumentCollection getDocumentCollection(String collectionName)
        {
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.SelfLink)
                .Where(c => c.Id == CollectionTimelineName).ToArray().FirstOrDefault();

            return documentCollection;
        }


        public void createDocument() {

            DocumentCollection documentCollection = getDocumentCollection(CollectionTimelineName);

            Timeline timeline = new Timeline()
            {
                Title = "Super Title!"
            };

            client.CreateDocumentAsync(documentCollection.SelfLink, timeline);
        }

        public async void getFeaturedContent()
        {
            const string query = @"SELECT * FROM Timeline";

            DocumentCollection documentCollection = getDocumentCollection(CollectionTimelineName);

            var featuredTimelines = client.CreateDocumentQuery(documentCollection.SelfLink, query);

            foreach (var timeline in featuredTimelines)
            {
                Console.WriteLine("\tRead {0} from SQL", timeline);
            }
        }
    }
}
