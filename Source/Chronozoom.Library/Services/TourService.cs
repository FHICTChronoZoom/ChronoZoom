using Chronozoom.Business.Models;
using Chronozoom.Business.Models.Compability;
using Chronozoom.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Services
{

    public class TourService
    {
        private ITourRepository tourRepository;
        private ICollectionRepository collectionRepository;

        public TourService(ITourRepository tourRepository, ICollectionRepository collectionRepository)
        {
            if (tourRepository == null) throw new ArgumentNullException("tourRepository");
            if (collectionRepository == null) throw new ArgumentException("collectionRepository");
            this.tourRepository = tourRepository;
            this.collectionRepository = collectionRepository;
        }

        public async Task<Tour> GetTourAsync(string superCollection, string collection, Guid guid)
        {
            var tour = await tourRepository.GetTour(superCollection, collection, guid);
            if (tour != null)
            {
                var bookmarks = await tourRepository.GetBookmarks(tour, collection, superCollection); /// not yet implemented
                if (bookmarks != null)
                {
                    // Tour needs a Bookmark variable, not yet implemented
                }
            }

            return tour;
        }

        public Task<IEnumerable<Tour>> GetDefaultTours()
        {
            return tourRepository.GetDefaultTours();
        }

        public Task<IEnumerable<Tour>> GetToursAsync(User superCollection)
        {
            return tourRepository.GetTours(superCollection);
        }

        public Task<IEnumerable<Tour>> GetToursAsync(User superCollection, string collection)
        {
            return tourRepository.GetTours(superCollection, collection);
        }

        public Task<Boolean> PutTour(User superCollection, Business.Models.Tour tourRequest)
        {
            return tourRepository.PutTour(superCollection, tourRequest);
        }

        public Task<Boolean> PutTour(User superCollection, Guid collection, Business.Models.Tour tourRequest)
        {
            return tourRepository.PutTour(superCollection, collection, tourRequest);
        }

        public Task<Boolean> PostTour(User superCollection, Guid collection, Business.Models.Tour tourRequest)
        {
            return tourRepository.PostTour(superCollection, collection, tourRequest);
        }

        [Obsolete]
        public Task DeleteTour(string superCollectionName, Tour tourRequest)
        {
            return tourRepository.DeleteTour(superCollectionName, tourRequest);
        }

        public Task DeleteTour(string superCollectionName, string collectionName, Tour tourRequest)
        {
            return tourRepository.DeleteTour(superCollectionName, collectionName, tourRequest);
        }
    }
}
