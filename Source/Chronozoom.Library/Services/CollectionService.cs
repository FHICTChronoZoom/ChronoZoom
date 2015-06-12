using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Chronozoom.Business.Services
{
    public class CollectionService
    {
        private ICollectionRepository collectionRepository;
        private IApplicationSettings appSettings;

        public CollectionService(ICollectionRepository collectionRepository, IApplicationSettings appSettings)
        {
            if (collectionRepository == null) throw new ArgumentNullException("collectionRepository");
            if (appSettings == null) throw new ArgumentNullException("appSettings");
            this.collectionRepository = collectionRepository;
            this.appSettings = appSettings;
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

        public async Task<Guid> CollectionIdOrDefaultAsync(string superCollectionName, string collectionPath)
        {
            Collection collection;
            string _defaultSuperCollection = appSettings.Get<string>("DefaultSuperCollection") ;
            Guid   _defaultCollectionId    = Guid.Empty;

            if (string.IsNullOrEmpty(superCollectionName))
            {
                // no supercollection specified so use default supercollection's default collection
                superCollectionName = _defaultSuperCollection;
                collectionPath = "";
            }

            if (superCollectionName == _defaultSuperCollection && collectionPath == "" && _defaultCollectionId != Guid.Empty)
            {
                // we're looking up the default supercollection's default collection and that info is cached already
                return _defaultCollectionId;
            }

            if (string.IsNullOrEmpty(collectionPath))
            {
                // no collection specified so use default collection for specified supercollection
                collection = await collectionRepository.FindByNameOrDefaultAsync(superCollectionName);
            }
            else
            {
                // collection specified so look it up within the specified supercollection
                collection = await collectionRepository.FindByNameOrDefaultAsync(superCollectionName);
            }

            if (collection == null)
            {
                // we were unable to find the requested collection

                if (superCollectionName == _defaultSuperCollection && collectionPath == "")
                {
                    // the collection we could not find is the default supercollection's collection
                    // this should not occur unless the database has an issue or web.config is not set up correctly
                    throw new Exception("Unable to locate the default supercollection's default collection.");
                }

                //// so return the default supercollection's default collection
                //return CollectionIdOrDefault(storage, _defaultSuperCollectionName, "");

                // indicate that collection could not be found
                return Guid.Empty;
            }

            if (superCollectionName == _defaultSuperCollection && collection.Default)
            {
                // we were looking up the default supercollection's collection so lets cache it for future use
                _defaultCollectionId = collection.Id;
            }

            return collection.Id;
        }
    }
}
