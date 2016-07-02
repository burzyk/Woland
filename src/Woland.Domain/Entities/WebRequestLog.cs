namespace Woland.Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WebRequestLog : BaseEntity
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string Request { get; set; }

        [Required]
        public string Response { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }

        [Required]
        public int ResponseCode { get; set; }
    }
}