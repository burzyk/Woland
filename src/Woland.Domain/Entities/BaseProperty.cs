namespace Woland.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseProperty : BaseEntity
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}