namespace Woland.Service.Domain.Entities
{
    public class JobLead
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public decimal? MinRate { get; set; }

        public decimal? MaxRate { get; set; }

        public virtual Recruiter Recruiter { get; set; }

        public SearchDetails SearchDetails { get; set; }
    }
}