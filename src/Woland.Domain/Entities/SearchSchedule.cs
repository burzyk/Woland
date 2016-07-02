namespace Woland.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SearchSchedule : BaseEntity
    {
        public DateTime? LastExecuted { get; set; }

        [Required]
        public SearchDetails SearchDetails { get; set; }
    }
}