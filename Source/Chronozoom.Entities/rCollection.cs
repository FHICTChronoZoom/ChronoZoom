using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Entities
{
    class rCollection
    {
        public rCollection()
        {
            this.Id = new Guid();
        }

        /// <summary>
        /// Guid identifying the collection
        /// </summary>
        public Guid Id { get; set;  }

        /// <summary>
        /// The Guid of the user who owns this collection
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The Guid of the super collection of which this collection is part of
        /// </summary>
        public Guid superCollectionId { get; set; }

        /// <summary>
        /// Boolean which states if this is the default collection within a super collection.
        /// Only one collection should be default within a super collection
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Boolean to determine if this collection is open to edit for other members
        /// </summary>
        public bool MembersAllowed { get; set; }

        /// <summary>
        /// Boolena to determine if this collection is publically searchable
        /// </summary>
        public bool PubliclySearchable { get; set; }

        /// <summary>
        /// Title of the collection
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Theme of the collection
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// A list of timelines which are part of this collection
        /// </summary>
        public List<rTimeline> timelines { get; set; }
    }
}
