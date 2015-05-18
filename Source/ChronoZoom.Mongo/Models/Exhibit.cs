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
        public Exhibit() { }

        /// <summary>
        /// 
        /// </summary>
        [BsonId]
        public ObjectId id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int depth { get; set; }

        /// <summary>
        /// The title of the exhibit
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Contains the information about the date the exhibit occurs
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// Determines if the exhibit is on an exact date or circa
        /// </summary>
        public bool isCirca { get; set; }

        /// <summary>
        /// Contains the information about edits in the exhibit
        /// </summary>
        //public Updated updated { get; set; }

        /// <summary>
        /// Used to store the contentItems which are in the exhibit
        /// </summary>
        public List<ContentItem> contentItems { get; set; } 


        /// <summary>
        /// 
        /// </summary>
        internal class Updated
        {
 
            public Updated(){}

            /// <summary>
            /// 
            /// </summary>
            public ObjectId id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string timestamp { get; set; }
        }
    }
}
