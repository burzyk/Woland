namespace Woland.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity
    {
        public int Id { get; set; }

        [Timestamp]
        public byte[] DbTimestamp { get; set; }
    }
}