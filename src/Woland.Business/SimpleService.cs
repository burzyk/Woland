namespace Woland.Business
{
    using Domain;

    public class SimpleService : IService
    {
        private readonly JobServeLeadsProvider leadsProvider;

        private readonly ILeadsImporter importer;

        private readonly ILog log;

        public SimpleService(JobServeLeadsProvider leadsProvider, ILeadsImporter importer, ILog log)
        {
            this.leadsProvider = leadsProvider;
            this.importer = importer;
            this.log = log;
        }

        public void Start()
        {
            this.log.Info("Starting import ...");
            this.importer.Import("C#", "London", new[] { this.leadsProvider });
            this.log.Info("Import finished");
        }
    }
}