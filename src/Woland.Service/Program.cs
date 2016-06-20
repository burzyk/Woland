//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="burzyk">
//     Copyright (c) burzyk. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Woland.Service
{
    using System;
    using Domain;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Application entry point class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Application entry point method.
        /// </summary>
        /// <param name="args">
        /// Runtime arguments passed to the application.
        /// </param>
        public static void Main(string[] args)
        {
            using (var container = new UnityContainer())
            {
                UnityConfiguration.ConfigureBindings(container);
                var log = container.Resolve<IServiceLog>();
                var service = container.Resolve<IService>();

                log.Info("========== Initializing service ==========");
                service.Start();

                log.Info("Waiting for termination");
                Console.ReadKey();

                log.Info("========== Service shutting down ==========");
            }
        }
    }
}
