﻿namespace Woland.Tests.Core
{
    using Domain;

    public class NullLog : IServiceLog
    {
        public void Info(string format, params object[] args)
        {
        }

        public void Error(string format, params object[] args)
        {
        }
    }
}