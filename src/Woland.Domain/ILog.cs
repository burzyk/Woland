namespace Woland.Domain
{
    public interface ILog
    {
        void Debug(string message);

        void Info(string message);

        void Error(string message);
    }
}