using ChronoZoom.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.PersistencyEngine
{
    class UserFactory
    {

        public UserFactory() { 
            
        }

        public async Task<User> findById(String userId) {
            var id = ObjectId.Parse(userId);
            var collection = MongoFactory.database.GetCollection<User>("user");
            
            var user = await collection.Find<User>(x => x.Id == id).FirstOrDefaultAsync();

            return user;
        }
    }
}
