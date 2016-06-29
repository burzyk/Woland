namespace Woland.Service.Business
{
    using Domain;

    public class SimpleService : IService
    {
        private readonly JobServeLeadsProvider leadsProvider;

        public SimpleService(JobServeLeadsProvider leadsProvider)
        {
            this.leadsProvider = leadsProvider;
        }

        public void Start()
        {
            this.leadsProvider.GetLatestLeads("C#", "London", 0, 3);
        }
    }
}