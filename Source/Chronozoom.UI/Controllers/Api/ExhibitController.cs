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
    public class ExhibitController : ApiController
    {
        private ExhibitService exhibitService;

        [HttpGet]
        [Route("~/api/v2/exhibit/{exhibitId}/lastupdate")]
        public async Task<IHttpActionResult> GetExhibitLastUpdate(string exhibitId)
        {
            var lastUpdated = await exhibitService.GetExhibitLastUpdate(exhibitId);
            return Ok(lastUpdated);
        }
    }
}
