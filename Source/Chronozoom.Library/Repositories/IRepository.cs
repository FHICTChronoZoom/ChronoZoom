using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Repositories
{
    /// <summary>
    /// Base interface for repositories.
    /// </summary>
    /// <typeparam name="T">The type the repository will handle.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Finds an object by id.
        /// </summary>
        /// <param name="id">The id of the object to find.</param>
        /// <returns>Returns the object if found, else null.</returns>
        Task<T> FindAsync(Guid id);
        /// <summary>
        /// Inserts an object into the data store.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns></returns>
        Task InsertAsync(T item);
        /// <summary>
        /// Update an object to the date store.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns></returns>
        Task UpdateAsync(T item);
        /// <summary>
        /// Deleted an object from the data store.
        /// </summary>
        /// <param name="id">The id of the object to delete.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
    }
}
