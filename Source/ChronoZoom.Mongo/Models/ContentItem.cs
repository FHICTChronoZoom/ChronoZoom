using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoZoom.Mongo.Models
{
 
    class ContentItem
    {

        public ContentItem(){ }

        /// <summary>
        /// 
        /// </summary>
        public int depth { get; set; }

        /// <summary>
        /// Stores the title of the contentItem
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Stores the caption of the contentItem (small description)
        /// </summary>
        public string caption { get; set; }

        /// <summary>
        /// Stores the year of the contentItem
        /// </summary>
        public int year { get; set; }

        /// <summary>
        /// Stores the mediatype and url of a contentItem
        /// </summary>
        public Media media { get; set; }

        /// <summary>
        /// Stores the link used to find this contentItem
        /// </summary>
        public string uri { get; set; }

        /// <summary>
        /// Stores the name of the person who curated the contentItem
        /// </summary>
        public string attribution { get; set; }

        /// <summary>
        /// Ordernumber in the exhibit
        /// </summary>
        public int order { get; set; }

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
