using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Entities
{
    public class rCollection
    {
        public rCollection()
        {
            this.Id = Guid.NewGuid();
            this.Default = false;
            this.MembersAllowed = false;
            this.PubliclySearchable = false;
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
        /// User Object reference.
        /// This is included because the front-end requires a user object when retreiving
        /// timelines. This could and probably should be handled in a better way, but
        /// it works for now.
        /// </summary>
        public rUser User { get; set; }

        /// <summary>
        /// The Guid of the super collection of which this collection is part of
        /// </summary>
        public Guid superCollectionId { get; set; }

        public rSuperCollection SuperCollection { get; set }

        /// <summary>
        /// Boolean which states if this is the default collection within a super collection.
        /// Only one collection should be default within a super collection.
        /// Default is false;
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Boolean to determine if this collection is open to edit for a list of members.
        /// Default is false;
        /// </summary>
        public bool MembersAllowed { get; set; }

        /// <summary>
        /// A list of users who have special rights to the collection, other than the collection owner.
        /// </summary>
        public virtual Collection<Member> Members { get; set; }

        /// <summary>
        /// Boolena to determine if this collection is publically searchable.
        /// Default is false;
        /// </summary>
        public bool PubliclySearchable { get; set; }

        /// <summary>
        /// Title of the collection
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL-sanitized version of title, which will be used as part of a URL path.
        /// Only a-z, 0-9 and hyphen are allowed. This should be unique per supercollection.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Theme of the collection
        /// </summary>
        public string rTheme { get; set; }

        /// <summary>
        /// A list of timelines which are part of this collection
        /// </summary>
        public List<rTimeline> timelines { get; set; }
    }

    /// <summary>
    /// The Collection.Theme text field is actually a compressed JSON object comprising of the following.
    /// </summary>
    public class rTheme
    {
        public string backgroundUrl { get; set; }
        public string backgroundColor { get; set; }
        public string timelineColor { get; set; }
        public string timelineStrokeStyle { get; set; }
        public string infoDotfillColor { get; set; }
        public string infoDotBorderColor { get; set; }
        public Boolean kioskMode { get; set; }
    }
}
