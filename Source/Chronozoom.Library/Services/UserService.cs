using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;

namespace Chronozoom.Business.Services
{
    public class UserService
    {
        private IUserRepository userRepository;
        private ICollectionRepository collectionRepository;

        public UserService(IUserRepository userRepository, ICollectionRepository collectionRepository)
        {
            if (userRepository == null) throw new ArgumentNullException("userRepository");
            if (collectionRepository == null) throw new ArgumentNullException("collectionRepository");
            this.userRepository = userRepository;
            this.collectionRepository = collectionRepository;
        }

        public async Task CreateUserAsync(User user)
        {
            await userRepository.InsertAsync(user);

            // Create personal, default collection.
            var defaultCollection = new Collection
            {
                Default = true,
                IsPublicSearchable = false,
                Title = user.DisplayName + " Default Collection",
            };
            await collectionRepository.InsertAsync(defaultCollection);
        }

        public async Task<User> GetUser(string name)
        {
            var result = await userRepository.FindByUsernameAsync(name);
            return result;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            return await userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            var collections = await collectionRepository.GetByUserAsync(user.Id);
            foreach (var collection in collections)
            {
                await collectionRepository.DeleteAsync(collection.Id);
            }

            return await userRepository.DeleteAsync(user.Id);
        }

        public async Task<IEnumerable<User>> FindByUsernameAsync(String partialName)
        {
            return await userRepository.FindUsersAsync(partialName);
        }
    }
}
