using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Chronozoom.UI.Controllers.Api
{
    public class SearchController : ApiController
    {
        /// <summary>
        /// Use to populate a drop-down list box of search scope options. Returns a select element friendly version of the SearchScope enum.
        /// </summary>
        /// <returns>A descriptive list of all of the search scope options, along with the value to pass (which is the dictionary key.)</returns>
        [Route("~/api/v2/search/scope/options")]
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

            // For compability.
            // The old API formats a dictionary as  :    [{"Key":"{keyvalue}", "Value":"{value}"}, ... ]
            // Web API automaticly format as        :    {"{keyvalue}": "{value}"}, ...
            var formatter = new JsonMediaTypeFormatter();
            formatter.UseDataContractJsonSerializer = true;

            var respFormatters = new List<MediaTypeFormatter>();
            respFormatters.Add(formatter);
            respFormatters.AddRange(Configuration.Formatters);

            return new NegotiatedContentResult<Dictionary<byte, string>>(HttpStatusCode.OK, rv, Configuration.Services.GetContentNegotiator(), Request, respFormatters);
        }
    }
}
