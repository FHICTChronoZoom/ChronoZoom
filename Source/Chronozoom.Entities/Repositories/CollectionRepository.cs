using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Repositories;
using System.Data.Entity;
using Chronozoom.Business.Models;
<<<<<<< .merge_file_a03988
using System.Data.Entity;
=======
>>>>>>> .merge_file_a08400

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
            return collection.ConvertAll(x => new Business.Models.Collection { Id = x.Id, Default = x.Default, IsPublicSearchable = x.PubliclySearchable, Theme = x.Theme, Title = x.Title });
        }

        public async Task<IEnumerable<Business.Models.Collection>> GetByUserAsync(Guid userId)
        {
            var collection = await storage.Collections.Where(x => x.User.Id == userId).ToListAsync();
            return collection.ConvertAll(x => new Business.Models.Collection { Id = x.Id, Default = x.Default, IsPublicSearchable = x.PubliclySearchable, Title = x.Title, Theme = x.Theme });
        }

        public async Task<Business.Models.Collection> GetUserDefaultAsync(Guid userId)
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

        public async Task<Business.Models.Collection> FindByIdAsync(Guid id)
        {
            var collection = await storage.Collections.FindAsync(id);
            return ToLibraryCollection(collection);
        }

        public async Task<bool> InsertAsync(Business.Models.Collection item)
        {
            var collection = ToEntityCollection(item);
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
            storage.Collections.Remove(collection);
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<Business.Models.Collection> GetByUserAndNameAsync(Guid userId, string collectionName)
        {
            throw new NotImplementedException();
        }

        private Business.Models.Collection ToLibraryCollection(Collection col)
        {
            return new Business.Models.Collection { Id = col.Id, Default = col.Default, IsPublicSearchable = col.PubliclySearchable, Theme = col.Theme, Title = col.Title };    
        }
        
        private Collection ToEntityCollection(Business.Models.Collection col)
        {
            return new Collection { Id = col.Id, Default = col.Default, PubliclySearchable = col.IsPublicSearchable, Theme = col.Theme, Title = col.Title };
        }

        public async Task<Business.Models.Collection> FindByTimelineIdAsync(Guid timelineId)
        {
            var collection = await storage.Collections.FindAsync(timelineId);
            return ToLibraryCollection(collection);
        }
<<<<<<< .merge_file_a03988


        public async Task<Business.Models.Collection> FindByNameAsync(string superCollection)
        {
            var collection = await storage.Collections.FirstOrDefaultAsync(x => x.SuperCollection.Title == superCollection) ;
            return ToLibraryCollection(collection) ;
        }
=======
>>>>>>> .merge_file_a08400
    }
}
