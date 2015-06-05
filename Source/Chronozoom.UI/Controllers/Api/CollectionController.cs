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
    public class CollectionController : ApiController
    {
        CollectionService collectionServive;
        TimelineService timelineService;

        public CollectionController(CollectionService collectionService)
        {
            this.collectionServive = collectionService;
        }

    }
}