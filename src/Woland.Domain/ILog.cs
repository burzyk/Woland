namespace Woland.Domain
{
    public interface ILog
    {
        void Info(string message);

        void Error(string message);
    }
}