using Chronozoom.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Repositories
{
    /// <summary>
    /// Repository interface for collections.
    /// </summary>
    public interface ICollectionRepository : IRepository<Collection>
    {
        /// <summary>
        /// Gets all public collections.
        /// </summary>
        /// <returns>An enumerable of all public collections.</returns>
        Task<IEnumerable<Collection>> GetPublicCollectionsAsync();
        /// <summary>
        /// Gets all collections of a user.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>An enumerable with collections, can be empty.</returns>
        Task<IEnumerable<Collection>> GetByUserAsync(Guid userId);
        /// <summary>
        /// Gets a user collection by name.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>The named collection of the user, or null if the user or collection are not found.</returns>
        Task<Collection> GetByUserAndNameAsync(Guid userId, string collectionName);
        /// <summary>
        /// Gets the default collection of a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>The default collection of the user, or null if the user has no default collection or no collections at all.</returns>
        Task<Collection> GetUserDefaultAsync(Guid userId);
        /// <summary>
        /// Checks if a user is a member of a collection.
        /// </summary>
        /// <param name="collectionId">The id of the collection.</param>
        /// <param name="userId">The id of the user.</param>
        /// <returns>True if the user is a member, else false.</returns>
        Task<bool> IsMemberAsync(Guid collectionId, Guid userId);

        Task<Collection> FindByTimelineIdAsync(Guid timelineId);

        Task<Collection> FindByNameOrDefaultAsync(string superCollection);
    }
}
