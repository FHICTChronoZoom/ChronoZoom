using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Services
{
    public class CollectionService
    {
        private ICollectionRepository collectionRepository;

        public CollectionService(ICollectionRepository collectionRepository)
        {
            if (collectionRepository == null) throw new ArgumentNullException("collectionRepository");
            this.collectionRepository = collectionRepository;
        }

        public Task<Collection> FindCollectionAsync(Guid id)
        {
            return collectionRepository.FindByIdAsync(id);
        }

        public Task<IEnumerable<Collection>> FindPublicCollectionAsync()
        {
            return collectionRepository.GetPublicCollectionsAsync();
        }

        public Task<IEnumerable<Collection>> FindUserCollectionsAsync(Guid userId)
        {
            return collectionRepository.GetByUserAsync(userId);
        }


    }
}
