namespace Woland.Business
{
    using System;
    using System.Collections.Generic;
    using Domain;

    public class SimpleService : IService
    {
        private readonly Func<IList<ILeadsProvider>> providerFactory;

        private readonly ILeadsImporter importer;

        private readonly ILog log;

        public SimpleService(Func<IList<ILeadsProvider>> providerFactory, ILeadsImporter importer, ILog log)
        {
            this.providerFactory = providerFactory;
            this.importer = importer;
            this.log = log;
        }

        public void Start()
        {
            this.log.Info("Starting import ...");
            this.importer.Import("C#", "London", this.providerFactory());
            this.log.Info("Import finished");
        }
    }
}