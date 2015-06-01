using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Models
{
    public class Tour
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int UniqueId { get; set; }

        public string AudioBlobUrl { get; set; }

        public string Category {get; set;}

        public int Sequence { get; set; }

        public Tour()
        {
            this.Id = new Guid();
        }
    }
}
