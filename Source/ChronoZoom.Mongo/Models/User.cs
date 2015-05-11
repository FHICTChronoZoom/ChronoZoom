using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Models
{
    class User
    {
        public User() { }

        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        [BsonId]
        public ObjectId id { get; set; }

        /// <summary>
        /// Full name of the user
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// List of collections from the user.
        /// </summary>
        public List<Collection> collections { get; set; }
    }
}
