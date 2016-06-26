// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobServeLeadsProviderTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the JobServeLeadsProviderTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Woland.Tests
{
    using System;

    using Woland.Service;
    using Woland.Service.Business;

    using Xunit;

    /// <summary>
    /// The job serve leads provider test.
    /// </summary>
    public class JobServeLeadsProviderTest
    {
        /// <summary>
        /// Smoke tests getting the leads from job serve.
        /// </summary>
        [Fact]
        public void SmokeTestGettingTheLeadsFromJobServe()
        {
            var provider = new JobServeLeadsProvider();

            var result = provider.GetLatestLeads("C#", "London");

            Assert.NotNull(result);
        }
    }
}
