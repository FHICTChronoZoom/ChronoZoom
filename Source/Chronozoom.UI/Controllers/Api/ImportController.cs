using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chronozoom.Business.Services;
using System.Threading.Tasks;
using Chronozoom.UI.Services;
using Chronozoom.Business.Models.Compability;
using Chronozoom.Business.Models;

namespace Chronozoom.UI.Controllers.Api
{
    public class ImportController : ApiController
    {
        private ImportService importService;
        private SecurityService securityService;
        
        public ImportController(ImportService importService, SecurityService securityService)
        {
            this.importService = importService;
            this.securityService = securityService;
        }

        [HttpPut]
        [Route("~/api/v2//import/timeline/{intoTimelineId:Guid}")]
        public async Task<IHttpActionResult> ImportTimelines(Guid intoTimelineId, List<FlatTimeline> importContent)
        {
            var user = await securityService.GetUserAsync(User.Identity);
            try
            {
                await importService.ImportTimelinesAsync(intoTimelineId, importContent, user);
                return Ok();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        [HttpGet]
        [Route("~/api/v2/import/exhibit/{intoTimelineId:Guid}")]
        public async Task<IHttpActionResult> ImportExhibit(Guid intoTimelineId, Exhibit newExhibit)
        {
            var user = await securityService.GetUserAsync(User.Identity);
            try
            {
                await importService.ImportExhibit(intoTimelineId, newExhibit, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
