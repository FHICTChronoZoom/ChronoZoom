using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Models.Compability
{
    public class FlatTimeline
    {
        public Guid? ParentTimelineId { get; set; }
        public Timeline Timeline { get; set; }
        public List<Exhibit> Exhibits { get; set; }
    }
}
