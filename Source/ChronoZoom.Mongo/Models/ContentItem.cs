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
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string caption { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Media media { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uri { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string attribution { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int order { get; set; }

        /// <summary>
        /// 
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
