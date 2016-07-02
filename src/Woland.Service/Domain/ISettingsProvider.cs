namespace Woland.Service.Domain
{
    public interface ISettingsProvider
    {
        string ConnectionString { get; }
    }
}