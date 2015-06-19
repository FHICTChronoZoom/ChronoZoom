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

        public TourService(ITourRepository tourRepository)
        {
            if (tourRepository == null) throw new ArgumentNullException("tourRepository");
            this.tourRepository = tourRepository;
        }

        public Task<Tour> GetTourAsync(Guid id)
        {
            return tourRepository.FindByIdAsync(id);
        }

        public Task<Tour> GetTourAsync(string SuperCollection, string collection, Guid guid)
        {
            throw new NotImplementedException();
            // needs refactoring
        //    Tour rv = storage.Tours
        //               .Where
        //               (t =>
        //                   t.Id == guid
        //                   &&
        //                   (
        //                       t.Collection.Path == Collection.ToLower() ||
        //                       (t.Collection.Default && Collection == "")
        //                   )
        //                   &&
        //                   (
        //                       t.Collection.SuperCollection.Title.ToLower() == superCollection.ToLower() ||
        //                       (t.Collection.SuperCollection.Title == _defaultSuperCollectionName && superCollection == "")
        //                   )
        //               )
        //               .FirstOrDefault();

        //    if (rv != null)
        //    {
        //        // would've been so much easier to prived sorted bookmarks if could reference tour from bookmark...

        //        var bookmarks = storage.Tours
        //            .Include("Bookmarks")
        //            .Where
        //            (t =>
        //                t.Id == guid
        //                &&
        //                (
        //                    t.Collection.Path == Collection.ToLower() ||
        //                    (t.Collection.Default && Collection == "")
        //                )
        //                &&
        //                (
        //                    t.Collection.SuperCollection.Title.ToLower() == superCollection.ToLower() ||
        //                    (t.Collection.SuperCollection.Title == _defaultSuperCollectionName && superCollection == "")
        //                )
        //            )
        //            .Select(t => t.Bookmarks)
        //            .ToList();

        //        IEnumerable<Bookmark> sorted = bookmarks[0].ToList()
        //            .OrderBy(b => b.SequenceId)
        //            .ThenBy(b => b.LapseTime);

        //        Collection<Bookmark> inserts = new Collection<Bookmark>();
        //        foreach (Bookmark bookmark in sorted)
        //        {
        //            inserts.Add(bookmark);
        //        }

        //        rv.Bookmarks = inserts;
        //    }

        //    return rv;
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
