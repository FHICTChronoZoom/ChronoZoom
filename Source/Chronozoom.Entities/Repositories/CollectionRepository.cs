using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Repositories;
using System.Data.Entity;
using Chronozoom.Business.Models;

namespace Chronozoom.Entities.Repositories
{
    public class CollectionRepository : ICollectionRepository
    {
        private Storage storage;

        public CollectionRepository(Storage storage)
        {
            this.storage = storage;
        }

        public async Task<IEnumerable<Business.Models.Collection>> GetPublicCollectionsAsync()
        {
            var collection = await storage.Collections.Where(x => x.PubliclySearchable).ToListAsync();
            return collection.ConvertAll(ToModel);
        }

        public async Task<IEnumerable<Business.Models.Collection>> GetByUserAsync(Guid userId)
        {
            var collection = await storage.Collections.Where(x => x.User.Id == userId).ToListAsync();
            return collection.ConvertAll(ToModel);
        }

        public async Task<Business.Models.Collection> GetUserDefaultAsync(Guid userId)
        {
            var collection = await storage.Collections.FirstOrDefaultAsync(x => x.User.Id == userId && x.Default);
            if (collection == null)
            {
                // TODO: ensure personal collection (Make a new default collection that is empty)
                return null;
            }
            else
                return ToModel(collection);
        }

        public async Task<bool> IsMemberAsync(Guid collectionId, Guid userId)
        {
            var collection = await storage.Collections.FindAsync(collectionId);
            if (collection.User.Id == userId || collection.Members.Any(x => x.User.Id == userId))
                return true;
            else
                return false;
        }

        public async Task<Business.Models.Collection> FindByIdAsync(Guid id)
        {
            var collection = await storage.Collections.FindAsync(id);
            return ToModel(collection);
        }

        public async Task<bool> InsertAsync(Business.Models.Collection item)
        {
            var collection = ToEntity(item);
            storage.Collections.Add(collection);
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Business.Models.Collection item)
        {
            var collection = await storage.Collections.FindAsync(item.Id);
            collection.Default = item.Default;
            collection.PubliclySearchable = item.IsPublicSearchable;
            collection.Theme = item.Theme;
            collection.Title = item.Title;
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var collection = await storage.Collections.FindAsync(id);
            var tours = await storage.Tours.Where(x => x.Collection.Id == id).ToListAsync();
            foreach (Tour t in tours)
            {
                foreach (Bookmark b in t.Bookmarks)
                {
                    storage.Bookmarks.Remove(b);
                }
                storage.Tours.Remove(t);
            }
            storage.Collections.Remove(collection);
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<Business.Models.Collection> GetByUserAndNameAsync(Guid userId, string collectionName)
        {
            var collection = await storage.Collections.FirstOrDefaultAsync(x => x.User.Id == userId && x.Title == collectionName);
            return collection == null ? null : ToModel(collection);
        }

        public async Task<Business.Models.Collection> FindByTimelineIdAsync(Guid timelineId)
        {
            var collection = await storage.Timelines.Where(x => x.Id == timelineId).Select(x => x.Collection).FirstOrDefaultAsync();
            return ToModel(collection);
        }

        public async Task<Business.Models.Collection> FindByNameOrDefaultAsync(string superCollection)
        {
            var collection = await storage.Collections.FirstOrDefaultAsync(x => x.SuperCollection.Title == superCollection);
            if(collection ==null)
            {
                collection = await storage.Collections.Where(x => x.Default).FirstOrDefaultAsync();
            }

            return ToModel(collection);
        }

        private Business.Models.Collection ToModel(Collection col)
        {
            return new Business.Models.Collection { Id = col.Id, Default = col.Default, IsPublicSearchable = col.PubliclySearchable, Theme = col.Theme, Title = col.Title };
        }

        private Collection ToEntity(Business.Models.Collection col)
        {
            return new Collection { Id = col.Id, Default = col.Default, PubliclySearchable = col.IsPublicSearchable, Theme = col.Theme, Title = col.Title };
        }
    }
}
