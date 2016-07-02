namespace Woland.Service.Domain
{
    using System;

    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}