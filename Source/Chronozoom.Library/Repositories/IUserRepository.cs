using Chronozoom.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByUserIdentifierAsync(string nameIdentifier);
        Task<IEnumerable<User>> FindUsersAsync(string partialName);
    }
}
