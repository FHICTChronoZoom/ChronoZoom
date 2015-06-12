using Chronozoom.Business.Models;
using Chronozoom.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronozoom.Business.Services
{
    public class ExhibitService : IExhibitRepository
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
            // There's extended logic in the original WCF implementation which cannot be implemented due to the missing
            // ContentItemRepository
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
            // There's extended logic in the original WCF implementation which cannot be implemented due to the missing
            // ContentItemRepository
            return await exhibitRepository.UpdateAsync(exhibitRequest);
        }

        /// <summary>
        /// Deletes an exhibit based on the specified Id
        /// </summary>
        /// <param name="superCollectionName">The superCollection to remove the exhibit from (unused)</param>
        /// <param name="exhibitRequest">The exhibit to be deleted</param>
        /// <returns></returns>
        public async Task<bool> DeleteExhibit(string superCollectionName, Exhibit exhibitRequest)
        {
            return await exhibitRepository.DeleteAsync(exhibitRequest.Id);
        }

        /// <summary>
        /// Deletes an exhibit based on the specified Id
        /// </summary>
        /// <param name="superCollectionName">The superCollection to remove the exhibit from (unused)</param>
        /// <param name="collectionName">The collection to remove the exhibit from (unused)</param>
        /// <param name="exhibitRequest">The exhibit to be deleted</param>
        /// <returns></returns>
        public async Task<bool> DeleteExhibit(string superCollectionName, string collectionName, Exhibit exhibitRequest)
        {
            return await exhibitRepository.DeleteAsync(exhibitRequest.Id);
        }
    }
}
