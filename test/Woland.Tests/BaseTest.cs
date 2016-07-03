namespace Woland.Tests
{
    using Microsoft.Practices.Unity;
    using Service;

    public abstract class BaseTest
    {
        public IUnityContainer CreateContainer()
        {
            var container = new UnityContainer();
            UnityConfiguration.ConfigureBindings(container);

            return container;
        }
    }
}