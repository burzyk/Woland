// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchSchedule.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the SearchSchedule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain.Entities
{
    using System;

    /// <summary>
    /// The search schedule to be performed.
    /// </summary>
    public class SearchSchedule : BaseEntity
    {
        /// <summary>
        /// Gets or sets the date then the search was last executed.
        /// </summary>
        public DateTime LastExecuted { get; set; }

        /// <summary>
        /// Gets or sets the search details.
        /// </summary>
        public SearchDetails SearchDetails { get; set; }
    }
}