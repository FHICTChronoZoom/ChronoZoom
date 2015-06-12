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
    public class UserController : ApiController
    {
        private UserService userService;
        
        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("~/api/v2/user?name={name}")]
        public async Task<IHttpActionResult> GetUser(string name)
        {
            var user = await userService.GetUser(name);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpDelete]
        [Route("~/api/v2/user")]
        public async Task<IHttpActionResult> DeleteUser(User userRequest)
        {
            await userService.DeleteUserAsync(userRequest);
            return Ok();
        }

        [HttpPost]
        [Route("~/api/v2/user")]
        public async Task<IHttpActionResult> PutUser(User userRequest)
        {
            await userService.CreateUserAsync(userRequest);
            return Ok();
        }
    }
}
