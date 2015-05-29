using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
using ChronoZoom.Mongo.PersistencyEngine;

namespace ChronoZoom.Mongo.Mapper
{
    class CollectionMapper : ICollectionRepository
    {
        private CollectionFactory factory;

        public CollectionMapper(CollectionFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IEnumerable<Chronozoom.Library.Models.Collection>> GetPublicCollectionsAsync()
        {
            List<Mongo.Models.Collection> collection = await factory.Find
            Chronozoom.Library.Models.User cUser = new Chronozoom.Library.Models.User
        }

        public Task<IEnumerable<Chronozoom.Library.Models.Collection>> GetByUserAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Chronozoom.Library.Models.Collection> GetByUserAndNameAsync(Guid userId, string collectionName)
        {
            throw new NotImplementedException();
        }

        public Task<Chronozoom.Library.Models.Collection> GetUserDefaultAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMemberAsync(Guid collectionId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Chronozoom.Library.Models.Collection> FindByTimelineIdAsync(Guid timelineId)
        {
            throw new NotImplementedException();
        }

        public Task<Chronozoom.Library.Models.Collection> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(Chronozoom.Library.Models.Collection item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Chronozoom.Library.Models.Collection item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
