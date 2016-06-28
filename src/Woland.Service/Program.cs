namespace Woland.Service
{
    using System;
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

                var provider = container.Resolve<JobServeLeadsProvider>();
                var resule = provider.GetLatestLeads("C#", "London");


                return;

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
