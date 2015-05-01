using Chronozoom.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByUsernameAsync(String username);
    }
}
