using Chronozoom.Library.Repositories;
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

        public static async Task<Exhibit> FindByTimelineIdAsync(ObjectId timelineId) 
        {
            var collection = MongoFactory.database.GetCollection<Exhibit>(COLLECTION_NAME);
            var exhibit = await collection.Find<Exhibit>(x => x.Id.Equals(timelineId)).FirstOrDefaultAsync();

            return exhibit;
        }
    }
}
