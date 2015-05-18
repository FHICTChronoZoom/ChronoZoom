using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
using Chronozoom.Library.Models;
using Chronozoom.Library.Models.Compability;

namespace Chronozoom.Library.Services
{
    public class TimelineService
    {
        private ITimelineRepository timelineRepository;

        public TimelineService(ITimelineRepository timelineRepository)
        {
            if (timelineRepository == null) throw new ArgumentNullException("timelineRepository");
            this.timelineRepository = timelineRepository;
        }
    }
}
