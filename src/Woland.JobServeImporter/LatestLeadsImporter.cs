namespace Woland.JobServeImporter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Entities;
    using Entities;

    public class LatestLeadsImporter : IImporter
    {
        private readonly int importDelta;

        private readonly ILog log;

        private readonly IDataRepository repository;

        private readonly ILeadsProvider provider;

        public LatestLeadsImporter(ILog log, IDataRepository repository, ISettingsProvider settings, ILeadsProvider provider)
        {
            this.log = log;
            this.repository = repository;
            this.provider = provider;
            this.importDelta = settings.ImporterDelta;
        }

        public DateTime LatestLeadDate { get; set; } = DateTime.MaxValue;

        public void Import(ImportSchedule schedule)
        {
            try
            {
                var details = schedule.GetDetails<JobImportDetails>();

                this.log.Info($"Importing from: {schedule.ImporterName}");
                this.ImportInternal(details.SearchKeywords, details.SearchLocation, schedule);
                this.log.Info("Import Complete");
            }
            catch (Exception ex)
            {
                this.log.Error($"Import failed: {ex}");
            }
        }

        private void ImportInternal(string keyword, string location, ImportSchedule schedule)
        {
            var lastResult = this.repository.ImportResults
                .Where(x => x.ImportSchedule == schedule)
                .Where(x => x.Timestamp < this.LatestLeadDate)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefault() ?? new ImportResult();
            var lastLead = lastResult.GetDetails<JobLead>();
            this.log.Info($"Last available lead: {lastLead.Title ?? "N/A"}");

            var i = 0;
            var importDone = false;

            do
            {
                this.log.Info($"Getting leads -> index: {i}, pageSize: {this.importDelta}");
                var delta = this.provider.GetLatestLeads(keyword, location, i, this.importDelta).ToList();
                var filtered = delta.TakeWhile(x => x.PostedTimestamp > lastResult.Timestamp).ToList();

                using (var tx = this.repository.BeginTransaction())
                {
                    this.log.Info("Saving leads to the repository ...");
                    filtered.ForEach(x => this.repository.Add(x));

                    foreach (var jobLead in filtered)
                    {
                        var result = new ImportResult
                        {
                            ImportSchedule = schedule,
                            Timestamp = jobLead.PostedTimestamp
                        };
                        result.SaveDetails(jobLead);
                        this.repository.Add(result);
                    }

                    tx.Commit();
                }

                i += this.importDelta;
                importDone = !delta.Any() || delta.Count != filtered.Count;
            } while (!importDone);
        }
    }
}