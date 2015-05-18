using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronozoom.Library.Models;
using Chronozoom.Library.Repositories;

namespace Chronozoom.Library.Services
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

        public async Task CreateUser(User user)
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

        public async Task UpdateUser(User user)
        {
            await userRepository.UpdateAsync(user);
        }

        public async Task DeleteUser(User user)
        {
            var collections = await collectionRepository.GetByUserAsync(user.Id);
            foreach (var collection in collections)
            {
                await collectionRepository.DeleteAsync(collection.Id);
            }

            await userRepository.DeleteAsync(user.Id);
        }
    }
}
