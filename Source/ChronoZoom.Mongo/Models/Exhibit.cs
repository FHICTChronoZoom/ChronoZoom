using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace ChronoZoom.Mongo.Models
{

    public class Exhibit
    {
        public Exhibit() 
        { 
            this.IsCirca = false;
        }

        /// <summary>
        /// Unique identifier of the exhibit
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// The depth of the exhibit in the timeline tree.
        /// Inherited from the pre-Fontys version of ChronoZoom.
        /// </summary>
        [BsonElement("depth")]
        public int Depth { get; set; }

        /// <summary>
        /// The title of the exhibit
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// The timestamp of the exhibit. This is the point in time
        /// where the exhibit should be placed. Decimal because it can then be
        /// placed on a specific day/month/year
        /// </summary>
        [BsonElement("year")]
        public decimal Year { get; set; }

        /// <summary>
        /// Boolean determining if the `Year` property is an estimation.
        /// If this is false, it will not be persisted.
        /// The default is false.
        /// </summary>
        [BsonElement("isCirca")]
        [BsonIgnoreIfDefault]
        public bool IsCirca { get; set; }

        /// <summary>
        /// Identifier of the user who last updated this exhibit
        /// If this is not available, it will not be persisted.
        /// </summary>
        [BsonElement("updated.userId")]
        [BsonIgnoreIfDefault]
        public ObjectId UpdatedByUser { get; set; }

        /// <summary>
        /// Timestamp of the moment the last edit was mode.
        /// If this is not available, it will not be persisted.
        /// </summary>
        [BsonElement("updated.timestamp")]
        [BsonIgnoreIfDefault]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// A list of content items belonging to this exhibit.
        /// If this is not available, it will not be persisted.
        /// </summary>
        [BsonElement("contentItems")]
        [BsonIgnoreIfDefault]
        public List<ContentItem> ContentItems { get; set; }
    }
}
