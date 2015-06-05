using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Repositories;
using System.Data.Entity;

namespace Chronozoom.Entities.Repositories
{
    public class TimelineRepository : ITimelineRepository
    {
        private Storage storage;

        public TimelineRepository(Storage storage)
        {
            this.storage = storage;
        }

        public async Task<IEnumerable<Business.Models.Timeline>> GetByCollectionAsync(Guid collectionId)
        {
            var timelines = await storage.Timelines.Where(x => x.Id == collectionId).ToListAsync();

            return await MakeTimelineList(timelines);
        }

        public async Task<IEnumerable<Business.Models.Timeline>> GetByTimelineAsync(Guid timelineId)
        {
            var timelines = await storage.Timelines.Where(x => x.Id == timelineId).ToListAsync();

            return await MakeTimelineList(timelines);
        }

        public async Task<Business.Models.Timeline> FindByIdAsync(Guid id)
        {
            var timeline = await storage.Timelines.FindAsync(id);
            return ToLibraryTimeLine(timeline);
        }

        public async Task<bool> InsertAsync(Business.Models.Timeline item)
        {
            var timeline = ToEntityTimeline(item);
            storage.Timelines.Add(timeline);
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Business.Models.Timeline item)
        {
            var timeline = await storage.Timelines.FindAsync(item.Id);

            timeline.Title = item.Title;
            timeline.ToIsCirca = item.ToIsCirca;
            timeline.Regime = item.Regime;
            timeline.FromYear = item.FromYear;
            timeline.FromIsCirca = item.FromIsCirca;
            timeline.ToYear = item.ToYear;
            timeline.ToIsCirca = item.ToIsCirca;
            timeline.Height = item.Height;
            timeline.BackgroundUrl = item.BackgroundUrl;
            timeline.AspectRatio = item.AspectRatio;

            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var timeline = await storage.Timelines.FindAsync(id);
            storage.Timelines.Remove(timeline);
            return await storage.SaveChangesAsync() > 0;
        }

        private Business.Models.Timeline ToLibraryTimeLine(Timeline timeline)
        {
            // NO PARENTID
            return new Business.Models.Timeline { Id = timeline.Id, Title = timeline.Title, Regime = timeline.Regime, FromYear = timeline.FromYear, FromIsCirca = timeline.FromIsCirca, ToYear = timeline.ToYear, ToIsCirca = timeline.ToIsCirca, Height = timeline.Height, BackgroundUrl = timeline.BackgroundUrl, AspectRatio = timeline.AspectRatio };
        }

        private Timeline ToEntityTimeline(Business.Models.Timeline timeline)
        {
            return new Timeline { Id = timeline.Id, Title = timeline.Title, Regime = timeline.Regime, FromYear = timeline.FromYear, FromIsCirca = timeline.FromIsCirca, ToYear = timeline.ToYear, ToIsCirca = timeline.ToIsCirca, Height = timeline.Height, BackgroundUrl = timeline.BackgroundUrl, AspectRatio = timeline.AspectRatio };
        }

        private async Task<IEnumerable<Chronozoom.Business.Models.Timeline>> MakeTimelineList(List<Timeline> timelines)
        {
            var listOfTimelines = new List<Chronozoom.Business.Models.Timeline>();

            foreach (var timeline in timelines)
            {
                var parent = await storage.Timelines.FirstOrDefaultAsync(x => x.ChildTimelines.Contains(timeline));

                listOfTimelines.Add(new Business.Models.Timeline
                {
                    AspectRatio = timeline.AspectRatio,
                    BackgroundUrl = timeline.BackgroundUrl,
                    FromIsCirca = timeline.FromIsCirca,
                    FromYear = timeline.FromYear,
                    Height = timeline.Height,
                    Id = timeline.Id,
                    Regime = timeline.Regime,
                    Title = timeline.Title,
                    ToIsCirca = timeline.ToIsCirca,
                    ToYear = timeline.ToYear,
                    ParentTimeline = parent.Id
                });
            }

            return listOfTimelines;
        }


        public async Task<IEnumerable<Business.Models.Timeline>> GetRootTimelines(Guid collectionId)
        {
            var timelines = await storage.Timelines.Where(x => x.Collection.Id == collectionId).ToListAsync();
            return await MakeTimelineList(timelines);
        }
    }
}
