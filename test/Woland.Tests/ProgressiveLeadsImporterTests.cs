namespace Woland.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Business;
    using Core;
    using Domain;
    using Domain.Entities;
    using Moq;
    using Xunit;

    public class ProgressiveLeadsImporterTests : BaseTest
    {
        [Fact]
        public void ImporterWithEmptyRepoTest()
        {
            var leads = this.ImporterSimpleTest(new List<JobLead>());

            Assert.Equal(14, leads.Count);

            Assert.Equal("test1", leads[0].Title);
            Assert.Equal("test10", leads[9].Title);

            Assert.Equal("body1", leads[0].Body);
            Assert.Equal("body10", leads[9].Body);
        }

        [Fact]
        public void ImporterWithNonEmptyRepoTest()
        {
            var leads = this.ImporterSimpleTest(new List<JobLead>
            {
                new JobLead { Title = "test8", Body = "body8", SourceName = "StaticLeadsProvider" }
            });

            Assert.Equal(8, leads.Count);

            Assert.Equal("test1", leads[1].Title);
            Assert.Equal("test5", leads[5].Title);

            Assert.Equal("body1", leads[1].Body);
            Assert.Equal("body5", leads[5].Body);
        }

        private IList<JobLead> ImporterSimpleTest(List<JobLead> originalLeads)
        {
            var repo = new Mock<IDataRepository>();
            repo.Setup(x => x.JobLeads).Returns(originalLeads.AsQueryable());
            repo.Setup(x => x.BeginTransaction()).Returns(new Mock<IRepositoryTransaction>().Object);
            repo.Setup(x => x.Add(It.IsAny<JobLead>())).Callback<JobLead>(originalLeads.Add).Returns<JobLead>(x => x);

            var settings = new Mock<ISettingsProvider>();
            settings.Setup(x => x.ProgressiveImporterDelta).Returns(3);

            var importer = new ProgressiveLeadsImporter(new NullLog(), repo.Object, settings.Object);
            importer.Import("C#", "London", new[] { new StaticLeadsProvider() });

            return originalLeads;
        }

        private class StaticLeadsProvider : ILeadsProvider
        {
            public string Name => "StaticLeadsProvider";

            public IEnumerable<JobLead> GetLatestLeads(string keyword, string location, int index, int count)
            {
                var leads = new[]
                {
                    new JobLead { Title = "test1", Body = "body1" },
                    new JobLead { Title = "test2", Body = "body2" },
                    new JobLead { Title = "test3", Body = "body3" },
                    new JobLead { Title = "test4", Body = "body4" },
                    new JobLead { Title = "test5", Body = "body5" },
                    new JobLead { Title = "test6", Body = "body6" },
                    new JobLead { Title = "test7", Body = "body7" },
                    new JobLead { Title = "test8", Body = "body8" },
                    new JobLead { Title = "test9", Body = "body9" },
                    new JobLead { Title = "test10", Body = "body10" },
                    new JobLead { Title = "test11", Body = "body11" },
                    new JobLead { Title = "test12", Body = "body12" },
                    new JobLead { Title = "test13", Body = "body13" },
                    new JobLead { Title = "test14", Body = "body14" }
                };

                return leads.Skip(index).Take(count).ToList();
            }
        }
    }
}