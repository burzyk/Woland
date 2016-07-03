namespace Woland.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class JobLead : BaseEntity
    {
        [Required]
        public string SourceName { get; set; }

        [Required]
        public string SourceUrl { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public decimal? MinRate { get; set; }

        public decimal? MaxRate { get; set; }

        public DateTime? PostedTimestamp { get; set; }

        [StringLength(128)]
        public string FullName { get; set; }

        [StringLength(128)]
        public string Telephone { get; set; }

        [StringLength(128)]
        public string Email { get; set; }

        [StringLength(128)]
        public string AgencyName { get; set; }

        [Required]
        public string SearchLocation { get; set; }

        [Required]
        public string SearchKeywords { get; set; }
    }
}