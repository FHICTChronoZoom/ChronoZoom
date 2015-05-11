using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoZoom.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChronoZoom.Mongo.PersistencyEngine
{
    class CollectionFactory
    {
        public CollectionFactory() { }

        public async Task<Collection> findById(ObjectId collectionId)
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var chronoCollection = await collection.Find<Collection>(x => x.Id == collectionId).FirstOrDefaultAsync();

            return chronoCollection;
        }

        public static async Task<List<Collection>> findMultipleById(List<ObjectId> collectionIds)
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var chronoCollections = await collection.Find<Collection>(x => collectionIds.Contains(x.Id)).ToListAsync();

            return chronoCollections;
        }
    }
}
