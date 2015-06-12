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

        [HttpGet]
        [Route("~/api/v2/tour/{id:Guid}")]
        public async Task<IHttpActionResult> GetTour(Guid id)
        {
            try
            {
                await tourService.GetTourAsync(id);
                return Ok();
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
                await tourService.GetDefaultTours();
                return Ok();
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
                await tourService.GetToursAsync(user);
                return Ok();
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("~/api/v2/tours/{superCollection:Guid}/{collection:Guid}")]
        public async Task<IHttpActionResult> GetTours(Guid superCollection, Guid collection)
        {
            try
            {
                var user = await securityService.GetUserAsync(User.Identity);
                await tourService.GetToursAsync(user, collection);
                return Ok();
            }

            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }


}
