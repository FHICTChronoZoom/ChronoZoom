using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chronozoom.Business.Repositories;
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



        public async Task<Chronozoom.Business.Models.User> FindByUsernameAsync(string username)
        {
            Mongo.Models.User user = await factory.FindByNameAsync(username);
            Chronozoom.Business.Models.User cUser = new Chronozoom.Business.Models.User
            {
                Email = user.Email,
                DisplayName = user.Name,
                Id = user.Id,
                IdentityProvider = "none",
                NameIdentifier = "none"
            };

            return cUser;
        }

        public async Task<Chronozoom.Business.Models.User> FindByEmailAsync(string email)
        {
            Mongo.Models.User user = await factory.FindByEmailAsync(email);
            Chronozoom.Business.Models.User cUser = new Chronozoom.Business.Models.User
            {
                Email = user.Email,
                DisplayName = user.Name,
                Id = user.Id,
                IdentityProvider = "none",
                NameIdentifier = "none"
            };

            return cUser;
        }

        public async Task<Chronozoom.Business.Models.User> FindByIdAsync(Guid id)
        {
            Mongo.Models.User user = await factory.FindByIdAsync(id);
            Chronozoom.Business.Models.User cUser = new Chronozoom.Business.Models.User
            {
                Email = user.Email,
                DisplayName = user.Name,
                Id = user.Id,
                IdentityProvider = "none",
                NameIdentifier = "none"
            };

            return cUser;
        }

        public async Task<bool> InsertAsync(Chronozoom.Business.Models.User item)
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

        public async Task<bool> UpdateAsync(Chronozoom.Business.Models.User item)  
        {
            Mongo.Models.User cUser = new Mongo.Models.User
            {
                Email = item.Email,
                Name = item.DisplayName,
                Id = item.Id,
            };

            Boolean success = await factory.UpdateAsync(cUser);

            return success;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            Boolean success = await factory.DeleteAsync(id);

            return success;
        }

        public async Task<IEnumerable<Chronozoom.Business.Models.User>> FindUsersAsync(string partialName)
        {
            List<Chronozoom.Business.Models.User> listMappedUsers = new List<Chronozoom.Business.Models.User>();
            IEnumerable<Mongo.Models.User> listUser = await factory.FindUsersAsync(partialName);
            foreach (Mongo.Models.User user in listUser) {
                Chronozoom.Business.Models.User mappedUser = mapUser(user);
                listMappedUsers.Add(mappedUser);
            }
            return listMappedUsers;

        }

        private Chronozoom.Business.Models.User mapUser(Mongo.Models.User user)
        {
            Chronozoom.Business.Models.User cUser = new Chronozoom.Business.Models.User
                {
                    Email = user.Email,
                    DisplayName = user.Name,
                    Id = user.Id,
                    IdentityProvider = "none",
                    NameIdentifier = "none"
                };
            return cUser;
        }
    }
}
