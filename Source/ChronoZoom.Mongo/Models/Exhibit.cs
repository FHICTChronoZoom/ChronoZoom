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

    class Exhibit
    {
        public Exhibit() { }

        [BsonId]
        public ObjectId id { get; set; }
        public int depth { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public bool isCirca { get; set; }

        public Updated updated { get; set; }
        public List<ContentItem> contentItems { get; set; } 


        internal class Updated
        {
            public Updated(){}

            public ObjectId id { get; set; }
            public string timestamp { get; set; }
        }
    }
}
