//-----------------------------------------------------------------------
// <copyright file="IServiceLog.cs" company="burzyk">
//     Copyright (c) burzyk. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Woland.Service.Domain
{
    /// <summary>
    /// Provides logging capabilities for all subsystems.
    /// </summary>
    public interface IServiceLog
    {
        /// <summary>
        /// Records information message into the main log.
        /// </summary>
        void Info(string format, params object[] args);

        /// <summary>
        /// Records error message into the main log.
        /// </summary>
        void Error(string format, params object[] args);
    }
}