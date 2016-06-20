//-----------------------------------------------------------------------
// <copyright file="SimpleLogger.cs" company="burzyk">
//     Copyright (c) burzyk. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Woland.Service.Business
{
    using System;
    using Woland.Service.Domain;

    /// <inheritDoc />
    public class SimpleLogger : IServiceLog
    {
        /// <inheritDoc />
        public void Error(string format, params object[] args)
        {
            this.Log("ERROR", format, args);
        }

        /// <inheritDoc />
        public void Info(string format, params object[] args)
        {
            this.Log("INFO", format, args);
        }

        private void Log(string level, string format, params object[] args)
        {
            Console.WriteLine(
                "{0:yyyy-MM-dd HH:mm:ss.ms} [{1}]: {2}",
                DateTime.Now,
                level,
                string.Format(format, args));
        }
    }
}