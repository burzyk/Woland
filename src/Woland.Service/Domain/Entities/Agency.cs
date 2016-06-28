namespace Woland.Service.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Agency : BaseEntity
    {
        [StringLength(128)]
        public string Name { get; set; }
    }
}