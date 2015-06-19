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

        public async Task<bool> DeleteCollection(String superCollectionPath, String collectionPath)
        {
            return await collectionRepository.DeleteAsync(collectionPath);
        }

        public async Task<Guid> PutCollection(String superCollectionName, Collection collectionRequest)
        {
            Boolean done = await collectionRepository.UpdateAsync(collectionRequest);
            if (done)
            {
                return collectionRequest.Id;
            }
            return Guid.Empty;
        }

        public async Task<Guid> PutCollection(String superCollectionName,String collectionName, Collection collectionRequest)
        {
            Boolean done = await collectionRepository.UpdateAsync(collectionRequest);
            if (done)
            {
                return collectionRequest.Id;
            }
            return Guid.Empty;
        }

        public async Task<Boolean> PostCollection(String superCollectionPath, String newCollectionPath, Collection newCollectionData)
        {
            return await collectionRepository.InsertAsync(newCollectionData);
        }

        /// <summary>
        /// Checks if a user is a member of the collection and therefore has member priviliges
        /// 
        /// WARNING!! This function has too little parameters to actually function. 
        /// Because of that I hardcoded the user Id parameter.
        /// Also, the collectionId should be delivered as a Guid in stead of a string.
        /// </summary>
        /// <param name="collectionId">The collection to check membership for</param>
        /// <returns>Boolean - True if the user has membership priviliges, otherwise false</returns>
        public async Task<bool> UserIsMember(string collectionId)
        {
            return await collectionRepository.IsMemberAsync(new Guid(collectionId), new Guid());
        }

        /// <summary>
        /// Referencing UserIsMember documentation above. This will not work
        /// </summary>
        /// <param name="superCollectionName"></param>
        /// <returns></returns>
        public async Task<bool> UserCanEdit(string superCollectionName)
        {
            return await UserIsMember(superCollectionName);
        }

        /// <summary>
        /// Referencing UserIsMember documentation above. This will not work
        /// </summary>
        /// <param name="superCollectionName"></param>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public async Task<bool> UserCanEdit(string superCollectionName, string collectionName)
        {
            return await UserIsMember(superCollectionName);
        }


    }
}
