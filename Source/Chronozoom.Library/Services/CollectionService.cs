using Chronozoom.Library.Models;
using Chronozoom.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Services
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
            return collectionRepository.FindAsync(id);
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
