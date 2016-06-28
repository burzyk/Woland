namespace Woland.Service.Domain
{
    using System;
    using System.Collections.Generic;

    using Entities;

    public interface ILeadsProvider
    {
        IEnumerable<JobLead> GetLatestLeads(string keyword, string location);
    }
}