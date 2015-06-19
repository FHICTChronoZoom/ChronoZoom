using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chronozoom.Business.Services;
using System.Threading.Tasks;
using Chronozoom.UI.Services;

namespace Chronozoom.UI.Controllers.Api
{
    public class TourController : ApiController
    {
        private TourService tourService;
        private SecurityService securityService;
        private CollectionService collectionService;

        public TourController(TourService tourService, SecurityService securityService, CollectionService collectionService)
        {
            this.tourService = tourService;
            this.securityService = securityService;
            this.collectionService = collectionService;
        }

        [HttpPut]
        [Route("~/api/v2/tour/{id:Guid}")]
        public async Task<IHttpActionResult> GetTour(Guid id)
        {
            try
            {
                var tour = await tourService.GetTourAsync(id);
                return Ok(tour);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("~/api/v2/defaulttours")]
        public async Task<IHttpActionResult> GetDefaultTours()
        {
            try
            {
                var tours = await tourService.GetDefaultTours();
                return Ok(tours);
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("~/api/v2/tours/{user:String}")]
        public async Task<IHttpActionResult> GetTours(string superCollection)
        {
            try
            {
                var user = await securityService.GetUserAsync(User.Identity);
                var tour = await tourService.GetToursAsync(user);
                return Ok(tour);
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //TODO:
        //Needs to be fixed to retrieve correct user
        [HttpPut]
        [Route("~/api/v2/tours/{superCollection:Guid}/{collection:Guid}")]
        public async Task<IHttpActionResult> GetTours(string superCollection, string collection)
        {
            try
            {
                var user = await securityService.GetUserAsync(User.Identity);
                var tour = await tourService.GetToursAsync(user, collection);
                return Ok(tour);
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public async Task<IHttpActionResult> PutTour(string superCollection, Business.Models.Tour tourRequest)
        {
            var user = await securityService.GetUserAsync(User.Identity);
            var success = await tourService.PutTour(user, tourRequest);
            return Ok(success);
        }


        public async Task<IHttpActionResult> PutTour(string superCollection, string collection, Business.Models.Tour tourRequest)
        {
            var user = await securityService.GetUserAsync(User.Identity);
            Guid collectionId = await collectionService.CollectionIdOrDefaultAsync(superCollection, collection);
            var success = await tourService.PutTour(user, collectionId, tourRequest);
            return Ok(success);
        }

        [HttpPut]
        [Route("~/api/v2/posttour/{superCollection:Guid}/{collection:Guid}")]
        public async Task<IHttpActionResult> PostTour(string superCollection, string collection, Business.Models.Tour tourRequest)
        {
            var user = await securityService.GetUserAsync(User.Identity);
            Guid collectionId = await collectionService.CollectionIdOrDefaultAsync(superCollection, collection);
            var success = tourService.PostTour(user, collectionId, tourRequest);
            return Ok(success);
        }

        public async Task<IHttpActionResult> DeleteTour(string superCollectionName, Business.Models.Tour tourRequest)
        {
            await tourService.DeleteTour(superCollectionName, tourRequest);
            return Ok();
        }

        public async Task<IHttpActionResult> DeleteTour(string superCollectionName, string collectionName, Business.Models.Tour tourRequest)
        {
            await tourService.DeleteTour(superCollectionName, collectionName, tourRequest);
            return Ok();
        }
    }
}
