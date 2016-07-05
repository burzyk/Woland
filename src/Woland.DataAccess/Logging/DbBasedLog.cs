namespace Woland.DataAccess.Logging
{
    using System;
    using Domain;

    public class DbBasedLog : ILog
    {
        private readonly Func<EfDataContext> contextFactory;

        private readonly ITimeProvider timeProvider;

        public DbBasedLog(Func<EfDataContext> contextFactory, ITimeProvider timeProvider)
        {
            this.contextFactory = contextFactory;
            this.timeProvider = timeProvider;
        }

        public void Error(string message)
        {
            this.Log("ERROR", message);
        }

        public void Debug(string message)
        {
        }

        public void Info(string message)
        {
            this.Log("INFO", message);
        }

        private void Log(string level, string message)
        {
            using (var context = this.contextFactory())
            {
                context.LogEntries.Add(new LogEntry
                {
                    Level = level,
                    Message = message,
                    Timestamp = this.timeProvider.Now
                });
                context.SaveChanges();
            }
        }
    }
}