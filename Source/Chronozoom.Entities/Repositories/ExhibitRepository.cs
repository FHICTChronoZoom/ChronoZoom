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
            var exhibits = await storage.Timelines.Where(x => x.Id == timelineId).SelectMany(x => x.Exhibits).ToListAsync();
            return exhibits.ConvertAll(x => new Library.Models.Exhibit
            {
                Id = x.Id,
                ContentItems = x.ContentItems.Select(c => new Library.Models.ContentItem { Id = c.Id, Attribution = c.Attribution, Caption = c.Caption, Depth = c.Depth, MediaSource = c.MediaSource, MediaType = c.MediaType, Order = c.Order, Title = c.Title, Uri = c.Uri, Year = c.Year }).ToList(),
                Depth = x.Depth,
                IsCirca = x.IsCirca,
                Title = x.Title,
                UpdatedTime = x.UpdatedTime,
                UpdatedBy = new Library.Models.User { Id = x.UpdatedBy.Id, DisplayName = x.UpdatedBy.DisplayName,  Email = x.UpdatedBy.Email, IdentityProvider = x.UpdatedBy.IdentityProvider, NameIdentifier = x.UpdatedBy.NameIdentifier },
                Year = x.Year,
                TimelineId = timelineId
            });
        }

        public async Task<Library.Models.Exhibit> FindAsync(Guid id)
        {
            var exhibit = await storage.Exhibits.FindAsync(id);
            if (exhibit != null)
            {
                var timeline = await storage.Timelines.FirstOrDefaultAsync(x => x.Exhibits.Contains(exhibit));
                return ToLibraryExhibit(exhibit, timeline.Id);
            }
            else
            {
                return null;
            }
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
            var user = await storage.Users.FindAsync(item.UpdatedBy.Id);

            exhibit.UpdatedBy = user;
            exhibit.Depth = item.Depth;
            exhibit.IsCirca = item.IsCirca;
            exhibit.Title = item.Title;
            exhibit.UpdatedTime = item.UpdatedTime;
            exhibit.Year = item.Year;

            foreach (var contentItem in item.ContentItems)
            {
                var getItem = exhibit.ContentItems.FirstOrDefault(x => x.Id == contentItem.Id);
                if (getItem == null)
                {
                    //Insert contentItem
                    exhibit.ContentItems.Add(new ContentItem { Depth = contentItem.Depth, Caption = contentItem.Caption, Attribution = contentItem.Attribution, MediaSource = contentItem.MediaSource, MediaType = contentItem.MediaType, Order = contentItem.Order, Title = contentItem.Title, Uri = contentItem.Uri, Year = contentItem.Year} );
                }
                else
                {
                    // update contentItem
                    getItem.Depth = contentItem.Depth;
                    getItem.Caption = contentItem.Caption;
                    getItem.Attribution = contentItem.Attribution;
                    getItem.MediaSource = contentItem.MediaSource;
                    getItem.MediaType = contentItem.MediaType;
                    getItem.Order = contentItem.Order;
                    getItem.Title = contentItem.Title;
                    getItem.Uri = contentItem.Uri;
                    getItem.Year = contentItem.Year;
                }

                if (!exhibit.ContentItems.Any(x => x.Id == contentItem.Id))
                {
                    var remove = exhibit.ContentItems.FirstOrDefault(x => x.Id == contentItem.Id);
                    exhibit.ContentItems.Remove(remove);
                }
            }

            await storage.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var exhibit = await storage.Exhibits.FindAsync(id);
            storage.Exhibits.Remove(exhibit);
            await storage.SaveChangesAsync();
        }

        private Library.Models.Exhibit ToLibraryExhibit(Exhibit exhibit, Guid TimelineId)
        {
            return new Library.Models.Exhibit { Id = exhibit.Id, 
                ContentItems = exhibit.ContentItems.Select(c => new Library.Models.ContentItem { Id = c.Id, Attribution = c.Attribution, Caption = c.Caption, Depth = c.Depth, MediaSource = c.MediaSource, MediaType = c.MediaType, Order = c.Order, Title = c.Title, Uri = c.Uri, Year = c.Year }).ToList(), 
                Depth = exhibit.Depth,
                IsCirca = exhibit.IsCirca, 
                Title = exhibit.Title, UpdatedTime = exhibit.UpdatedTime, 
                UpdatedBy = new Library.Models.User { Id = exhibit.UpdatedBy.Id, DisplayName = exhibit.UpdatedBy.DisplayName, Email = exhibit.UpdatedBy.Email, NameIdentifier = exhibit.UpdatedBy.NameIdentifier, IdentityProvider = exhibit.UpdatedBy.IdentityProvider }, 
                Year = exhibit.Year, 
                TimelineId = TimelineId };
        }

        private Exhibit ToEntityExhibit(Library.Models.Exhibit exhibit)
        {
            return new Exhibit { Id = exhibit.Id, ContentItems = exhibit.ContentItems, Depth = exhibit.Depth, IsCirca = exhibit.IsCirca, Title = exhibit.Title, UpdatedTime = exhibit.UpdatedTime, UpdatedBy = exhibit.UpdatedBy, Year = exhibit.Year, TimelineId = timelineId };
        }
    }
}
