using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Models
{
    public class User
    {
        public User() { }

        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Full name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// List of collections from the user.
        /// </summary>
        public List<Collection> Collections { get; set; }
    }
}
