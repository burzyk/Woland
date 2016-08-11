namespace Woland.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.EntityFrameworkCore.Query.Internal;

    public class ImportSchedule : BaseEntity, IComplexObject<ImportScheduleProperty>
    {
        public ImportSchedule()
        {
            this.Properties = new List<ImportScheduleProperty>();
        }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public DateTime? NextRunDate { get; set; }

        public virtual IList<ImportScheduleProperty> Properties { get; set; }
        
        [Required]
        public string ImporterName { get; set; }

        public TDetails GetDetails<TDetails>()
        {
            throw new NotImplementedException();
        }

        public void SaveDetails<TDetails>(TDetails source)
        {
            throw new NotImplementedException();
        }
    }
}