// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobLead.cs" company="burzyk">
//   Copyright (c) burzyk. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain.Entities
{
    /// <summary>
    /// Represents a single job lead.
    /// </summary>
    public class JobLead
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body that contains the entire lead content.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the minimal daily rate.
        /// </summary>
        public decimal? MinRate { get; set; }

        /// <summary>
        /// Gets or sets the maximal daily rate.
        /// </summary>
        public decimal? MaxRate { get; set; }

        /// <summary>
        /// Gets or sets the recruiter who posted the lead.
        /// </summary>
        public virtual Recruiter Recruiter { get; set; }

        /// <summary>
        /// Gets or sets the search details.
        /// </summary>
        public SearchDetails SearchDetails { get; set; }
    }
}