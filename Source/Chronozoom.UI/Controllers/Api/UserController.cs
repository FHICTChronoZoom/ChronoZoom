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
        [Route("~/api/v2/user")]
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
            IEnumerable<User> updateUser = await userService.FindByUsernameAsync(userRequest.DisplayName);
            User user = updateUser.FirstOrDefault();
            if (userRequest.Id == Guid.Empty && user == null)
            {
                await userService.CreateUserAsync(userRequest);
            }
            else
            {
                await userService.UpdateUserAsync(userRequest);
            }
            return Ok();
        }

        /// <summary>
        /// Returns a list of users whose display names match the partial display name provided as a parameter.
        /// </summary>
        /// <param name="partialName">Part of a User's DisplayName.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/v2/find/users")]
        public async Task<IHttpActionResult> FindUsers(string partialName)
        {
            var users = await userService.FindByUsernameAsync(partialName);
            return Ok(users);
        }
    }
}
