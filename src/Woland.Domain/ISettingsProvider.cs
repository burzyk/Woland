namespace Woland.Domain
{
    using System;

    public interface ISettingsProvider
    {
        string ConnectionString { get; }

        TimeSpan WebClientDelay { get; }
    }
}