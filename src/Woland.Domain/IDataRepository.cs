namespace Woland.Domain
{
    using System.Linq;
    using Entities;

    public interface IDataRepository
    {
        IQueryable<WebRequestLog> WebRequestLogs { get; }

        IQueryable<JobLead> JobLeads { get; }

        IRepositoryTransaction BeginTransaction();

        T Add<T>(T entity) where T : class;

        T Remove<T>(T entity) where T : class;

        T Update<T>(T entity) where T : class;
    }
}