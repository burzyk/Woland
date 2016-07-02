namespace Woland.Domain
{
    using System.Collections.Generic;
    using Entities;

    public interface ILeadsProvider
    {
        IEnumerable<JobLead> GetLatestLeads(string keyword, string location, int index, int count);
    }
}