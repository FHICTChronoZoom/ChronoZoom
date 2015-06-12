using Chronozoom.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Chronozoom.UI.Controllers.Api
{
    public class ExportController : ApiController
    {
        private ExportService exportService;
        
        public ExportController(ExportService exportService)
        {
            this.exportService = exportService;
        }

        [HttpGet]
        [Route("~/api/v2/export/timeline/{topmostTimelineId:Guid}")]
        public async Task<IHttpActionResult> ExportTimeline(Guid topmostTimelineId)
        {
            var result = await exportService.ExportTimeline(topmostTimelineId);
            if(result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("~/api/v2/export/exhibit/{exhibitId:Guid}")]
        public async Task<IHttpActionResult> ExportExhibit(Guid exhibitId)
        {
            var result = await exportService.ExportExhibit(exhibitId);
            if(result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
