namespace Woland.Tests.Core
{
    using Domain;
    using Microsoft.Practices.Unity;
    using Service;

    public abstract class BaseTest
    {
        public IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            UnityConfiguration.ConfigureBindings(container);

            container.RegisterInstance<ILog>(new NullLog());

            return container;
        }
    }
}