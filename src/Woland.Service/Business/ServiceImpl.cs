namespace Woland.Service.Business
{
    using Domain;

    public class ServiceImpl : IService
    {
        private readonly IServiceLog log;

        public ServiceImpl(IServiceLog log)
        {
            this.log = log;
        }

        public void Dispose()
        {
        }

        public void Start()
        {
            this.log.Info("Test info");
        }
    }
}