using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Models
{
    public class Collection
    {
       
        public Guid Id { get; set; }
        public bool Default { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public bool IsPublicSearchable { get; set; }

        public Collection()
        {
            this.Id = new Guid();
        }
    }
}
