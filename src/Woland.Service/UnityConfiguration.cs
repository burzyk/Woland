//-----------------------------------------------------------------------
// <copyright file="UnityConfiguration.cs" company="burzyk">
//     Copyright (c) burzyk. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Woland.Service
{
    using Business;
    using Domain;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Provides configuration for unity bindings.
    /// </summary>
    public static class UnityConfiguration
    {
        /// <summary>
        /// Configures all unity bindings.
        /// </summary>
        /// <param name="container">
        /// Container to apply the bindings to.
        /// </param>
        public static void ConfigureBindings(IUnityContainer container)
        {
            container.RegisterType<IService, ServiceImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceLog, SimpleLogger>(new ContainerControlledLifetimeManager());
        }
    }
}