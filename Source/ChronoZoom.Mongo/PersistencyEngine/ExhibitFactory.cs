﻿using Chronozoom.Library.Repositories;
using ChronoZoom.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.PersistencyEngine
{
    public class ExhibitFactory : IExhibitRepository
    {
        private const string COLLECTION_NAME = "exhibit";

        private ExhibitFactory() { }

        /// <summary>
        /// Find an exhibit by it's timeline id
        /// </summary>
        /// <param name="timelineId"></param>
        /// <returns></returns>
        public static async Task<Exhibit> FindByTimelineIdAsync(ObjectId timelineId) 
        {
            var collection = MongoFactory.database.GetCollection<Exhibit>(COLLECTION_NAME);
            var exhibit = await collection.Find<Exhibit>(x => x.Id.Equals(timelineId)).FirstOrDefaultAsync();

            return exhibit;
        }

        /// <summary>
        /// Insert an exhibit
        /// </summary>
        /// <param name="exhibit">The exhibit to be inserted</param>
        /// <returns>A boolean which indicated whether the insertion was succesful</returns>
        public static async Task<Boolean> InsertAsync(Exhibit exhibit) 
        {
            var collection = MongoFactory.database.GetCollection<Exhibit>(COLLECTION_NAME);
            await collection.InsertOneAsync(exhibit);

            return true;
        }

        /// <summary>
        /// Update an exhibit.
        /// For now, we update the entire user object. This is not ideal and definitely not the neatest way
        /// to do this. But it's fast and it's effective.
        /// </summary>
        /// <param name="exhibit">The updated Exhibit Object</param>
        /// <returns>ReplaceOneResult generated by the MongoDB server</returns>
        public static async Task<ReplaceOneResult> UpdateAsync(Exhibit exhibit)
        {
            var collection = MongoFactory.database.GetCollection<Exhibit>(COLLECTION_NAME);
            var result = await collection.ReplaceOneAsync<Exhibit>(x => x.Id.Equals(exhibit.Id), exhibit, new UpdateOptions { IsUpsert = true });

            return result;
        }

        /// <summary>
        /// Deletes an exhibit from the database. permanently.
        /// </summary>
        /// <param name="exhibit">The Exhibit object to be deleted</param>
        /// <returns>DeleteResult generated by the MongoDB server</returns>
        public static async Task<DeleteResult> DeleteAsync(Exhibit exhibit)
        {
            var collection = MongoFactory.database.GetCollection<Exhibit>(COLLECTION_NAME);
            var result = await collection.DeleteOneAsync<Exhibit>(x => x.Id.Equals(exhibit));
            
            return result;
        }
    }
}
