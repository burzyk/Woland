namespace Woland.Service
{
    using System;
    using System.Collections.Generic;
    using Business;
    using DataAccess;
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.Practices.Unity;

    public static class UnityConfiguration
    {
        public static void ConfigureBindings(IUnityContainer container)
        {
            container.RegisterType<IService, SimpleService>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILog, SimpleLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsProvider, JsonSettingsProvider>(new ContainerControlledLifetimeManager());

            container.RegisterType<IDataRepository, EfDataRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<EfDataContext>(new HierarchicalLifetimeManager());

            container.RegisterType<Func<IList<ILeadsProvider>>>(
                new InjectionFactory(x => new Func<IList<ILeadsProvider>>(() => new ILeadsProvider[] { x.Resolve<JobServeLeadsProvider>() })));
            container.RegisterType<IWebClient, DefaultWebClient>();
            container.RegisterType<ITimeProvider, UtcTimeProvider>();
            container.RegisterType<ILeadsImporter, LatestLeadsImporter>();
        }
    }
}