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
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// List of collections from the user.
        /// </summary>
        [BsonElement("collections")]
        public List<ObjectId> Collections { get; set; }
    }
}
