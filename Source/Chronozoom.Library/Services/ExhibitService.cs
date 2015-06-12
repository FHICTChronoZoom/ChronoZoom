using Chronozoom.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Services
{
    class ExhibitService
    {
        private IExhibitRepository exhibitRepository;

        public ExhibitService(IExhibitRepository exhibitRepository)
        {
            if (exhibitRepository == null) throw new ArgumentNullException("timelineRepository");
            this.exhibitRepository = exhibitRepository;
        }

        public async Task<IEnumerable<Timeline>> GetRootTimelines(Guid collectionId)
        {
            return await timelineRepository.GetRootTimelines(collectionId);
        }
    }
}
