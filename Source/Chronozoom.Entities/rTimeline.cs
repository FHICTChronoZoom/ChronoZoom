using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Chronozoom.Entities
{
    class rTimeline
    {
        public rTimeline()
        {
            this.Id = Guid.NewGuid();
            this.FromIsCirca = false;
            this.ToIsCirca = false;
        }

        /// <summary>
        /// Guid identifying the timeline
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Integer determining the depth of the timeline.
        /// Used by the frontend to render
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// The title of the timeline
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The regime the timeline is part of
        /// One of the hardcoded values within the application, somewhere.
        /// </summary>
        public string Regime { get; set; }

        /// <summary>
        /// Integer which determines where the timeline will start
        /// </summary>
        public decimal FromYear { get; set; }

        /// <summary>
        /// Integer which determines where the timeline will end
        /// </summary>
        public decimal ToYear { get; set; }

        /// <summary>
        /// No bloody clue.
        /// </summary>
        public decimal ForkNode { get; set; }

        /// <summary>
        /// The height of the timeline.
        /// The question mark makes this field nullable.
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// Another Guid for the timeline. Sam knows why.
        /// </summary>
        public Guid TimeLineId { get; set; }

        /// <summary>
        /// The CollectionId which this timeline is part of
        /// </summary>
        public Guid CollectionId { get; set; }

        /// <summary>
        /// The number of content items contained in subtree under current timeline.
        /// </summary>
        public int SubtreeSize { get; set; }

        /// <summary>
        /// Boolean which determines if the FromYear field is an approximation.
        /// Default is false
        /// </summary>
        public bool FromIsCirca { get; set; }

        /// <summary>
        /// Boolean which determines if the ToYear field is an approximation
        /// Default is false
        /// </summary>
        public bool ToIsCirca { get; set; }

        /// <summary>
        /// The URL of the background image
        /// </summary>
        public string BackgroundUrl { get; set; }

        /// <summary>
        /// The Aspect ratio of the timeline
        /// Aspect ratio = (width / height)
        /// </summary>
        public decimal? AspectRatio { get; set; }

        /// <summary>
        /// The child timelines belonging to this timeline
        /// </summary>
        public virtual Collection<rTimeline> ChildTimelines { get; set; }

        /// <summary>
        /// The exhibits belonging to this timeline
        /// </summary>
        public virtual Collection<Exhibit> Exhibits { get; set; }
    }


}
