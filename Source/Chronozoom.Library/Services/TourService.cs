﻿using Chronozoom.Business.Models;
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

        public TourService(ITourRepository tourRepository)
        {
            if (tourRepository == null) throw new ArgumentNullException("tourRepository");
            this.tourRepository = tourRepository;
        }

        public Task<Tour> GetTourAsync(Guid id)
        {
            return tourRepository.FindByIdAsync(id);
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

        public Task<Boolean> PutTour(string superCollection, Business.Models.Tour tourRequest)
        {
            return tourRepository.PutTour(superCollection, tourRequest);
        }

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
