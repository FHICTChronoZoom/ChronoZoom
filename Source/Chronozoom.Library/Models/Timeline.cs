using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Models
{
    /// <summary>
    /// A timeline in chronozoom.
    /// </summary>
    public class Timeline
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the regime.
        /// </summary>
        public string Regime { get; set; }
        /// <summary>
        /// Gets or sets the left year.
        /// </summary>
        public decimal FromYear { get; set; }
        /// <summary>
        /// Gets or sets if the left year is not precise.
        /// </summary>
        public bool FromIsCirca { get; set; }
        /// <summary>
        /// Gets or sets the right year.
        /// </summary>
        public decimal ToYear { get; set; }
        /// <summary>
        /// Gets or sets if the right year is not precise.
        /// </summary>
        public bool ToIsCirca { get; set; }
        /// <summary>
        /// Gets or sets the height (nullable).
        /// </summary>
        public decimal? Height { get; set; }
        /// <summary>
        /// Gets or sets the background image url.
        /// </summary>
        public string BackgroundUrl { get; set; }
        /// <summary>
        /// Gets or sets the aspect ration (width / height).
        /// </summary>
        public decimal? AspectRatio { get; set; }

        /// <summary>
        /// Create a new instance of Timeline.
        /// </summary>
        public Timeline()
        {
            this.Id = new Guid();
        }
    }
}
