namespace Woland.Service.Domain.Entities
{
    public class Recruiter : BaseEntity
    {
        public virtual Agency Agency { get; set; }

        public string FullName { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }
    }
}