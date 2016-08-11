namespace Woland.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;

    public class ScheduleBasedImportManager : IImportManager
    {
        public void Import()
        {
            throw new NotImplementedException();
            /*
            var now = this.timeProvider.Now;
            var importSchedules = this.repository.ImportSchedules.Where(x => x.NextRunDate != null && x.NextRunDate < now).ToList();

            using (var tx = this.repository.BeginTransaction())
            {
                foreach (var x in importSchedules)
                {
                    var tomorrow = now.AddDays(1);
                    x.NextRunDate = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, x.Hour, x.Minute, 0);
                }

                tx.Commit();
            }

            this.log.Info($"Found: {importSchedules.Count} import schedules to execute");

            foreach (var schedule in importSchedules)
            {
                try
                {
                    this.log.Info($"Executing import for: {schedule.SearchKeywords} in {schedule.SearchLocation}");
                    this.leadsImporter.Import(schedule.SearchKeywords, schedule.SearchLocation, this.providers);
                }
                catch (Exception ex)
                {
                    this.log.Error($"Exception while executing import: {ex}");
                }
            }

            this.log.Info("All schedules executed");
            */
        }
    }
}