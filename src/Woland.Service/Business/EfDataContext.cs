namespace Woland.Service.Business
{
    using System.Net;
    using Domain;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class EfDataContext : DbContext, IDataContext
    {
        private readonly string connectionString;

        public EfDataContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbSet<WebRequestLog> WebRequestLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}