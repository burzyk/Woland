namespace Woland.Service
{
    using Business;
    using Domain;
    using Microsoft.Practices.Unity;

    public static class UnityConfiguration
    {
        public static void ConfigureBindings(IUnityContainer container)
        {
            container.RegisterType<IService, ServiceImpl>(new ContainerControlledLifetimeManager());
            container.RegisterType<IServiceLog, SimpleLogger>(new ContainerControlledLifetimeManager());
            container.RegisterType<IWebClient, DefaultWebClient>();
        }
    }
}