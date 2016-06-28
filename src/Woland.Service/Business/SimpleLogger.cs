namespace Woland.Service.Business
{
    using System;
    using Domain;

    public class SimpleLogger : IServiceLog
    {
        public void Error(string format, params object[] args)
        {
            this.Log("ERROR", format, args);
        }

        public void Info(string format, params object[] args)
        {
            this.Log("INFO", format, args);
        }

        private void Log(string level, string format, params object[] args)
        {
            Console.WriteLine(
                "{0:yyyy-MM-dd HH:mm:ss.ms} [{1}]: {2}",
                DateTime.Now,
                level,
                string.Format(format, args));
        }
    }
}