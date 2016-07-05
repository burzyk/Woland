namespace Woland.DataAccess.Logging
{
    using System;
    using Domain.Entities;

    public class LogEntry : BaseEntity
    {
        public string Message { get; set; }

        public string Level { get; set; }

        public DateTime Timestamp { get; set; }
    }
}