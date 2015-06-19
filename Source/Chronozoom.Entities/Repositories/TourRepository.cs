using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Repositories;
using System.Data.Entity;
using Chronozoom.Business.Models.Compability;
using Chronozoom.Business.Services;

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
                return new Task<Boolean>(false);
            }

            await PutTour(superCollection, collection, tourRequest);
            return new Task<Boolean>(true);
        }

        public async Task<Boolean> PutTour(User superCollection, Entities.Collection collection, Business.Models.Tour tourRequest)
        {

            if (collection == null)
            {
                return new Task<Boolean>(false);
            }

            var existingTour = await storage.Tours.FindAsync(tourRequest.Id);

            if (existingTour == null)
            {
                return new Task<Boolean>(false);
            }

            existingTour.Id = tourRequest.Id;
            existingTour.Name = tourRequest.Name;
            existingTour.Description = tourRequest.Description;
            existingTour.UniqueId = tourRequest.UniqueId;
            existingTour.AudioBlobUrl = tourRequest.AudioBlobUrl;
            existingTour.Category = tourRequest.Category;
            existingTour.Sequence = tourRequest.Sequence;

            await storage.SaveChangesAsync();
            return new Task<Boolean>(true);
        }

        public Task<Boolean> PostTour (User superCollection, Guid collection, Business.Models.Tour tourRequest)
        {
            if (collection == null)
            {
                return new Task<Boolean>(false);
            }

            storage.Tours.Add(ToTour(tourRequest));
            storage.SaveChangesAsync();
            return new Task<Boolean>(true);
        }

        public Task DeleteTour(string superCollectionName, Business.Models.Tour tourRequest)
        {
            var collection = storage.Collections.Where(candidate => candidate.SuperCollection.Title == superCollectionName).FirstOrDefault();

            if (collection != null)
            {
                storage.Tours.Remove(ToTour(tourRequest));
                storage.SaveChangesAsync();
            }
            return new Task();
        }

        public Task DeleteTour(string superCollectionName, string collectionName, Business.Models.Tour tourRequest)
        {
            var collection = storage.Collections.Where(candidate => candidate.SuperCollection.Title == superCollectionName).FirstOrDefault();

            if (collection != null)
            {
                storage.Tours.Remove(ToTour(tourRequest));
                storage.SaveChangesAsync();
            }
            return new Task();
        }

        private Entities.Tour ToTour(Business.Models.Tour tourModel)
        {
            return new Entities.Tour { Id = tourModel.Id, Name = tourModel.Name, Description = tourModel.Description, UniqueId = tourModel.UniqueId, AudioBlobUrl = tourModel.AudioBlobUrl, Category = tourModel.Category, Sequence = tourModel.Sequence };
        }



    }
}
