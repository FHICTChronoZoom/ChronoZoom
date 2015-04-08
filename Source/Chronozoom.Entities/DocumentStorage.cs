using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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


        public static readonly string COLLECTION_TIMELINE_NAME = "Timeline";
        public static readonly string COLLECTION_EXHIBIT_NAME = "Exhibit";
        public static readonly string COLLECTION_CONTENT_ITEM_NAME = "ContentItem";
        public static readonly string COLLECTION_COLLECTION_NAME = "Collection";
        public static readonly string COLLECTION_USER_NAME = "User";
        //private static readonly string COLLECTION_



        private static readonly string baseCollectionsUserName = "ChronoZoom";
        private DocumentClient client;
        private Database database;
        private DocumentCollection currentCollection;

        public DocumentStorage() 
        {
            // Create a new instance of the DocumentClient
            client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);

            connectToDatabase();
            getDocumentCollection(COLLECTION_TIMELINE_NAME);
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

        public DocumentClient Client { get { return client; }  }

        /***
         * Check to see if a Document collection exists and if not, create one. 
         **/
        private DocumentCollection getDocumentCollection(String collectionName)
        {
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.SelfLink)
                .Where(c => c.Id == COLLECTION_TIMELINE_NAME).ToArray().FirstOrDefault();

            return documentCollection;
        }

        private dynamic getDocumentFromCollection(String collectionName, String query)
        {
            DocumentCollection documentCollection = getDocumentCollection(collectionName);
            var documents = client.CreateDocumentQuery(documentCollection.SelfLink, query);

            return documents;

        }

        public void createDocument() {

            DocumentCollection documentCollection = getDocumentCollection(COLLECTION_TIMELINE_NAME);

            Timeline timeline = new Timeline()
            {
                Title = "Super Title!"
            };

            client.CreateDocumentAsync(documentCollection.SelfLink, timeline);
        }

        public rCollection getCollection(Guid collectionId)
        {
            rCollection rv = client.CreateDocumentQuery<rCollection>(COLLECTION_COLLECTION_NAME).Where(c => c.Id == collectionId).FirstOrDefault();
            rUser user = client.CreateDocumentQuery<rUser>(COLLECTION_USER_NAME).Where(u => u.Id == rv.UserId).FirstOrDefault();
            rv.User = user;
            return rv;
        }

        public rCollection getDefaultCollection(String superColletionName)
        {

            rCollection rv = client.CreateDocumentQuery<rCollection>(COLLECTION_COLLECTION_NAME)
                .Where(c => c.SuperCollection.Title == superColletionName &&
                        c.Default == true).FirstOrDefault();
            return rv;
        }


        public void getFeaturedContent()
        {
            const string query = @"SELECT * FROM Timeline";

            DocumentCollection documentCollection = getDocumentCollection(COLLECTION_TIMELINE_NAME);

            var featuredTimelines = getDocumentFromCollection(documentCollection.SelfLink, query);

            foreach (var timeline in featuredTimelines)
            {
                Console.WriteLine("\tRead {0} from SQL", timeline);
            }
        }

        public IEnumerable<Timeline> RetrieveAllTimelines(Guid collectionId)
        {
            int maxAllElements = 0;
            IEnumerable<TimelineRaw> allTimelines = new TimelineRaw[0];
            Dictionary<Guid, Timeline> timelinesMap = new Dictionary<Guid, Timeline>();
            DocumentCollection documentCollection = getDocumentCollection(COLLECTION_TIMELINE_NAME);

            string query = string.Format(@"SELECT * FROM Timelines WHERE Collection_ID = {0}", collectionId).ToString();

            try
            {
                var ballTimelines = getDocumentFromCollection(documentCollection.SelfLink, query);
            }
            catch (Exception e)
            {
                throw e;
            }

            IEnumerable<Timeline> rootTimelines = FillTimelinesFromFlatList(documentCollection.SelfLink, allTimelines, timelinesMap, null, ref maxAllElements);
            FillTimelineRelations(timelinesMap, int.MaxValue);

            return rootTimelines;
        }

        private void FillTimelineRelations( Dictionary<Guid, Timeline> timelinesMap, int maxElements)
        {
            if (!timelinesMap.Keys.Any())
                return;

            // Populate Exhibits
            string exhibitsQuery = string.Format(
                CultureInfo.InvariantCulture,
                "SELECT TOP({0}) *, Year as [Time] FROM Exhibits WHERE Timeline_Id IN ('{1}')",
                maxElements,
                string.Join("', '", timelinesMap.Keys.ToArray()));

            //IEnumerable<ExhibitRaw> exhibitsRaw = new ExhibitRaw[0];
            DocumentCollection exhibitCollection = getDocumentCollection(COLLECTION_EXHIBIT_NAME);
            var exhibitsRaw = getDocumentFromCollection(exhibitCollection.SelfLink, exhibitsQuery).ToArray();
            

            Dictionary<Guid, Exhibit> exhibits = new Dictionary<Guid, Exhibit>();
            foreach (ExhibitRaw exhibitRaw in exhibitsRaw)
            {
                if (exhibitRaw.ContentItems == null)
                    exhibitRaw.ContentItems = new Collection<ContentItem>();

                if (timelinesMap.Keys.Contains(exhibitRaw.Timeline_ID))
                {
                    timelinesMap[exhibitRaw.Timeline_ID].Exhibits.Add(exhibitRaw);
                    exhibits[exhibitRaw.Id] = exhibitRaw;
                }
            }

            if (exhibits.Keys.Any())
            {
                // Populate Content Items
                string contentItemsQuery = string.Format(
                    CultureInfo.InvariantCulture,
                    @"
                        SELECT * 
                        FROM ContentItems 
                        WHERE Exhibit_Id IN ('{0}')
                        ORDER BY [Order] ASC
                    ",
                    string.Join("', '", exhibits.Keys.ToArray()));

                //IEnumerable<ContentItemRaw> contentItemsRaw = new ContentItemRaw[0];
                DocumentCollection contentItemCollection = getDocumentCollection(COLLECTION_CONTENT_ITEM_NAME);
                var contentItemsRaw = getDocumentFromCollection(contentItemCollection.SelfLink, contentItemsQuery).ToArray();


                foreach (ContentItemRaw contentItemRaw in contentItemsRaw)
                {
                    if (exhibits.Keys.Contains(contentItemRaw.Exhibit_ID))
                    {
                        exhibits[contentItemRaw.Exhibit_ID].ContentItems.Add(contentItemRaw);
                    }
                }
            }
        }

        private static List<Timeline> FillTimelinesFromFlatList(String documentLink, IEnumerable<TimelineRaw> timelinesRaw, Dictionary<Guid, Timeline> timelinesMap, Guid? commonAncestor, ref int maxElements)
        {
            List<Timeline> timelines = new List<Timeline>();
            Dictionary<Guid, Guid?> timelinesParents = new Dictionary<Guid, Guid?>();

            foreach (TimelineRaw timelineRaw in timelinesRaw)
            {
                if (timelineRaw.Exhibits == null)
                    timelineRaw.Exhibits = new Collection<Exhibit>();

                timelinesParents[timelineRaw.Id] = timelineRaw.Timeline_ID;
                timelinesMap[timelineRaw.Id] = timelineRaw;

                maxElements--;
            }

            // Build the timelines tree by assigning each timeline to its parent
            foreach (Timeline timeline in timelinesMap.Values)
            {
                Guid? parentId = timelinesParents[timeline.Id];

                // If the timeline should be a root timeline
                if (timeline.Id == commonAncestor || parentId == null || !timelinesMap.Keys.Contains((Guid)parentId))
                {
                    timelines.Add(timeline);
                }
                else
                {
                    Timeline parentTimeline = timelinesMap[(Guid)parentId];
                    if (parentTimeline.ChildTimelines == null)
                        parentTimeline.ChildTimelines = new Collection<Timeline>();

                    parentTimeline.ChildTimelines.Add(timeline);
                }
            }

            return timelines;
        }
    }
}
