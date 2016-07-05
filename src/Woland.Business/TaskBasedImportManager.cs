namespace Woland.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;

    public class TaskBasedImportManager : IImportManager
    {
        private readonly IDataRepository repository;

        private readonly ILog log;

        private readonly ITimeProvider timeProvider;

        private readonly ILeadsImporter leadsImporter;

        private readonly IList<ILeadsProvider> providers;

        private readonly ISettingsProvider settings;

        public TaskBasedImportManager(
            IDataRepository repository,
            ILog log,
            ITimeProvider timeProvider,
            ILeadsImporter leadsImporter,
            IList<ILeadsProvider> providers,
            ISettingsProvider settings)
        {
            this.repository = repository;
            this.log = log;
            this.timeProvider = timeProvider;
            this.leadsImporter = leadsImporter;
            this.providers = providers;
            this.settings = settings;
        }

        public void Import()
        {
            var threshold = this.timeProvider.Now - this.settings.ImportInterval;
            var importTasks = this.repository.ImportTasks.Where(x => x.LastExecuted == null || x.LastExecuted < threshold).ToList();

            this.log.Info($"Found: {importTasks.Count} import tasks to execute");

            foreach (var task in importTasks)
            {
                try
                {
                    this.log.Info($"Executing import for: {task.SearchKeywords} in {task.SearchLocation}");
                    this.leadsImporter.Import(task.SearchKeywords, task.SearchLocation, this.providers);
                }
                catch (Exception ex)
                {
                    this.log.Error($"Exception while executing import: {ex}");
                }
            }

            this.log.Info("All tasks executed");

            using (var tx = this.repository.BeginTransaction())
            {
                importTasks.ForEach(x => x.LastExecuted = this.timeProvider.Now);
                tx.Commit();
            }
        }
    }
}