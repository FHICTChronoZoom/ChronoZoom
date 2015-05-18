using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
using System.Data.Entity;

namespace Chronozoom.Entities.Repositories
{
    public class TimeLineRepository : ITimelineRepository
    {
        private Storage storage;

        public TimeLineRepository(Storage storage)
        {
            this.storage = storage;
        }

        public async Task<IEnumerable<Library.Models.Timeline>> GetByCollectionAsync(Guid collectionId)
        {
            
        }

        public async Task<IEnumerable<Library.Models.Timeline>> GetByTimelineAsync(Guid timelineId)
        {
            throw new NotImplementedException();
        }

        public async Task<Library.Models.Timeline> FindAsync(Guid id)
        {
            var timeline = await storage.Timelines.FindAsync(id);
            return ToLibraryTimeLine(timeline);
        }

        public async Task InsertAsync(Library.Models.Timeline item)
        {
            var timeline = ToEntityTimeline(item);
            storage.Timelines.Add(timeline);
            await storage.SaveChangesAsync();
        }

        public async Task UpdateAsync(Library.Models.Timeline item)
        {
            var timeline = await storage.Exhibits.FindAsync(item.Id);

            timeline.Title = item.Title;
            timeline.IsCirca = item.ToIsCirca;

            timeline.Regime = item.Regime;
            timeline.FromYear = item.FromYear;
            timeline.FromIsCirca = item.FromIsCirca;
            timeline.ToYear = item.ToYear;
            timeline.ToIsCirca = item.ToIsCirca;
            timeline.Height = item.Height;
            timeline.BackgroundUrl = item.BackgroundUrl;
            timeline.AspectRatio = item.AspectRatio;

            await storage.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var timeline = await storage.Timelines.FindAsync(id);
            storage.Timelines.Remove(timeline);
            await storage.SaveChangesAsync();
        }

        private Library.Models.Timeline ToLibraryTimeLine(Timeline timeline)
        {
            // NO PARENTID
            return new Library.Models.Timeline { Id = timeline.Id, Title = timeline.Title, Regime = timeline.Regime, FromYear = timeline.FromYear, FromIsCirca = timeline.FromIsCirca, ToYear = timeline.ToYear, ToIsCirca = timeline.ToIsCirca, Height = timeline.Height, BackgroundUrl = timeline.BackgroundUrl, AspectRatio = timeline.AspectRatio };
        }

        private Timeline ToEntityTimeline(Library.Models.Timeline timeline)
        {
            return new Timeline { Id = timeline.Id, Title = timeline.Title, Regime = timeline.Regime, FromYear = timeline.FromYear, FromIsCirca = timeline.FromIsCirca, ToYear = timeline.ToYear, ToIsCirca = timeline.ToIsCirca, Height = timeline.Height, BackgroundUrl = timeline.BackgroundUrl, AspectRatio = timeline.AspectRatio };
        }
    
    }
}
