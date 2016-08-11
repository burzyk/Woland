namespace Woland.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ImportScheduleProperty : BaseProperty
    {
        [Required]
        public virtual ImportSchedule ImportSchedule { get; set; }
    }
}