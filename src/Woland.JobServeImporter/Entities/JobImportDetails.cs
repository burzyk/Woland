namespace Woland.JobServeImporter.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Entities;

    public class JobImportDetails
    {
        public string SearchLocation { get; set; }

        public string SearchKeywords { get; set; }
    }
}