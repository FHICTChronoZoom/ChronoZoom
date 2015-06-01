using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Repositories;
using System.Data.Entity;

namespace Chronozoom.Entities.Repositories
{
    public class UserRepository : IUserRepository
    {
        private Storage storage;

        public UserRepository(Storage storage)
        {
            this.storage = storage;
        }

        public async Task<Library.Models.User> FindByUsernameAsync(string username)
        {
            var user = await storage.Users.FirstOrDefaultAsync(x => x.DisplayName == username);
            return ToLibraryUser(user);
        }

        public async Task<Library.Models.User> FindByEmailAsync(string email)
        {
            var user = await storage.Users.FirstOrDefaultAsync(x => x.Email == email);
            return ToLibraryUser(user);
        }

        public async Task<Library.Models.User> FindAsync(Guid id)
        {
            var user = await storage.Users.FindAsync(id);
            return ToLibraryUser(user);
        }

        public async Task<bool> InsertAsync(Library.Models.User item)
        {
            var user = ToEntityUser(item);
            storage.Users.Add(user);
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Library.Models.User item)
        {
            var user = await storage.Users.FindAsync(item.Id);
            user.DisplayName = item.DisplayName;
            user.Email = item.Email;
            user.IdentityProvider = item.IdentityProvider;
            user.NameIdentifier = item.NameIdentifier;
            return await storage.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await storage.Users.FindAsync(id);
            storage.Users.Remove(user);
            return await storage.SaveChangesAsync() > 0;
        }

        private Library.Models.User ToLibraryUser(User user)
        {
            return new Library.Models.User{Id = user.Id, Email = user.Email, DisplayName = user.DisplayName, NameIdentifier = user.NameIdentifier, IdentityProvider = user.IdentityProvider };
        }

        private User ToEntityUser(Library.Models.User user)
        {
            return new User { Id = user.Id, DisplayName = user.DisplayName, Email = user.Email, IdentityProvider = user.IdentityProvider, NameIdentifier = user.NameIdentifier };
        }


        public async Task<Library.Models.User> FindByUserIdentifierAsync(string nameIdentifier)
        {
            var user = await storage.Users.FirstOrDefaultAsync(x => x.NameIdentifier == nameIdentifier);
            return ToLibraryUser(user);
        }

        public async Task<Library.Models.User> FindByIdAsync(Guid id)
        {
            var user = await storage.Users.FindAsync(id);
            return ToLibraryUser(user);

        }
    }
}
