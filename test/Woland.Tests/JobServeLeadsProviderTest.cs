namespace Woland.Tests
{
    using System;

    using Service;
    using Service.Business;

    using Xunit;

    public class JobServeLeadsProviderTest
    {
        [Fact]
        public void SmokeTestGettingTheLeadsFromJobServe()
        {
            var provider = new JobServeLeadsProvider(new DefaultWebClient());

            var result = provider.GetLatestLeads("C#", "London");

            Assert.NotNull(result);
        }
    }
}
