using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Repositories;
using Chronozoom.Business.Models;
using Chronozoom.Business.Models.Compability;

namespace Chronozoom.Business.Services
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
