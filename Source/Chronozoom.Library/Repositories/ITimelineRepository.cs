using Chronozoom.Business.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Repositories
{
    public interface ITimelineRepository : IRepository<Timeline>
    {
        Task<IEnumerable<Timeline>> GetByCollectionAsync(Guid collectionId);
        Task<IEnumerable<Timeline>> GetByTimelineAsync(Guid timelineId);
        Task<IEnumerable<Timeline>> GetRootTimelines(Guid collectionId);
    }
}
