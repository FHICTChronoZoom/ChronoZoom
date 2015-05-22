using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoZoom.Mongo.Models;
using Chronozoom.Library.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Chronozoom.Library.Repositories;

namespace ChronoZoom.Mongo.PersistencyEngine
{
    public class CollectionFactory : ICollectionRepository
    {

        private const string COLLECTION_NAME = "collection";

        public CollectionFactory() { }

        public async Task<Mongo.Models.Collection> FindByIdAsync(Guid collectionId)
        {
            var collection = MongoFactory.database.GetCollection<Mongo.Models.Collection>(COLLECTION_NAME);
            var chronoCollection = await collection.Find<Mongo.Models.Collection>(x => x.Id == collectionId).FirstOrDefaultAsync();

            return chronoCollection;
        }

        /// <summary>
        /// Find all the collection of a specific user
        /// </summary>
        /// <param name="userId">The Id of the user to find all the collections from</param>
        /// <returns>A list of collections</returns>
        public async Task<List<Mongo.Models.Collection>> FindByUserIdAsync(Guid userId)
        {
            var collection = MongoFactory.database.GetCollection<Mongo.Models.Collection>(COLLECTION_NAME);
            var userCollections = await collection.Find<Mongo.Models.Collection>(x => x.OwnerId.Equals(userId)).ToListAsync();

            return userCollections;
        }

        /// <summary>
        /// Finds a list of collections by their id's
        /// </summary>
        /// <param name="collectionIds">A list of ObjectId's</param>
        /// <returns>A list of ObjectId's</returns>
        public async Task<List<Mongo.Models.Collection>> FindMultipleByIdAsync(List<Guid> collectionIds)
        {
            var collection = MongoFactory.database.GetCollection<Mongo.Models.Collection>(COLLECTION_NAME);
            var chronoCollections = await collection.Find<Mongo.Models.Collection>(x => collectionIds.Contains(x.Id)).ToListAsync();

            return chronoCollections;
        }

        //public static async Task<Collection> FindByTimelineIdAsync(ObjectId timelineId) {
        //    var collection = MongoFactory.database.GetCollection<Collection>(COLLECTION_NAME);
            
        //    //var timelineCollection = await collection.Find<Collection>(x => x.Timelines.Find<Timeline>(y => y.Id == timelineId)).ToListAsync();

        //    return timelineCollection;
        //}

        /// <summary>
        /// Find all public collections.
        /// </summary>
        /// <returns>A list of public collections</returns>
        public async Task<List<Mongo.Models.Collection>> FindPublicCollectionsAsync()
        {
            var collection = MongoFactory.database.GetCollection<Mongo.Models.Collection>(COLLECTION_NAME);
            var publicCollections = await collection.Find<Mongo.Models.Collection>(x => x.PubliclySearchable == true).ToListAsync();

            return publicCollections;
        }

        /// <summary>
        /// Find the public collection of a specific user
        /// </summary>
        /// <param name="userId">The userId of the user to find the default collection from</param>
        /// <returns>The default collection or null</returns>
        public async Task<Mongo.Models.Collection> FindUserDefaultCollectionAsync(Guid userId)
        {
            var collection = MongoFactory.database.GetCollection<Mongo.Models.Collection>(COLLECTION_NAME);
            var defaultCollection = await collection.Find<Mongo.Models.Collection>(x => x.Default == true && x.OwnerId.Equals(userId)).FirstOrDefaultAsync();

            return defaultCollection;
        }

        /// <summary>
        /// Checks if a user is a member inside a collection.
        /// This can only be true if the collection allows members and the user is part of the member array
        /// </summary>
        /// <param name="userId">The user to be checked if he has member privilages</param>
        /// <param name="collectionId">The collection to check it on</param>
        /// <returns>True if the user has member priviliges, otherwise false.</returns>
        public async Task<Boolean> IsMemberAsync(Guid userId, Guid collectionId)
        {
            var collection = MongoFactory.database.GetCollection<Mongo.Models.Collection>(COLLECTION_NAME);
            var memberCollection = await collection.Find<Mongo.Models.Collection>(x => x.Id.Equals(collectionId) && x.MembersAllowed == true && x.Members.Contains(userId)).FirstOrDefaultAsync();

            return memberCollection != null;
        }
    }
}
