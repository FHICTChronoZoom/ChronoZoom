using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Models
{
    /// <summary>
    /// A content item in an exhibit.
    /// </summary>
    public class ContentItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the depth in the timeline tree.
        /// </summary>
        public int Depth { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets the sets the caption.
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public decimal? Year { get; set; }
        /// <summary>
        /// Gets or sets the type of media.
        /// </summary>
        public string MediaType { get; set; }
        /// <summary>
        /// Gets or sets the URL of the content.
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// Gets or sets the source of the content.
        /// </summary>
        public string MediaSource { get; set; }
        /// <summary>
        /// Getsor sets the attribution.
        /// </summary>
        public string Attribution { get; set; }
        /// <summary>
        /// Gets or sets the order in which the item should appear.
        /// </summary>
        public short? Order { get; set; }

        /// <summary>
        /// Create a new instance of Content item.
        /// </summary>
        public ContentItem()
        {
            this.Id = new Guid();
        }
    }
}
