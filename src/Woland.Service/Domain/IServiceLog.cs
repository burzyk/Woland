namespace Woland.Service.Domain
{
    public interface IServiceLog
    {
        void Info(string format, params object[] args);

        void Error(string format, params object[] args);
    }
}