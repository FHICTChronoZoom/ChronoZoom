using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
using System.Data.Entity;

namespace Chronozoom.Entities.Repositories
{
    class TourRepository : ITourRepository
    {
        private Storage storage;

        public TourRepository(Storage storage)
        {
            this.storage = storage;
        }

        public async Task<Library.Models.Tour> FindAsync(Guid id)
        {
            var tour = await storage.Tours.FindAsync(id);
            return ToLibraryTour(tour);
        }

        public async Task InsertAsync(Library.Models.Tour item)
        {
            var tour = ToEntityTour(item);
            storage.Tours.Add(tour);
            await storage.SaveChangesAsync();
        }

        public async Task UpdateAsync(Library.Models.Tour item)
        {
            var tour = await storage.Tours.FindAsync(item.Id);
            //TODO: Properties inserten
            await storage.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var tour = await storage.Tours.FindAsync(id);
            storage.Tours.Remove(tour);
            await storage.SaveChangesAsync();
        }

        private Library.Models.Tour ToLibraryTour(Tour tour)
        {
            return new Library.Models.Tour { Id = tour.Id, AudioBlobUrl = tour.AudioBlobUrl, Category = tour.Category, Description = tour.Description, Name = tour.Name, Sequence = (int)tour.Sequence, UniqueId = tour.UniqueId };
        }

        private Tour ToEntityTour(Library.Models.Tour tour)
        {
            return new Tour { Id = tour.Id, AudioBlobUrl = tour.AudioBlobUrl, Category = tour.Category, Description = tour.Description, Name = tour.Name, Sequence = tour.Sequence, UniqueId = tour.UniqueId };
        }
    }
}
