//-----------------------------------------------------------------------
// <copyright file="IService.cs" company="burzyk">
//     Copyright (c) burzyk. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Woland.Service.Domain
{
    using System;

    /// <summary>
    /// Main service responsible for bootstrapping the application and
    /// managing the lifecycle of all importers.
    /// </summary>
    public interface IService : IDisposable
    {
        /// <summary>
        /// Starts the service and initializes all importers.
        /// </summary>
        void Start();
    }
}