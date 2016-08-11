namespace Woland.Domain.Entities
{
    public abstract class BaseProperty : BaseEntity
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}