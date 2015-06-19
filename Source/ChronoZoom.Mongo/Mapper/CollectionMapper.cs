using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;
using ChronoZoom.Mongo.PersistencyEngine;

namespace ChronoZoom.Mongo.Mapper
{
    public class CollectionMapper : ICollectionRepository
    {
        private CollectionFactory factory;

        public CollectionMapper(CollectionFactory factory)
        {
            this.factory = factory;
        }

        public async Task<IEnumerable<Chronozoom.Business.Models.Collection>> GetPublicCollectionsAsync()
        {
            List<Mongo.Models.Collection> collection = await factory.FindPublicCollectionsAsync();

            List<Chronozoom.Business.Models.Collection> mappedCollections = mapCollections(collection);

            return mappedCollections;

        }

        public async Task<IEnumerable<Chronozoom.Business.Models.Collection>> GetByUserAsync(Guid userId)
        {
            List<Mongo.Models.Collection> collection = await factory.FindByUserIdAsync(userId);

            List<Chronozoom.Business.Models.Collection> mappedCollections = mapCollections(collection);

            return mappedCollections;
        }

        public async Task<Chronozoom.Business.Models.Collection> GetByUserAndNameAsync(Guid userId, string collectionName)
        {
            Mongo.Models.Collection collection = await factory.FindByUserIdAndName(userId, collectionName);

            Chronozoom.Business.Models.Collection mappedCollection = mapCollection(collection);

            return mappedCollection;
        }

        public async Task<Chronozoom.Business.Models.Collection> GetUserDefaultAsync(Guid userId)
        {
            Mongo.Models.Collection collection = await factory.FindUserDefaultCollectionAsync(userId);

            Chronozoom.Business.Models.Collection mappedCollection = mapCollection(collection);

            return mappedCollection;
        }

        public async Task<bool> IsMemberAsync(Guid collectionId, Guid userId)
        {
            return await factory.IsMemberAsync(userId, collectionId);
        }

        /// <summary>
        /// NOT WORKING YET. Need to fix the query in the CollectionFactory
        /// </summary>
        /// <param name="timelineId"></param>
        /// <returns></returns>
        public async Task<Chronozoom.Business.Models.Collection> FindByTimelineIdAsync(Guid timelineId)
        {
            Mongo.Models.Collection collection = await factory.FindByTimelineIdAsync(timelineId);

            Chronozoom.Business.Models.Collection mappedCollection = mapCollection(collection);

            return mappedCollection;
        }

        public async Task<Chronozoom.Business.Models.Collection> FindByIdAsync(Guid id)
        {
            Mongo.Models.Collection collection = await factory.FindByIdAsync(id);

            Chronozoom.Business.Models.Collection mappedCollection = mapCollection(collection);

            return mappedCollection;
        }

        public async Task<bool> InsertAsync(Chronozoom.Business.Models.Collection item)
        {
            Mongo.Models.Collection collection = mapCollection(item);

            return await factory.InsertAsync(collection);
        }

        public async Task<bool> UpdateAsync(Chronozoom.Business.Models.Collection item)
        {
            Mongo.Models.Collection collection = mapCollection(item);

            return await factory.UpdateAsync(collection);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await factory.DeleteAsync(id);
        }

        private List<Chronozoom.Business.Models.Collection> mapCollections(List<Mongo.Models.Collection> collections)
        {
            List<Chronozoom.Business.Models.Collection> mappedCollections = new List<Chronozoom.Business.Models.Collection>();

            foreach (var c in collections)
            {
                mappedCollections.Add(mapCollection(c));
            }

            return mappedCollections;
        }

        private Chronozoom.Business.Models.Collection mapCollection(Mongo.Models.Collection collection)
        {
            Chronozoom.Business.Models.Collection cCollection = new Chronozoom.Business.Models.Collection
            {
                Id = collection.Id,
                UserId = collection.OwnerId,
                Default = collection.Default,
                Title = collection.Title,
                //Path = collection.Path // The Library model doesn't have a path..
                //MembersAllowed = collection.MembersAllowed,
                //Members = collection.Members,
                //Timelines = collection.Timelines
            };

            return cCollection;
        }

        private Mongo.Models.Collection mapCollection(Chronozoom.Business.Models.Collection collection)
        {
            Mongo.Models.Collection cCollection = new Mongo.Models.Collection
            {
                Id = collection.Id,
                OwnerId = collection.UserId,
                Default = collection.Default,
                Title = collection.Title,
                //Path = collection.Path // The Library model doesn't have a path..
                //MembersAllowed = collection.MembersAllowed,
                //Members = collection.Members,
                //Timelines = collection.Timelines
            };

            return cCollection;
        }

        public Task<Collection> FindByNameOrDefaultAsync(string superCollection)
        {
            throw new NotImplementedException();
        }
    }
}
