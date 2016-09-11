namespace Woland.Service.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Dto;

    using Woland.DataAccess;
    using Woland.Domain;

    [Route("api-v1/job-leads")]
    public class JobLeadsController : Controller
    {
        private readonly IDataRepository repository;

        public JobLeadsController(IDataRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public BaseReponse<IEnumerable<JobLeadModel>> Get()
        {
            return new BaseReponse<IEnumerable<JobLeadModel>>
            {
                Model = new[] { new JobLeadModel { AgencyName = "some name" } }
            };
        }
    }
}