namespace Woland.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class ImportResultProperty : BaseProperty
    {
        [Required]
        public virtual ImportResult ImportResult { get; set; }
    }
}