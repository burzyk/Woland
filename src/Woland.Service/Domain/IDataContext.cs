namespace Woland.Service.Domain
{
    using System.Net;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IDataContext
    {
        DbSet<WebRequestLog> WebRequestLogs { get; set; }

        int SaveChanges();
    }
}