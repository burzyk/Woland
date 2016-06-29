namespace Woland.Service.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class SearchDetails
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Keywords { get; set; }
    }
}