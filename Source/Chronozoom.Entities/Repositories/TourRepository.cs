using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Repositories;
using System.Data.Entity;

namespace Chronozoom.Entities.Repositories
{
    public class TourRepository : ITourRepository
    {
        private Storage storage;

        public TourRepository(Storage storage)
        {
            this.storage = storage;
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
            return new Tour { Id = tour.Id, AudioBlobUrl = tour.AudioBlobUrl, Category = tour.Category, Description = tour.Description, Name = tour.Name, Sequence = tour.Sequence, UniqueId = tour.UniqueId };
        }
    }
}
