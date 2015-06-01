using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Models
{
    /// <summary>
    /// A registered chronozoom user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the email adress.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the identity provider.
        /// </summary>
        public string IdentityProvider { get; set; }
        /// <summary>
        /// Gets or sets the name identifier.
        /// </summary>
        public string NameIdentifier { get; set; }

        /// <summary>
        /// Create a new instance of User.
        /// </summary>
        public User()
        {
            this.Id = new Guid();
        }
    }
}
