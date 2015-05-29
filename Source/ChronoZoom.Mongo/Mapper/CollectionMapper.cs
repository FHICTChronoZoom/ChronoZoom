﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
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

        public async Task<IEnumerable<Chronozoom.Library.Models.Collection>> GetPublicCollectionsAsync()
        {
            List<Mongo.Models.Collection> collection = await factory.FindPublicCollectionsAsync();

            List<Chronozoom.Library.Models.Collection> mappedCollections = mapCollections(collection);

            return mappedCollections;

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

        private List<Chronozoom.Library.Models.Collection> mapCollections(List<Mongo.Models.Collection> collections)
        {
            List<Chronozoom.Library.Models.Collection> mappedCollections = new List<Chronozoom.Library.Models.Collection>();

            foreach (var c in collections)
            {
                mappedCollections.Add(mapCollection(c));
            }

            return mappedCollections;
        }

        private Chronozoom.Library.Models.Collection mapCollection(Mongo.Models.Collection collection)
        {
            Chronozoom.Library.Models.Collection cCollection = new Chronozoom.Library.Models.Collection
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
    }
}
