using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Models
{
    /// <summary>
    /// An exhibit contains contentitems in a Timeline.
    /// </summary>
    public class Exhibit
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the id of the timeline this exhibit belongs to.
        /// </summary>
        public Guid TimelineId { get; set; }
        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        public int Depth { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        public decimal Year { get; set; }
        /// <summary>
        /// Gets or sets if the year is not precise.
        /// </summary>
        public bool IsCirca { get; set; }
        /// <summary>
        /// Gets or sets the last user who updated the exhibit.
        /// </summary>
        public User UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the time on which this exhibit has been edited last.
        /// </summary>
        public DateTime UpdatedTime { get; set; }
        /// <summary>
        /// Gets or sets the content items in this exhibit.
        /// </summary>
        public ICollection<ContentItem> ContentItems { get; set; }

        /// <summary>
        /// Create a new instance of Exhibit.
        /// </summary>
        public Exhibit()
        {
            this.Id = new Guid();
        }
    }
}
