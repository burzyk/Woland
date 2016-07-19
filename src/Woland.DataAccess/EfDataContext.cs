namespace Woland.DataAccess
{
    using System.Linq;
    using Domain;
    using Domain.Entities;
    using Logging;
    using Microsoft.EntityFrameworkCore;

    public class EfDataContext : DbContext
    {
        private readonly string connectionString;

        public EfDataContext(ISettingsProvider settings)
        {
            this.connectionString = settings.ConnectionString;
        }

        public EfDataContext()
        {
            // This is for migrations
            this.connectionString = @"Server=.\SQLEXPRESS;Database=Woland;User Id=woland;Password=woland";
        }

        public DbSet<WebRequestLog> WebRequestLogs { get; set; }

        public DbSet<JobLead> JobLeads { get; set; }

        public DbSet<ImportSchedule> ImportSchedules { get; set; }

        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}