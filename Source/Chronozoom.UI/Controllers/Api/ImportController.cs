using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chronozoom.Business.Services;
using System.Threading.Tasks;
using Chronozoom.Business.Models.Compability;
using Chronozoom.Business.Models;

namespace Chronozoom.UI.Controllers.Api
{
    public class ImportController : ApiController
    {
        private ImportService importService;
        
        public ImportController(ImportService importService)
        {
            this.importService = importService;
        }

        [HttpPut]
        [Route("~/api/v2//import/timeline/{intoTimelineId:Guid}")]
        public async Task<IHttpActionResult> ImportTimelines(Guid intoTimelineId, List<FlatTimeline> importContent)
        {
            // TODO : Get user
            try
            {
                await importService.ImportTimelinesAsync(intoTimelineId, importContent, null);
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [CLSCompliant(false)]
        [HttpGet]
        [Route("~/api/v2/import/exhibit/{intoTimelineId:Guid}")]
        public async Task<IHttpActionResult> ImportExhibit(Guid intoTimelineId, Exhibit newExhibit)
        {
            // TODO : Get user
            try
            {
                await importService.ImportExhibit(intoTimelineId, newExhibit, null);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
