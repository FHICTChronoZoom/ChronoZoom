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

        public async Task<IEnumerable<Timeline>> GetRootTimelines(Guid collectionId)
        {
            return await timelineRepository.GetRootTimelines(collectionId);
        }

        /// <summary>
        /// Deletes a timeline with the specified Id
        /// </summary>
        /// <param name="superCollectionName">The superCollection name/User name whom the timeline belongs to</param>
        /// <param name="timelineRequest">The timeline to be deleted</param>
        public async void DeleteTimeline(string superCollectionName, Timeline timelineRequest) {
            await timelineRepository.DeleteAsync(timelineRequest.Id);
        }

        /// <summary>
        /// Overloaded method for deleting a timeline with the specified Id
        /// </summary>
        /// <param name="superCollectionName">The superCollection name/User name whom the timeline belongs to</param>
        /// <param name="collectionName">The name of the collection of which the timeline should be deleted (doesn't do anything for now)</param>
        /// <param name="timelineRequest">The timeline to be deleted</param>
        public async void DeleteTimeline(string superCollectionName, string collectionName, Timeline timelineRequest)
        {
            await timelineRepository.DeleteAsync(timelineRequest.Id);
        }
    }
}
