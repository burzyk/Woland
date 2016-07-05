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
            container.RegisterType<ISettingsProvider, JsonSettingsProvider>(new ContainerControlledLifetimeManager());
            container.RegisterType<EfDataContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IList<ILeadsProvider>>(
                new InjectionFactory(x => new ILeadsProvider[] { x.Resolve<JobServeLeadsProvider>() }));
            container.RegisterType<IWebClient, DefaultWebClient>();
            container.RegisterType<ITimeProvider, UtcTimeProvider>();
            container.RegisterType<ILeadsImporter, LatestLeadsImporter>();
            container.RegisterType<IImportManager, TaskBasedImportManager>();
            container.RegisterType<ILog, SimpleLogger>();
            container.RegisterType<IDataRepository, EfDataRepository>();
            container.RegisterType<IFileSystem, HddFileSystem>();
        }
    }
}