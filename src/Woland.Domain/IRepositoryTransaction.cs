namespace Woland.Domain
{
    using System;

    public interface IRepositoryTransaction : IDisposable
    {
        void Commit();
    }
}