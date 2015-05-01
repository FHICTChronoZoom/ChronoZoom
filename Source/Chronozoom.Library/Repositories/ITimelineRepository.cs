using Chronozoom.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Library.Repositories
{
    public interface ITimelineRepository : IRepository<Timeline>
    {
        Task<IEnumerable<Timeline>> GetByCollectionAsync(Guid collectionId);
        Task<IEnumerable<Timeline>> GetByTimelineAsync(Guid timelineId);
    }
}
