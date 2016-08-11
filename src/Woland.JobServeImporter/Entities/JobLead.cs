namespace Woland.JobServeImporter.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities;

    public class JobLead
    {
        public string SourceUrl { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public decimal? MinRate { get; set; }

        public decimal? MaxRate { get; set; }

        public DateTime PostedTimestamp { get; set; }

        public string FullName { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string AgencyName { get; set; }

        public string SearchLocation { get; set; }

        public string SearchKeywords { get; set; }
    }
}