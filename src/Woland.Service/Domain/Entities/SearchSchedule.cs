namespace Woland.Service.Domain.Entities
{
    using System;

    public class SearchSchedule : BaseEntity
    {
        public DateTime LastExecuted { get; set; }

        public SearchDetails SearchDetails { get; set; }
    }
}