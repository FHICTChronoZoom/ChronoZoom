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
    public class UserFactory
    {

        public UserFactory() { 
            
        }

        /// <summary>
        /// Finds a user by his unique identifier
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>The user object or a default</returns>
        public async Task<User> findById(ObjectId userId) 
        {
            var collection = MongoFactory.database.GetCollection<User>("user");
            var user = await collection.Find<User>(x => x.Id == userId).FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Finds a user by his name
        /// </summary>
        /// <param name="name">The name of the user</param>
        /// <returns>The user object or a default</returns>
        public async Task<User> findByName(String name) 
        {
            var collection = MongoFactory.database.GetCollection<User>("user");
            var user = await collection.Find<User>(x => x.Name == name).FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Creates a User in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Boolean> create(User user) 
        {
            var collection = MongoFactory.database.GetCollection<User>("user");
            await collection.InsertOneAsync(user);

            return true;
        }

    }
}
