namespace Woland.Domain
{
    using System;

    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}