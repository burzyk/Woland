namespace Woland.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImportTask : BaseEntity
    {
        public DateTime? LastExecuted { get; set; }

        [Required]
        public string SearchLocation { get; set; }

        [Required]
        public string SearchKeywords { get; set; }
    }
}