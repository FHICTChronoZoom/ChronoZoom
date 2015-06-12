using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
