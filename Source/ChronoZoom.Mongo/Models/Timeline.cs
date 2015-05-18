using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Models
{
    class Timeline
    {
        public Timeline() 
        {
            this.FromIsCirca = false;
            this.ToIsCirca = false;
        }
        
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("regime")]
        public string Regime { get; set; }

        [BsonElement("height")]
        public decimal Height { get; set; }

        [BsonElement("timeRange.from.year")]
        public decimal FromYear { get; set; }

        [BsonElement("timeRange.from.isCirca")]
        [BsonIgnoreIfDefault]
        public Boolean FromIsCirca { get; set; }

        [BsonElement("timeRange.to.year")]
        public decimal ToYear { get; set; }

        [BsonElement("timeRange.to.isCirca")]
        [BsonIgnoreIfDefault]
        public Boolean ToIsCirca { get; set; }

        [BsonElement("depth")]
        public int Depth { get; set; }

        [BsonElement("timelines")]
        [BsonIgnoreIfDefault]
        public List<Timeline> timelines { get; set; }

        [BsonElement("exhibits")]
        [BsonIgnoreIfDefault]
        public List<Exhibit> exhibits { get; set; }
    }
}
