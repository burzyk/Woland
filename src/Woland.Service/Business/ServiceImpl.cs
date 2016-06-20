//-----------------------------------------------------------------------
// <copyright file="ServiceImpl.cs" company="burzyk">
//     Copyright (c) burzyk. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Woland.Service.Business
{
    using System;
    using Woland.Service.Domain;

    /// <inheritDoc />
    public class ServiceImpl : IService
    {
        /// <summary>
        /// Used to log all service messages.
        /// </summary>
        private readonly IServiceLog log;

        /// <summary>
        /// Creates an instance of <see cref="ServiceImpl" /> class.
        /// </summary>
        public ServiceImpl(IServiceLog log)
        {
            this.log = log;
        }

        /// <summary>
        /// Stops all service threads.
        /// </summary>
        public void Dispose()
        {
        }

        /// <inheritDoc />
        public void Start()
        {
            this.log.Info("Test info");
        }
    }
}