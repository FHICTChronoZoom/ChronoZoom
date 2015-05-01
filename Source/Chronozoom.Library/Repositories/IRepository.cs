using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Repositories
{
    public interface IRepository<T>
    {
        Task<T> FindAsync(Guid id);
        Task InsertAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
    }
}
