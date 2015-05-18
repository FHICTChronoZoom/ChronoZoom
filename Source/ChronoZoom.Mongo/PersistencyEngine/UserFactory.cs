using Chronozoom.Library.Repositories;
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
    public class UserFactory : IUserRepository
    {

        private static const string COLLECTION_NAME = "User";

        private UserFactory() { }

        /// <summary>
        /// Finds a user by his unique identifier
        /// </summary>
        /// <param name="userId">The unique identifier of the user</param>
        /// <returns>The user object or null</returns>
        public static async Task<User> FindByIdAsync(ObjectId userId) 
        {
            var collection = MongoFactory.database.GetCollection<User>("user");
            var user = await collection.Find<User>(x => x.Id == userId).FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Finds a user by his name
        /// </summary>
        /// <param name="name">The name of the user</param>
        /// <returns>The user object or null</returns>
        public static async Task<User> FindByNameAsync(String name) 
        {
            var collection = MongoFactory.database.GetCollection<User>("user");
            var user = await collection.Find<User>(x => x.Name == name).FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Finds a user by his email
        /// </summary>
        /// <param name="email">The email of the user to be found</param>
        /// <returns>The user object or null</returns>
        public static async Task<User> FindByEmailAsync(String email)
        {
            var collection = MongoFactory.database.GetCollection<User>("user");
            var user = await collection.Find<User>(x => x.Email == email).FirstOrDefaultAsync();

            return user;
        }

        /// <summary>
        /// Creates a User in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<Boolean> InsertAsync(User user) 
        {
            var collection = MongoFactory.database.GetCollection<User>("user");
            await collection.InsertOneAsync(user);

            return true;
        }

        public static async Task<User> UpdateAsync(User user) { }

        public static async Task<User> DeleteAsync(User user) { }
    }
}
