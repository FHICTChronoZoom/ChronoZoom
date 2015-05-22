using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Models
{
    public class Timeline
    {
        public Timeline() 
        {
            this.FromIsCirca = false;
            this.ToIsCirca = false;
        }
        
        /// <summary>
        /// Unique identifier of the timeline
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the timeline
        /// </summary>
        [BsonElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// The Regime in which the timeline should appear
        /// </summary>
        [BsonElement("regime")]
        public string Regime { get; set; }

        /// <summary>
        /// The height of the timeline
        /// </summary>
        [BsonElement("height")]
        public decimal Height { get; set; }

        /// <summary>
        /// The starting point of the timeline
        /// </summary>
        [BsonElement("timeRange.from.year")]
        public decimal FromYear { get; set; }

        /// <summary>
        /// Boolean to determine of the starting point of the timeline
        /// is an estimation.
        /// If this is false, it will not be persisted.
        /// Default is false.
        /// </summary>
        [BsonElement("timeRange.from.isCirca")]
        [BsonIgnoreIfDefault]
        public Boolean FromIsCirca { get; set; }

        /// <summary>
        /// The end point of the timeline
        /// </summary>
        [BsonElement("timeRange.to.year")]
        public decimal ToYear { get; set; }

        /// <summary>
        /// Boolean to determine of the end point of the timeline
        /// is an estimation.
        /// If this is false, it will not be persisted.
        /// Default is false.
        /// </summary>
        [BsonElement("timeRange.to.isCirca")]
        [BsonIgnoreIfDefault]
        public Boolean ToIsCirca { get; set; }

        /// <summary>
        /// The depth of the timeline in the timeline tree.
        /// </summary>
        [BsonElement("depth")]
        public int Depth { get; set; }

        /// <summary>
        /// List of timelines which are childs of this timeline
        /// </summary>
        [BsonElement("timelines")]
        [BsonIgnoreIfDefault]
        public List<Timeline> Timelines { get; set; }

        /// <summary>
        /// List of exhibits which are childs of this timeline
        /// </summary>
        [BsonElement("exhibits")]
        [BsonIgnoreIfDefault]
        public List<Guid> Exhibits { get; set; }
    }
}
