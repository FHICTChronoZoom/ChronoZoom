using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Models;
using Chronozoom.Library.Models.Compability;
using Chronozoom.Library.Repositories;

namespace Chronozoom.Library.Services
{
    public class ImportService
    {
        private ICollectionRepository collectionRepository;
        private ITimelineRepository timelineRepository;
        private IExhibitRepository exhibitRepository;

        public ImportService(ICollectionRepository collectionRepository, ITimelineRepository timelineRepository, IExhibitRepository exhibitRepository)
        {
            if (collectionRepository == null) throw new ArgumentNullException("collectionRepository");
            if (timelineRepository == null) throw new ArgumentNullException("timelineRepository");
            if (exhibitRepository == null) throw new ArgumentNullException("exhibitRepository");
            this.collectionRepository = collectionRepository;
            this.timelineRepository = timelineRepository;
            this.exhibitRepository = exhibitRepository;
        }

        /// <summary>
        /// Import a collection of timelines.
        /// </summary>
        /// <param name="intoTimelineId">The parent timeline to import into.</param>
        /// <param name="importContent">A list of timelines to import.</param>
        /// <param name="user">The user which started this action.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Thrown when importContent or user parameters are null.</exception>
        /// <exception cref="System.Exception">Thrown when the destination timeline is not found or does not have permission to alter the parent collection.</exception>
        public async Task ImportTimelinesAsync(Guid intoTimelineId, List<FlatTimeline> importContent, User user)
        {
            if (importContent == null)
                throw new ArgumentNullException("importContent");

            if (user == null)
                throw new ArgumentNullException("user");

            var timeline = await timelineRepository.FindAsync(intoTimelineId);
            if (timeline == null)
                throw new Exception("The destination timeline, \"" + intoTimelineId.ToString() + "\", where you want to paste to, does not exist.");

            var collection = await collectionRepository.GetByTimelineAsync(timeline.Id);

            var isMember = await collectionRepository.IsMemberAsync(collection.Id, user.Id);
            if (!isMember)
                throw new Exception("You do not have permission to alter the \"" + collection.Title + "\" collection.");

            var timestamp = DateTime.UtcNow;
            var newGuids = new Dictionary<Guid, Guid>();

            foreach (var flat in importContent)
            {
                // Topmost timeline to be imported - ensure that its date range fits within the target timeline date range
                if (!flat.ParentTimelineId.HasValue)
                {
                    if (flat.Timeline.FromYear < timeline.FromYear || flat.Timeline.ToYear > timeline.ToYear)
                    {
                        throw new Exception("Unable to paste \"" + flat.Timeline.Title + "\" into \"" + timeline.Title + "\", since " + flat.Timeline.Title + "'s date range exceeds that of " + timeline.Title + ".");
                    }
                    if (flat.Timeline.FromYear == timeline.FromYear && flat.Timeline.ToYear == timeline.ToYear)
                    {
                        throw new Exception("Unable to paste \"" + flat.Timeline.Title + "\" into \"" + timeline.Title + "\", since " + "both timelines' date ranges are identical.");
                    }
                }

                var guid = Guid.NewGuid();
                newGuids.Add(flat.Timeline.Id, guid);

                flat.Timeline.Id = guid;
                if (flat.ParentTimelineId.HasValue)
                {
                    flat.Timeline.ParentTimeline = newGuids[flat.Timeline.ParentTimeline.Value];
                }
                else
                {
                    flat.Timeline.ParentTimeline = timeline.Id;
                }


                foreach (var exhibit in flat.Exhibits)
                {
                    exhibit.Id = Guid.NewGuid();
                    exhibit.TimelineId = guid;
                    exhibit.UpdatedBy = user;
                    exhibit.UpdatedTime = timestamp;

                    foreach (var item in exhibit.ContentItems)
                    {
                        item.Id = Guid.NewGuid();
                    }
                }
            }
        }
    }
}
