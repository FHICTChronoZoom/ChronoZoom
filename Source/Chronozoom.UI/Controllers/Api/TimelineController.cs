using Chronozoom.Business.Repositories;
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
    public class TimelineController : ApiController
    {
        private TimelineService timelineService;
        private CollectionService collectionService;

        public TimelineController(TimelineService timelineService)
        {
            this.timelineService = timelineService;
        }

        public async Task<IHttpActionResult> GetRootAsync(string superCollection, string collection)
        {
            return null;
            //var collectionId = await collectionService
            //Guid collectionId = CollectionIdOrDefault(storage, superCollection, collection);
            //Timeline timeline = storage.GetRootTimelines(collectionId);

            //return timeline == null ? new Guid() : timeline.Id;
        }
    }
}