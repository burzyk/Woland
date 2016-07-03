namespace Woland.Tests.Core
{
    using Domain;

    public class NullLog : ILog
    {
        public void Debug(string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Error(string message)
        {
        }
    }
}