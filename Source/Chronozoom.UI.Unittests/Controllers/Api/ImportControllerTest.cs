using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;
using Chronozoom.Business.Services;
using Chronozoom.UI.Controllers.Api;
using Chronozoom.UI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net;

namespace Chronozoom.UI.UnitTests.Controllers.Api
{
    [TestClass]
    public class ImportControllerTest
    {
        private ImportController controller;

        private User user = new User { Id = Guid.NewGuid(), DisplayName = "Unit test", Email = "unit@test.nl", IdentityProvider = "UnitTest", NameIdentifier = "UnitTestIdentifier" };

        private List<Collection> collections;
        private List<Timeline> timelines;
        private List<Exhibit> exhibits;

        [TestInitialize]
        public void Initialize()
        {
            collections = new List<Collection>
            {
                new Collection { Title = "Test collection", UserId = user.Id, Theme = "Unit testing", Default = true, IsPublicSearchable = true }
            };
            timelines = new List<Timeline>
            {
                new Timeline { Title = "Test timeline" }
            };

            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.FindByUserIdentifierAsync(It.IsAny<string>())).Returns(Task.FromResult(user));

            var collectionRepository = new Mock<ICollectionRepository>();
            collectionRepository.Setup(x => x.FindByTimelineIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(collections[0]));
            collectionRepository.Setup(x => x.IsMemberAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(Task.FromResult(true));

            var timelineRepository = new Mock<ITimelineRepository>();
            timelineRepository.Setup(x => x.FindByIdAsync(It.IsAny<Guid>())).Returns<Guid>(x => Task.FromResult(timelines.FirstOrDefault(t => t.Id == x)));
            timelineRepository.Setup(x => x.InsertAsync(It.IsAny<Timeline>())).Returns<Timeline>(x => 
            {
                timelines.Add(x);
                return Task.FromResult(true);
            });

            var exhibitRepository = new Mock<IExhibitRepository>();
            exhibitRepository.Setup(x => x.InsertAsync(It.IsAny<Exhibit>())).Returns<Exhibit>(X =>
            {
                exhibits.Add(X);
                return Task.FromResult(true);
            });

            var importService = new ImportService(collectionRepository.Object, timelineRepository.Object, exhibitRepository.Object);
            var securityService = new SecurityService(userRepository.Object);

            controller = new ImportController(importService, securityService);
        }

        [TestMethod]
        public void ImportTimeline_Test()
        {
            var response = controller.ImportTimelines(timelines[0].Id, null).Result;
            var result = response.ExecuteAsync(new System.Threading.CancellationToken(false)).Result;

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
