using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Chronozoom.UI.Controllers.Api
{
    public class SearchController : ApiController
    {
        public IHttpActionResult GetSearchScopeOptions()
        {
            Dictionary<byte, string> rv = new Dictionary<byte, string>();

            foreach (SearchScope scope in (SearchScope[])Enum.GetValues(typeof(SearchScope)))
            {
                rv.Add
                (
                    (byte)scope,
                    System.Text.RegularExpressions.Regex.Replace(scope.ToString(), "[A-Z]", " $0").Trim()   // changes enum name to a displayable description with spaces before each capital letter
                );
            }

            return Ok(rv);
        }
    }
}
