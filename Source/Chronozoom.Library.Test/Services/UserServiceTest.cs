using System;
using Chronozoom.Business.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Chronozoom.Business.Test.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserRepository userRepository;
        private ICollectionRepository collectionRepository;

        [TestInitialize]
        public void Initialize()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
        }


        [TestMethod]
        public void CreateUserAsync_Test()
        {
            
        }
    }
}
