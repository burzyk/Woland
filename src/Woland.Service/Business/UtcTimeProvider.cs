namespace Woland.Service.Business
{
    using System;
    using Domain;

    public class UtcTimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.UtcNow;
    }
}