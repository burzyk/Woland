namespace Woland.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Entities;

    public class ProgressiveLeadsImporter : ILeadsImporter
    {
        private readonly int importDelta;

        private readonly ILog log;

        private readonly IDataRepository repository;

        public ProgressiveLeadsImporter(ILog log, IDataRepository repository, ISettingsProvider settings)
        {
            this.log = log;
            this.repository = repository;
            this.importDelta = settings.ProgressiveImporterDelta;
        }

        public void Import(string keyword, string location, IList<ILeadsProvider> providers)
        {
            this.log.Info("Starting import ...");

            foreach (var provider in providers)
            {
                try
                {
                    this.log.Info($"Importing from: {provider.Name}");
                    this.ImportInternal(keyword, location, provider);
                }
                catch (Exception ex)
                {
                    this.log.Error($"Import failed: {ex}");
                }
            }

            this.log.Info("Import Complete");
        }

        private void ImportInternal(string keyword, string location, ILeadsProvider provider)
        {
            var lastLead = this.repository.JobLeads
                .Where(x => x.SourceName == provider.Name)
                .OrderByDescending(x => x.PostedTimestamp)
                .FirstOrDefault() ?? new JobLead();
            this.log.Info($"Last available lead: {lastLead.Title ?? "N/A"}");

            var leads = new List<JobLead>();
            var i = 0;
            var importDone = false;

            do
            {
                var delta = provider.GetLatestLeads(keyword, location, i, this.importDelta).ToList();
                var filtred = delta.TakeWhile(x => !this.LeadsEqual(x, lastLead)).ToList();

                leads.AddRange(filtred);

                i += this.importDelta;
                importDone = !delta.Any() || delta.Count != filtred.Count;
            } while (!importDone);


            this.log.Info($"Imported: {leads.Count} leads");

            using (var tx = this.repository.BeginTransaction())
            {
                this.log.Info("Saving leads to the repository ...");
                leads.ForEach(x => this.repository.Add(x));

                tx.Commit();
            }
        }

        private bool LeadsEqual(JobLead first, JobLead second)
        {
            return first?.Title == second?.Title && first?.Body == second?.Body;
        }
    }
}