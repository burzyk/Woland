namespace Woland.Domain
{
    public interface ISettingsProvider
    {
        string ConnectionString { get; }
    }
}