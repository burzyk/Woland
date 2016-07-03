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

        public EfDataContext()
        {
            // This is for migrations
            this.connectionString = @"Server=DESKTOP-L481L6R\SQLEXPRESS;Database=Woland;User Id=woland;Password=woland";
        }

        public DbSet<WebRequestLog> WebRequestLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}