namespace Woland.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImportSchedule : BaseEntity
    {
        public int Hour { get; set; }

        public int Minute { get; set; }

        public DateTime? NextRunDate { get; set; }

        [Required]
        public string SearchLocation { get; set; }

        [Required]
        public string SearchKeywords { get; set; }
    }
}