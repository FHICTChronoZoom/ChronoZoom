using Chronozoom.Business.Models;
using Chronozoom.Business.Models.Compability;
using Chronozoom.Business.Repositories;
using Chronozoom.Business.Services;
using Chronozoom.UI.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Chronozoom.UI.UnitTests.Controllers.Api
{
    [TestClass]
    public class ExportControllerTest
    {
        private ExportController exportController;

        private List<Timeline> timelines;
        private List<Exhibit> exhibits;

        [TestInitialize]
        public void Initialize()
        {
            timelines = new List<Timeline>
            {
                new Timeline { Title = "Test timeline" }
            };
            exhibits = new List<Exhibit>
            {
                new Exhibit { Title = "Test exhibit", TimelineId = timelines[0].Id },
                new Exhibit { Title = "Yet another exhibit", TimelineId = timelines[0].Id }
            };

            var timelineRepository = new Mock<ITimelineRepository>();
            timelineRepository.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).Returns<Guid>(x => Task.FromResult(timelines.Find(t => t.Id == x)));
            timelineRepository.Setup(x => x.GetByTimelineAsync(It.IsAny<Guid>())).Returns<Guid>(x => Task.FromResult(timelines.Where(t => t.ParentTimeline == x)));

            var exhibitRepository = new Mock<IExhibitRepository>();
            exhibitRepository.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).Returns<Guid>(x => Task.FromResult(exhibits.Find(e => e.Id == x)));
            exhibitRepository.Setup(x => x.GetByTimelineAsync(It.IsAny<Guid>())).Returns<Guid>(x => Task.FromResult(exhibits.Where(e => e.TimelineId == x)));

            var service = new ExportService(timelineRepository.Object, exhibitRepository.Object);

            exportController = new ExportController(service);
            exportController.Configuration = new HttpConfiguration();
            exportController.Request = new HttpRequestMessage();
        }

        [TestMethod]
        public void ExportExhibit_Test()
        {
            var response = exportController.ExportExhibit(exhibits[0].Id).Result;
            var result = response.ExecuteAsync(new System.Threading.CancellationToken(false)).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            Exhibit exhibit = null;
            Assert.IsTrue(result.TryGetContentValue(out exhibit));
            Assert.AreEqual(exhibits[0].Id, exhibit.Id);
        }

        [TestMethod]
        public void ExportTimeline_Test()
        {
            var response = exportController.ExportTimeline(timelines[0].Id).Result;
            var result = response.ExecuteAsync(new System.Threading.CancellationToken(false)).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            IEnumerable<FlatTimeline> exports = null;
            Assert.IsTrue(result.TryGetContentValue(out exports));
            Assert.AreEqual(timelines[0].Id, exports.First().Timeline.Id);
        }
    }
}
