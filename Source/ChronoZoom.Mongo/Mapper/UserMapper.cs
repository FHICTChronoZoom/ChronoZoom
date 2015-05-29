using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronozoom.Library.Repositories;
using System.Threading.Tasks;
using ChronoZoom.Mongo.PersistencyEngine;

namespace ChronoZoom.Mongo.Mapper
{
    public class UserMapper : IUserRepository
    {
        private UserFactory factory;

        public UserMapper(UserFactory factory) 
        {
            this.factory = factory;
        }



        public async Task<Chronozoom.Library.Models.User> FindByUsernameAsync(string username)
        {
            Mongo.Models.User user = await factory.FindByNameAsync(username);
            Chronozoom.Library.Models.User cUser = new Chronozoom.Library.Models.User
            {
                Email = user.Email,
                DisplayName = user.Name,
                Id = user.Id,
                IdentityProvider = "none",
                NameIdentifier = "none"
            };

            return cUser;
        }

        public async Task<Chronozoom.Library.Models.User> FindByEmailAsync(string email)
        {
            Mongo.Models.User user = await factory.FindByEmailAsync(email);
            Chronozoom.Library.Models.User cUser = new Chronozoom.Library.Models.User
            {
                Email = user.Email,
                DisplayName = user.Name,
                Id = user.Id,
                IdentityProvider = "none",
                NameIdentifier = "none"
            };

            return cUser;
        }

        public async Task<Chronozoom.Library.Models.User> FindByIdAsync(Guid id)
        {
            Mongo.Models.User user = await factory.FindByIdAsync(id);
            Chronozoom.Library.Models.User cUser = new Chronozoom.Library.Models.User
            {
                Email = user.Email,
                DisplayName = user.Name,
                Id = user.Id,
                IdentityProvider = "none",
                NameIdentifier = "none"
            };

            return cUser;
        }

        public async Task<bool> InsertAsync(Chronozoom.Library.Models.User item)
        {

            Mongo.Models.User cUser = new Mongo.Models.User
            {
                Email = item.Email,
                Name = item.DisplayName,
                Id = item.Id,
            };

            Boolean success = await factory.InsertAsync(cUser);

            return success;
        }

        public Task<bool> UpdateAsync(Chronozoom.Library.Models.User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
