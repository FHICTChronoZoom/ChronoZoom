using Chronozoom.Business.Models;
using Chronozoom.Business.Models.Compability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Repositories
{
    public interface ITourRepository : IRepository<Tour>
    {
        Task<IEnumerable<Business.Models.Tour>> GetDefaultTours();

        Task<IEnumerable<Business.Models.Tour>> GetTours(User superCollection);

        Task<IEnumerable<Business.Models.Tour>> GetTours(User superCollection, string collection);

        Task<Boolean> PutTour(string superCollection, Business.Models.Tour tourRequest);

        Task DeleteTour(string superCollectionName, Business.Models.Tour tourRequest);

        Task DeleteTour(string superCollectionName, string collectionName, Business.Models.Tour tourRequest);
    }
}
