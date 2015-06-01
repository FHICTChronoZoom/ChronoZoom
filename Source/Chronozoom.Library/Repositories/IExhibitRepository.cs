using Chronozoom.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Repositories
{
    public interface IExhibitRepository : IRepository<Exhibit>
    {
        Task<IEnumerable<Exhibit>> GetByTimelineAsync(Guid timelineId);
    }
}
