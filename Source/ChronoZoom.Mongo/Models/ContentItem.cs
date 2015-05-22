using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Models
{
 
    public class ContentItem
    {

        public ContentItem(){ }

        [BsonId]
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// Stores the title of the contentItem
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Stores the caption of the contentItem (small description)
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Stores the year of the contentItem
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Stores the mediatype and url of a contentItem
        /// </summary>
        //public Media Media { get; set; }

        /// <summary>
        /// Stores the link used to find this contentItem
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Stores the name of the person who curated the contentItem
        /// </summary>
        public string Attribution { get; set; }

        /// <summary>
        /// Ordernumber in the exhibit
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Inner class used to determine which kind of media type it is and where to find the source
        /// </summary>
        internal class Media
        {
            public Media() { }

            /// <summary>
            /// 
            /// </summary>
            public string type { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string source { get; set; }
        }
        

    }
}
