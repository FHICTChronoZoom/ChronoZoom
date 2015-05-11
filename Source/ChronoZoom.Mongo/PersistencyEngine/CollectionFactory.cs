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
    public class CollectionFactory
    {
        public CollectionFactory() { }

        public async Task<Collection> findById(ObjectId collectionId)
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var chronoCollection = await collection.Find<Collection>(x => x.Id == collectionId).FirstOrDefaultAsync();

            return chronoCollection;
        }

        /// <summary>
        /// Find all the collection of a specific user
        /// </summary>
        /// <param name="userId">The Id of the user to find all the collections from</param>
        /// <returns>A list of collections</returns>
        public static async Task<List<Collection>> findByUserId(ObjectId userId)
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var userCollections = await collection.Find<Collection>(x => x.OwnerId.Equals(userId)).ToListAsync();

            return userCollections;
        }

        public static async Task<List<Collection>> findMultipleById(List<ObjectId> collectionIds)
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var chronoCollections = await collection.Find<Collection>(x => collectionIds.Contains(x.Id)).ToListAsync();

            return chronoCollections;
        }
    }
}
