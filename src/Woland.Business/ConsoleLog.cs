namespace Woland.Business
{
    using System;
    using Domain;

    public class ConsoleLog : ILog
    {
        private readonly ITimeProvider timeProvider;

        public ConsoleLog(ITimeProvider timeProvider)
        {
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
            Console.WriteLine(
                "{0:yyyy-MM-dd HH:mm:ss.ms} [{1}]: {2}",
                this.timeProvider.Now,
                level,
                message);
        }
    }
}