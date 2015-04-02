using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronozoom.Entities
{
    public class rUser
    {
        public rUser() 
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// The Identifier of the user
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The display name of the user
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The email address of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The identity provider of the user. Used to verify the users' identity
        /// </summary>
        public string IdentityProvider { get; set; }

        /// <summary>
        /// Name identifier of the user.
        /// </summary>
        public string NameIdentifier { get; set; }
    }
}
