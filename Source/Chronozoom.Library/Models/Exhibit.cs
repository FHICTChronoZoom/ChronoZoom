using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Models
{
    public class Exhibit
    {
        public Guid Id { get; set; }
        public List<ContentItem> ContentItems { get; set; }
    }
}
