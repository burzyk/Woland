namespace Woland.Service
{
    using System;
    using System.Threading;
    using Domain;
    using Microsoft.Practices.Unity;

    using Business;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var container = new UnityContainer())
            {
                UnityConfiguration.ConfigureBindings(container);
                var log = container.Resolve<ILog>();

                log.Info("========== Initializing service ==========");

                while (true)
                {
                    try
                    {
                        using (var scope = container.CreateChildContainer())
                        {
                            var manager = scope.Resolve<IImportManager>();

                            log.Info("Running importer");
                            manager.Import();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Unhandled exception when running importer: {ex}");
                    }

                    Thread.Sleep(TimeSpan.FromHours(1));
                }
            }
        }
    }
}
