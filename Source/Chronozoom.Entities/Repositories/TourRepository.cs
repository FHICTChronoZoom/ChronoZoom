using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Repositories;
using System.Data.Entity;
using Chronozoom.Business.Models.Compability;
using Chronozoom.Business.Services;
using Chronozoom.Business.Models;

namespace Chronozoom.Entities.Repositories
{
    public class TourRepository : ITourRepository
    {
        private Storage storage;
        private CollectionService collectionService;

        public TourRepository(Storage storage, CollectionService collectionService)
        {
            this.storage = storage;
            this.collectionService = collectionService;
        }

        public async Task<Business.Models.Tour> FindByIdAsync(Guid id)
        {
            var tour = await storage.Tours.FindAsync(id);
            return ToLibraryTour(tour);
        }

        public async Task<bool> InsertAsync(Business.Models.Tour item)
        {
            var tour = ToEntityTour(item);
            storage.Tours.Add(tour);
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Business.Models.Tour item)
        {
            var tour = await storage.Tours.FindAsync(item.Id);
            //TODO: Properties inserten
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tour = await storage.Tours.FindAsync(id);
            storage.Tours.Remove(tour);
            return await storage.SaveChangesAsync() > 0;
        }

        private Business.Models.Tour ToLibraryTour(Tour tour)
        {
            return new Business.Models.Tour { Id = tour.Id, AudioBlobUrl = tour.AudioBlobUrl, Category = tour.Category, Description = tour.Description, Name = tour.Name, Sequence = (int)tour.Sequence, UniqueId = tour.UniqueId };
        }

        private Tour ToEntityTour(Business.Models.Tour tour)
        {
            return new Entities.Tour { Id = tour.Id, AudioBlobUrl = tour.AudioBlobUrl, Category = tour.Category, Description = tour.Description, Name = tour.Name, Sequence = tour.Sequence, UniqueId = tour.UniqueId };
        }

        public async Task<Tour> GetTour(string superCollection, string collection, Guid guid)
        {
            var tour = new Tour();
            var query = storage.Tours.AsQueryable();

            if(collection != null)
            {
                query = query.Where(x => x.Collection.Title == collection);
            }

            if(superCollection !=null)
            {
                query = query.Where(x => x.Collection.User.DisplayName == superCollection);
            }
            if (guid != null)
            {
                query = query.Where(x => x.Id == guid);
            }

            tour = await query.FirstOrDefaultAsync();
            return tour;
        }

        public async Task<IEnumerable<Business.Models.Tour>> GetDefaultTours()
        {
            Guid defaultId = await collectionService.CollectionIdOrDefaultAsync("", "");
            var tours = (IEnumerable<Business.Models.Tour>)storage.Tours.Where(candidate => candidate.Collection.Id == defaultId);
            return tours;
        }

        public async Task<IEnumerable<Business.Models.Tour>> GetTours(Business.Models.User superCollection)
        {
            List<Business.Models.Tour> toursCollection = new List<Business.Models.Tour>();
            var collections = (IEnumerable<Business.Models.Collection>)storage.Collections.Where(candidate => candidate.SuperCollection.Id == superCollection.Id);
            foreach (Business.Models.Collection c in collections)
            {
                var tours = (IEnumerable<Business.Models.Tour>)storage.Tours.Where(candidate => candidate.Collection.Id == c.Id);
                toursCollection.AddRange(tours);
            }
            return toursCollection;
        }

        public async Task<IEnumerable<Business.Models.Tour>> GetTours(Business.Models.User superCollection, Guid collection)
        {
            List<Business.Models.Tour> toursCollection = new List<Business.Models.Tour>();
            var collections = (IEnumerable<Business.Models.Collection>)storage.Collections.Where(candidate => candidate.SuperCollection.Id == superCollection.Id);
            foreach (Business.Models.Collection c in collections)
            {
                if (c.Id == collection)
                {
                    var tours = (IEnumerable<Business.Models.Tour>)storage.Tours.Where(candidate => candidate.Collection.Id == collection);
                    toursCollection.AddRange(tours);
                }
            }
            return toursCollection;
        }

        public async Task<Boolean> PutTour(User superCollection, Business.Models.Tour tourRequest)
        {
            var collection = storage.Collections.Where(candidate => candidate.SuperCollection.Title == superCollection.NameIdentifier).FirstOrDefault();            

            if (collection == null)
            {
                return false;
            }

            await PutTour(superCollection, collection, tourRequest);
            return true;
        }

        public async Task<Boolean> PutTour(User superCollection, Entities.Collection collection, Business.Models.Tour tourRequest)
        {
            if (collection == null)
            {
                return false;
            }

            var existingTour = await storage.Tours.FindAsync(tourRequest.Id);
            if (existingTour == null)
            {
                return false;
            }

            existingTour.Id = tourRequest.Id;
            existingTour.Name = tourRequest.Name;
            existingTour.Description = tourRequest.Description;
            existingTour.UniqueId = tourRequest.UniqueId;
            existingTour.AudioBlobUrl = tourRequest.AudioBlobUrl;
            existingTour.Category = tourRequest.Category;
            existingTour.Sequence = tourRequest.Sequence;

            await storage.SaveChangesAsync();
            return true;
        }

        public async Task<Boolean> PostTour (User superCollection, Guid collection, Business.Models.Tour tourRequest)
        {
            if (collection == null)
            {
                return false;
            }

            storage.Tours.Add(ToTour(tourRequest));
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task DeleteTour(string superCollectionName, Business.Models.Tour tourRequest)
        {
            var collection = storage.Collections.Where(candidate => candidate.SuperCollection.Title == superCollectionName).FirstOrDefaultAsync();

            if (collection != null)
            {
                storage.Tours.Remove(ToTour(tourRequest));
                await storage.SaveChangesAsync();
            }
        }

        public async Task DeleteTour(string superCollectionName, string collectionName, Business.Models.Tour tourRequest)
        {
            var collection = await storage.Collections.Where(candidate => candidate.SuperCollection.Title == superCollectionName).FirstOrDefaultAsync();

            if (collection != null)
            {
                storage.Tours.Remove(ToTour(tourRequest));
                await storage.SaveChangesAsync();
            }
        }

        private Entities.Tour ToTour(Business.Models.Tour tourModel)
        {
            return new Entities.Tour { Id = tourModel.Id, Name = tourModel.Name, Description = tourModel.Description, UniqueId = tourModel.UniqueId, AudioBlobUrl = tourModel.AudioBlobUrl, Category = tourModel.Category, Sequence = tourModel.Sequence };
        }

        public Task<IEnumerable<Business.Models.Tour>> GetTours(Business.Models.User superCollection, string collection)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostTour(Business.Models.User superCollection, Guid collection, Business.Models.Tour tourRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutTour(Business.Models.User superCollection, Business.Models.Tour tourRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutTour(Business.Models.User superCollection, Guid collection, Business.Models.Tour tourRequest)
        {
            throw new NotImplementedException();
        }
    }
}
