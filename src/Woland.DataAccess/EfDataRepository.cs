namespace Woland.DataAccess
{
    using System.Linq;
    using System.Net;
    using Domain;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class EfDataRepository : IDataRepository
    {
        private readonly EfDataContext context;

        public EfDataRepository(EfDataContext context)
        {
            this.context = context;
            this.context.Database.Migrate();
        }

        public IQueryable<WebRequestLog> WebRequestLogs => this.context.WebRequestLogs;

        public IQueryable<JobLead> JobLeads => this.context.JobLeads;

        public IQueryable<ImportSchedule> ImportSchedules => this.context.ImportSchedules;

        public IRepositoryTransaction BeginTransaction()
        {
            return new Transaction(this.context);
        }

        public T Add<T>(T entity) where T : class
        {
            return this.context.Add(entity).Entity;
        }

        public T Remove<T>(T entity) where T : class
        {
            return this.context.Remove(entity).Entity;
        }

        public T Update<T>(T entity) where T : class
        {
            return this.context.Update(entity).Entity;
        }

        private class Transaction : IRepositoryTransaction
        {
            private readonly EfDataContext context;

            public Transaction(EfDataContext context)
            {
                this.context = context;
            }

            public void Dispose()
            {
            }

            public void Commit()
            {
                this.context.SaveChanges();
            }
        }
    }
}