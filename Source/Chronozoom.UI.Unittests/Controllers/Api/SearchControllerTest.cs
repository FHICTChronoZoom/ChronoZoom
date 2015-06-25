using Chronozoom.UI.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace Chronozoom.UI.UnitTests.Controllers.Api
{
    [TestClass]
    public class SearchControllerTest
    {
        private SearchController searchController;

        public SearchControllerTest()
        {
            searchController = new SearchController();
            searchController.Configuration = new HttpConfiguration();
            searchController.Request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, "api/v2/Search/Scope/Options");
        }

        [TestMethod]
        public void SearchScopeOptions_Test()
        {
            var result = searchController.GetSearchScopeOptions();
            var ncr = result as NegotiatedContentResult<Dictionary<byte, string>>;
            Assert.IsTrue(ncr.Content.ContainsKey((byte)SearchScope.AllMyCollections));
            Assert.IsTrue(ncr.Content.ContainsKey((byte)SearchScope.AllSearchableCollections));
            Assert.IsTrue(ncr.Content.ContainsKey((byte)SearchScope.CurrentCollection));
            Assert.AreEqual(SearchScope.AllMyCollections.ToString(), ncr.Content[(byte)SearchScope.AllMyCollections].Replace(" ", ""));
            Assert.AreEqual(SearchScope.AllSearchableCollections.ToString(), ncr.Content[(byte)SearchScope.AllSearchableCollections].Replace(" ", ""));
            Assert.AreEqual(SearchScope.CurrentCollection.ToString(), ncr.Content[(byte)SearchScope.CurrentCollection].Replace(" ", ""));
        }
    }
}
