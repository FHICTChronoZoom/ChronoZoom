using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
using System.Data.Entity;

namespace Chronozoom.Entities.Repositories
{
    public class ExhibitRepository : IExhibitRepository
    {
        private Storage storage;

        public ExhibitRepository(Storage storage)
        {
            this.storage = storage;
        }

        public async Task<IEnumerable<Library.Models.Exhibit>> GetByTimelineAsync(Guid timelineId)
        {
            var exhibit = await storage.Exhibits.Where(x => x.TimelineId);
            return exhibit;
        }

        public async Task<Library.Models.Exhibit> FindAsync(Guid id)
        {
            var exhibit = await storage.Exhibits.FindAsync(id);
            return ToLibraryExhibit(exhibit);
        }

        public async Task InsertAsync(Library.Models.Exhibit item)
        {
            var exhibit = ToEntityExhibit(item);
            storage.Exhibits.Add(exhibit);
            await storage.SaveChangesAsync();
        }

        public async Task UpdateAsync(Library.Models.Exhibit item)
        {
            var exhibit = await storage.Exhibits.FindAsync(item.Id);
            
            //Doubts
            exhibit.ContentItems = (ContentItem) item.ContentItems;
            exhibit.UpdatedBy = item.UpdatedBy;

            exhibit.Depth = item.Depth;
            exhibit.IsCirca = item.IsCirca;
            exhibit.Title = item.Title;
            exhibit.UpdatedTime = item.UpdatedTime;
            exhibit.Year = item.Year;
            await storage.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var exhibit = await storage.Exhibits.FindAsync(id);
            storage.Exhibits.Remove(exhibit);
            await storage.SaveChangesAsync();
        }

        private Library.Models.Exhibit ToLibraryExhibit(Exhibit exhibit)
        {
            return new Library.Models.Exhibit { Id = exhibit.Id, ContentItems = exhibit.ContentItems, Depth = exhibit.Depth, IsCirca = exhibit.IsCirca, Title = exhibit.Title, UpdatedTime = exhibit.UpdatedTime, UpdatedBy = exhibit.UpdatedBy, Year = exhibit.Year, TimelineId = exhibit.TimelineId };
        }

        private Exhibit ToEntityExhibit(Library.Models.Exhibit exhibit)
        {
            return new Exhibit { Id = exhibit.Id, ContentItems = exhibit.ContentItems, Depth = exhibit.Depth, IsCirca = exhibit.IsCirca, Title = exhibit.Title, UpdatedTime = exhibit.UpdatedTime, UpdatedBy = exhibit.UpdatedBy, Year = exhibit.Year, TimelineId = exhibit.TimelineId };
        }
    }
}
