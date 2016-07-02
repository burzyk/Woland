namespace Woland.DataAccess
{
    using Domain;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;

    public class EfDataContext : DbContext
    {
        private readonly string connectionString;

        public EfDataContext(ISettingsProvider settings)
        {
            this.connectionString = settings.ConnectionString;
        }

        public DbSet<WebRequestLog> WebRequestLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}