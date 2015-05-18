using Chronozoom.Library.Repositories;
using ChronoZoom.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.PersistencyEngine
{
    public class ExhibitFactory : IExhibitRepository
    {
        private ExhibitFactory() { }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("depth")]
        public int Depth { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("year")]
        public decimal Year { get; set; }

        [BsonElement("isCirca")]
        [BsonIgnoreIfDefault]
        public bool IsCirca { get; set; }

        [BsonElement("updated.userId")]
        [BsonIgnoreIfDefault]
        public ObjectId UpdatedByUser { get; set; }

        [BsonElement("updated.timestamp")]
        [BsonIgnoreIfDefault]
        public DateTime UpdatedAt { get; set; }

        [BsonElement("contentItems")]
        [BsonIgnoreIfDefault]
        public List<ContentItem> ContentItems {get; set; }
    }
}
