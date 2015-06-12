using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Services
{
    class ExhibitService : IExhibitRepository
    {
        private IExhibitRepository exhibitRepository;

        public ExhibitService(IExhibitRepository exhibitRepository)
        {
            if (exhibitRepository == null) throw new ArgumentNullException("timelineRepository");
            this.exhibitRepository = exhibitRepository;
        }

        /// <summary>
        /// Updates an exhibit. The Id of the exhibit is used to identify the exhibit to be updated.
        /// </summary>
        /// <param name="superColletionName">SuperCollection name of the exhibit (unused)</param>
        /// <param name="exhibitRequest">The updated exhibit</param>
        /// <returns>Boolean determining whether the update was succesful (true) or not (false)</returns>
        public async Task<bool> PutExhibit(string superColletionName, Exhibit exhibitRequest)
        {
            return await PutExhibit(superColletionName, "", exhibitRequest);
        }

        /// <summary>
        /// Updates an exhibit. The Id of the exhibit is used to identify the exhibit to be updated.
        /// </summary>
        /// <param name="superColletionName">SuperCollection name of the exhibit (unused)</param>
        /// <param name="collectionName">CollectionName of the exhibit (unused)</param>
        /// <param name="exhibitRequest">The updated exhibit</param>
        /// <returns>Boolean determining whether the update was succesful (true) or not (false)</returns>
        public async Task<bool> PutExhibit(string superColletionName, string collectionName, Exhibit exhibitRequest)
        {
            return await exhibitRepository.UpdateAsync(exhibitRequest);
        }
    }
}
