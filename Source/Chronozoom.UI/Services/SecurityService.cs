using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronozoom.Library.Repositories;
using Chronozoom.Library.Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Chronozoom.UI.Services
{
    public class SecurityService
    {
        private IUserRepository userRepository;

        public SecurityService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetUser(IIdentity identity)
        {
            Microsoft.IdentityModel.Claims.ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as Microsoft.IdentityModel.Claims.ClaimsIdentity;
            if (claimsIdentity == null || !claimsIdentity.IsAuthenticated) { return null; }

            Microsoft.IdentityModel.Claims.Claim nameIdentifierClaim = claimsIdentity.Claims.Where(candidate => candidate.ClaimType.EndsWith("nameidentifier", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (nameIdentifierClaim == null) { return null; }

            Microsoft.IdentityModel.Claims.Claim identityProviderClaim = claimsIdentity.Claims.Where(candidate => candidate.ClaimType.EndsWith("identityprovider", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (identityProviderClaim == null) { return null; }

            return await userRepository.FindByUserIdentifierAsync(nameIdentifierClaim.Value);
        }

    }
}