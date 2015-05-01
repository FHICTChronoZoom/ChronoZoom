using Chronozoom.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Repositories
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        Task<IEnumerable<Collection>> GetByUserAsync(Guid userId);

        Task<bool> IsMemberAsync(Guid collectionId, Guid userId);
    }
}
