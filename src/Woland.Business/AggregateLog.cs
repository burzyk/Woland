namespace Woland.Business
{
    using System;
    using System.Collections.Generic;
    using Domain;

    public class AggregateLog : ILog
    {
        private readonly IList<ILog> logs;

        public AggregateLog(IList<ILog> logs)
        {
            this.logs = logs;
        }

        public void Error(string message)
        {
            this.ForEachLog(x => x.Error(message));
        }

        public void Debug(string message)
        {
            this.ForEachLog(x => x.Debug(message));
        }

        public void Info(string message)
        {
            this.ForEachLog(x => x.Info(message));
        }

        private void ForEachLog(Action<ILog> action)
        {
            foreach (var log in this.logs)
            {
                try
                {
                    action(log);
                }
                catch (Exception)
                {
                    // suppress as there isn't much we can do ...
                }
            }
        }
    }
}