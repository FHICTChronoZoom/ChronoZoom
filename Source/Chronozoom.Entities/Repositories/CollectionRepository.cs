using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
using System.Data.Entity;
using Chronozoom.Library.Models;

namespace Chronozoom.Entities.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private Storage storage;

        public CollectionRepository(Storage storage)
        {
            this.storage = storage;
        }

        public async Task<IEnumerable<Library.Models.Collection>> GetPublicCollectionsAsync()
        {
            var collection = await storage.Collections.Where(x => x.PubliclySearchable).ToListAsync();
            return collection.ConvertAll(x => new Library.Models.Collection { Id = x.Id, Default = x.Default, IsPublicSearchable = x.PubliclySearchable, Theme = x.Theme, Title = x.Title });
        }

        public async Task<IEnumerable<Library.Models.Collection>> GetByUserAsync(Guid userId)
        {
            var collection = await storage.Collections.Where(x => x.User.Id == userId).ToListAsync();
            return collection.ConvertAll(x => new Library.Models.Collection { Id = x.Id, Default = x.Default, IsPublicSearchable = x.PubliclySearchable, Title = x.Title, Theme = x.Theme });
        }

        public async Task<Library.Models.Collection> GetUserDefaultAsync(Guid userId)
        {
            var collection = await storage.Collections.FirstOrDefaultAsync(x => x.User.Id == userId && x.Default);
            if (collection == null ) {
                // TODO: ensure personal collection (Make a new default collection that is empty)
                return null;
            }
            else
                return ToLibraryCollection(collection);
        }

        public async Task<bool> IsMemberAsync(Guid collectionId, Guid userId)
        {
            var collection = await storage.Collections.FindAsync(collectionId);
            if (collection.User.Id == userId || collection.Members.Any(x => x.User.Id == userId))
                return true;
            else
                return false;
        }

        public async Task<Library.Models.Collection> FindAsync(Guid id)
        {
            var collection = await storage.Collections.FindAsync(id);
            return ToLibraryCollection(collection);
        }

        public async Task InsertAsync(Library.Models.Collection item)
        {
            var collection = ToEntityCollection(item);
            storage.Collections.Add(collection);
            await storage.SaveChangesAsync();
        }

        public async Task UpdateAsync(Library.Models.Collection item)
        {
            var collection = await storage.Collections.FindAsync(item.Id);
            collection.Default = item.Default;
            collection.PubliclySearchable = item.IsPublicSearchable;
            collection.Theme = item.Theme;
            collection.Title = item.Title;
            await storage.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var collection = await storage.Collections.FindAsync(id);
            storage.Collections.Remove(collection);
            await storage.SaveChangesAsync();
        }

        private Library.Models.Collection ToLibraryCollection(Collection col)
        {
            return new Library.Models.Collection { Id = col.Id, Default = col.Default, IsPublicSearchable = col.PubliclySearchable, Theme = col.Theme, Title = col.Title };    
        }

        private Collection ToEntityCollection(Library.Models.Collection col)
        {
            return new Collection { Id = col.Id, Default = col.Default, PubliclySearchable = col.IsPublicSearchable, Theme = col.Theme, Title = col.Title };
        }

        public async Task<Library.Models.Collection> FindByTimelineIdAsync(Guid timelineId)
        {
            var collection = await storage.Collections.FindAsync(timelineId);
            return ToLibraryCollection(collection);
        }
    }
}
