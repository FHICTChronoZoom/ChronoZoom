using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chronozoom.Business.Services;
using System.Threading.Tasks;

namespace Chronozoom.UI.Controllers.Api
{
    public class TourController : ApiController
    {
        private TourService tourService;

        public TourController(TourService tourService)
        {
            this.tourService = tourService;
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
    }


}
