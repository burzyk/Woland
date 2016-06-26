// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILeadsProvider.cs" company="burzyk">
//   Copyright (c) burzyk. All rights reserved.
// </copyright>
// <summary>
//   Defines the ILeadsProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Service.Domain
{
    using System;
    using System.Collections.Generic;

    using Woland.Service.Domain.Entities;

    /// <summary>
    /// Extracts leads from the data source.
    /// </summary>
    public interface ILeadsProvider
    {
        /// <summary>
        /// Gets the latest <see cref="JobLead"/> form the underlying data source.
        /// </summary>
        /// <param name="keyword">
        ///     The keyword that is used to search for jobs.
        /// </param>
        /// <param name="location">
        ///     The geographical location of the lead.
        /// </param>
        /// <returns>
        /// The list of leads.
        /// </returns>
        IEnumerable<JobLead> GetLatestLeads(string keyword, string location);
    }
}