namespace Woland.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ImportResult : BaseEntity, IComplexObject<ImportResultProperty>
    {
        public ImportResult()
        {
            this.Properties = new List<ImportResultProperty>();
        }

        public DateTime Timestamp { get; set; }

        [Required]
        public virtual ImportSchedule ImportSchedule { get; set; }

        public virtual IList<ImportResultProperty> Properties { get; set; }

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