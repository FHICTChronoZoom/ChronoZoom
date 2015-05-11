using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChronoZoom.Mongo.Models
{
    class Collection
    {
        public Collection() 
        {
            this.Default = false;
            this.MembersAllowed = false;
            this.PubliclySearchable = false;
        }

        /// <summary>
        /// Unique identifier of the collection
        /// </summary>
        [BsonId]
        public ObjectId id { get; set; }

        /// <summary>
        /// Unique identifier of the user who owns this collection
        /// </summary>
        public ObjectId ownerId { get; set; }

        /// <summary>
        /// The name/title of the collection
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL-sanitized version of title, which will be used as part of a URL path.
        /// Only a-z, 0-9 and hyphen are allowed. This should be unique for all collections from a user.
        /// </summary>
        public string Path { get; set; }


        /// <summary>
        /// Boolean determining if this is the default collection for the user.
        /// There should only be only default collection. Default is false.
        /// </summary>
        public Boolean Default { get; set; }

        /// <summary>
        /// Boolean determining if this collection allows other users to edit
        /// or maintain the collection. Default is false.
        /// </summary>
        public Boolean MembersAllowed { get; set; }

        /// <summary>
        /// Boolean determining if this collection is visible to other users.
        /// Default is false.
        /// </summary>
        public Boolean PubliclySearchable { get; set; }
    }
}
