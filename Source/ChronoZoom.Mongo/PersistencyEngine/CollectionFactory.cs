﻿using System;
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

        /// <summary>
        /// Finds a list of collections by their id's
        /// </summary>
        /// <param name="collectionIds">A list of ObjectId's</param>
        /// <returns>A list of ObjectId's</returns>
        public static async Task<List<Collection>> findMultipleById(List<ObjectId> collectionIds)
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var chronoCollections = await collection.Find<Collection>(x => collectionIds.Contains(x.Id)).ToListAsync();

            return chronoCollections;
        }

        /// <summary>
        /// Find all public collections.
        /// </summary>
        /// <returns>A list of public collections</returns>
        public static async Task<List<Collection>> findPublicCollections()
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var publicCollections = await collection.Find<Collection>(x => x.PubliclySearchable == true).ToListAsync();

            return publicCollections;
        }

        /// <summary>
        /// Find the public collection of a specific user
        /// </summary>
        /// <param name="userId">The userId of the user to find the default collection from</param>
        /// <returns>The default collection or null</returns>
        public static async Task<Collection> findUserDefaultCollection(ObjectId userId)
        {
            var collection = MongoFactory.database.GetCollection<Collection>("collection");
            var defaultCollection = await collection.Find<Collection>(x => x.Default == true && x.OwnerId.Equals(userId)).FirstOrDefaultAsync();

            return defaultCollection;
        }
    }
}
