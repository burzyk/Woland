namespace Woland.Service
{
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
            container.RegisterType<IServiceLog, SimpleLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISettingsProvider, JsonSettingsProvider>(new ContainerControlledLifetimeManager());

            container.RegisterType<IDataRepository, EfDataRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<EfDataContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IWebClient, DefaultWebClient>();
            container.RegisterType<ITimeProvider, UtcTimeProvider>();
        }
    }
}