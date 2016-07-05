namespace Woland.Business
{
    using System.Linq;
    using Domain;

    public class ScheduledImportManager : IImportManager
    {
        private readonly IDataRepository repository;

        private readonly ILog log;

        private readonly ITimeProvider timeProvider;

        public ScheduledImportManager(IDataRepository repository, ILog log, ITimeProvider timeProvider)
        {
            this.repository = repository;
            this.log = log;
            this.timeProvider = timeProvider;
        }

        public void Import()
        {
            var now = this.timeProvider.Now;
            var schedules = this.repository.ImportTasks.Where(x => x.LastExecuted < now).ToList();

            this.log.Info($"Found: {schedules.Count} schedules ");
        }
    }
}