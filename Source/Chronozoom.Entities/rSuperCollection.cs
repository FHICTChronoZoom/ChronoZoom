using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Chronozoom.Entities
{
    class rSuperCollection
    {
         /// <summary>
        /// Constructor used to set default values.
        /// </summary>
        public rSuperCollection()
        {
            this.Id = Guid.NewGuid();   // Don't use [DatabaseGenerated(DatabaseGeneratedOption.Identity)] on Id
        }

        /// <summary>
        /// The ID of the supercollection.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The path from the web root to the the supercollection.  Title must therefore have a globally unique value.
        /// Is programmatically derived as a URL-sanitized version of user's display name using a-z, 0-9 and hyphen only.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The user who owns the supercollection.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// A collection of collections that belong to the supercollection.
        /// </summary>
        public virtual Collection<Entities.rCollection> Collections { get; set; }
    }
}
