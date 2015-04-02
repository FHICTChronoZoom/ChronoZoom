using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentEntities
{
    class Userd
    {

        public Userd()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string IdentityProvider { get; set; }

        public string NameIdentifier { get; set; }
    }
    }
}
