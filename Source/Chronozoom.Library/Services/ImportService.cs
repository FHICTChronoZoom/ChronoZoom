using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// Import a collection of timelines in a new collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="collectionTitle"></param>
        /// <param name="collectionTheme"></param>
        /// <param name="timelines"></param>
        /// <param name="tours"></param>
        /// <param name="user"></param>
        /// <param name="makeDefault"></param>
        /// <param name="forcePublic"></param>
        /// <param name="keepOldGuids"></param>
        /// <returns></returns>
        public async Task ImportCollection(string collectionTitle, string collectionTheme, List<FlatTimeline> importContent, List<Tour> tours,
            User user, bool makeDefault = false, bool forcePublic = false, bool keepOldGuids = false)
        {
            if (user == null)
                throw new ArgumentNullException("user", "In order to import a collection, you must first be logged in.");

            string path = Regex.Replace(collectionTitle.Trim(), @"[^A-Za-z0-9\-]+", "").ToLower();

            var existing = await collectionRepository.GetByUserAndNameAsync(user.Id, collectionTitle);
            if (existing != null)
                throw new Exception("A collection with this name already exists in the user's collection.");

            if (path.Length > 50)
                throw new Exception("The name of the collection is to long.");

            var collection = new Collection
            {
                UserId = user.Id,
                Default = makeDefault,
                Title = collectionTitle,
                Theme = collectionTheme,
                IsPublicSearchable = forcePublic,
            };

            // TODO : Check if item has been succesfully inserted into the data store, in future commit the IRepository methods will return a Task<bool>.
            await collectionRepository.InsertAsync(collection);


            var timestamp = DateTime.UtcNow;
            var newGuids = new Dictionary<Guid, Guid>();
            foreach (var flat in importContent)
            {
                var guid = Guid.NewGuid();
                newGuids.Add(flat.Timeline.Id, guid);

                flat.Timeline.Id = guid;
                if (flat.ParentTimelineId.HasValue)
                {
                    flat.Timeline.ParentTimeline = newGuids[flat.Timeline.ParentTimeline.Value];
                }

                await timelineRepository.InsertAsync(flat.Timeline);

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

                    await exhibitRepository.InsertAsync(exhibit);
                }
            }
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
                throw new Exception("In order to change a timeline, you must first be logged in.");

            var timeline = await timelineRepository.FindAsync(intoTimelineId);
            if (timeline == null)
                throw new Exception("The destination timeline, \"" + intoTimelineId.ToString() + "\", where you want to paste to, does not exist.");

            var collection = await collectionRepository.FindByTimelineIdAsync(timeline.Id);

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

                await timelineRepository.InsertAsync(flat.Timeline);

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

                    await exhibitRepository.InsertAsync(exhibit);
                }
            }
        }

        public async Task ImportExhibit(Guid intoTimelineId, Exhibit newExhibit, User user)
        {
            if (user == null)
                throw new Exception("In order to change a timeline, you first must be logged in.");

            var target = await timelineRepository.FindAsync(intoTimelineId);
            var timestamp = DateTime.UtcNow;

            if (target == null)
                throw new Exception("The destination timeline, \"" + intoTimelineId.ToString() + "\", where you want to paste to, does not exist.");

            var collection = await collectionRepository.FindAsync(target.Id);
            var isMember = await collectionRepository.IsMemberAsync(collection.Id, user.Id);
            if(!isMember)
                throw new Exception("You do not have permission to alter the \"" + collection.Title + "\" collection.");

            if (newExhibit.Year < target.FromYear || newExhibit.Year > target.ToYear)
                throw new Exception("Unable to paste \"" + newExhibit.Title + "\" into \"" + target.Title + "\", since " +
                        newExhibit.Title + "'s date exceeds that of " + target.Title + ".");

            newExhibit.Id = Guid.NewGuid();
            newExhibit.TimelineId = intoTimelineId;
            foreach (var item in newExhibit.ContentItems)
            {
                item.Id = Guid.NewGuid();
            }

            await exhibitRepository.InsertAsync(newExhibit);
        }
    }
}
