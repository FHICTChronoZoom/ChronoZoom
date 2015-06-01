using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Models;
using Chronozoom.Business.Models.Compability;
using Chronozoom.Business.Repositories;

namespace Chronozoom.Business.Services
{
    public class ExportService
    {
        private ITimelineRepository timelineRepository;
        private IExhibitRepository exhibitRepository;

        public ExportService(ITimelineRepository timelineRepository, IExhibitRepository exhibitRepository)
        {
            if (timelineRepository == null) throw new ArgumentNullException("timelineRepository");
            if (exhibitRepository == null) throw new ArgumentNullException("exhibitRepository");
            this.timelineRepository = timelineRepository;
            this.exhibitRepository = exhibitRepository;
        }

        public async Task<IEnumerable<FlatTimeline>> ExportTimeline(Guid topmostTimelineId)
        {
            var root = await timelineRepository.FindByIdAsync(topmostTimelineId);
            if (root == null)
                throw new Exception("The timeline, \"" + topmostTimelineId.ToString() + "\", you want to export, does not exist.");

            var timelines = new List<FlatTimeline>();
            await IterateTimelineAsync(timelines, null, root);
            return timelines;
        }

        public async Task<Exhibit> ExportExhibit(Guid exhibitId)
        {
            return await exhibitRepository.FindByIdAsync(exhibitId);
        }

        private async Task IterateTimelineAsync(List<FlatTimeline> timelines, Guid? parentId, Timeline timeline)
        {
            var flatTimeline = new FlatTimeline
            {
                ParentTimelineId = parentId,
                Timeline = timeline,
            };
            
            var exhibits = await exhibitRepository.GetByTimelineAsync(timeline.Id);
            flatTimeline.Exhibits = exhibits.ToList();

            // Add parent before child, avoid having to call .Reverse() = performance gain?
            timelines.Add(flatTimeline);
            var children = await timelineRepository.GetByTimelineAsync(timeline.Id);
            foreach (var child in children)
            {
                await IterateTimelineAsync(timelines, timeline.Id, child);
            }
        }
    }
}
