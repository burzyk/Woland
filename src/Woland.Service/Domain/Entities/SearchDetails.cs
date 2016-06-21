// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchDetails.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SearchDetails type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain.Entities
{
    /// <summary>
    /// The search details that is to be performed.
    /// </summary>
    public class SearchDetails
    {
        /// <summary>
        /// Gets or sets the location of the job.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the keywords used for this search.
        /// </summary>
        public string Keywords { get; set; }
    }
}