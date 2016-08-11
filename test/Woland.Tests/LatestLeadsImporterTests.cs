namespace Woland.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Business;
    using Core;
    using Domain;
    using Domain.Entities;
    using JobServeImporter;
    using JobServeImporter.Entities;
    using Moq;
    using Xunit;

    public class LatestLeadsImporterTests : BaseTest
    {
        [Fact]
        public void ImporterWithEmptyRepoTest()
        {
            var schedule = new ImportSchedule();
            schedule.SaveDetails(new JobImportDetails { SearchKeywords = "C#", SearchLocation = "London" });
            var leads = this.ImporterSimpleTest(schedule, new List<ImportResult>());

            Assert.Equal(14, leads.Count);

            Assert.Equal("test1", leads[0].GetDetails<JobLead>().Title);
            Assert.Equal("test10", leads[9].GetDetails<JobLead>().Title);

            Assert.Equal("body1", leads[0].GetDetails<JobLead>().Body);
            Assert.Equal("body10", leads[9].GetDetails<JobLead>().Body);
        }

        [Fact]
        public void ImporterWithNonEmptyRepoTest()
        {
            var schedule = new ImportSchedule();
            schedule.SaveDetails(new JobImportDetails { SearchKeywords = "C#", SearchLocation = "London" });

            var r1 = new ImportResult
            {
                ImportSchedule = schedule,
                Timestamp = new DateTime(2016, 01, 13)
            };
            r1.SaveDetails(new JobLead { Title = "test8", Body = "body8" });

            var leads = this.ImporterSimpleTest(schedule, new List<ImportResult> { r1 });

            Assert.Equal(8, leads.Count);

            Assert.Equal("test1", leads[1].GetDetails<JobLead>().Title);
            Assert.Equal("test5", leads[5].GetDetails<JobLead>().Title);

            Assert.Equal("body1", leads[1].GetDetails<JobLead>().Body);
            Assert.Equal("body5", leads[5].GetDetails<JobLead>().Body);
        }

        private IList<ImportResult> ImporterSimpleTest(ImportSchedule schedule, List<ImportResult> originalLeads)
        {
            var repo = new Mock<IDataRepository>();
            repo.Setup(x => x.ImportResults).Returns(originalLeads.AsQueryable());
            repo.Setup(x => x.BeginTransaction()).Returns(new Mock<IRepositoryTransaction>().Object);
            repo.Setup(x => x.Add(It.IsAny<ImportResult>())).Callback<ImportResult>(originalLeads.Add).Returns<ImportResult>(x => x);

            var settings = new Mock<ISettingsProvider>();
            settings.Setup(x => x.ImporterDelta).Returns(3);

            var importer = new LatestLeadsImporter(new NullLog(), repo.Object, settings.Object, new StaticLeadsProvider());

            importer.Import(schedule);

            return originalLeads;
        }

        private class StaticLeadsProvider : ILeadsProvider
        {
            public string Name => "StaticLeadsProvider";

            public IEnumerable<JobLead> GetLatestLeads(string keyword, string location, int index, int count)
            {
                var leads = new[]
                {
                    new JobLead { Title = "test1", Body = "body1", PostedTimestamp = new DateTime(2016, 01, 20) },
                    new JobLead { Title = "test2", Body = "body2", PostedTimestamp = new DateTime(2016, 01, 19) },
                    new JobLead { Title = "test3", Body = "body3", PostedTimestamp = new DateTime(2016, 01, 18) },
                    new JobLead { Title = "test4", Body = "body4", PostedTimestamp = new DateTime(2016, 01, 17)},
                    new JobLead { Title = "test5", Body = "body5", PostedTimestamp = new DateTime(2016, 01, 16) },
                    new JobLead { Title = "test6", Body = "body6", PostedTimestamp = new DateTime(2016, 01, 15) },
                    new JobLead { Title = "test7", Body = "body7", PostedTimestamp = new DateTime(2016, 01, 14) },
                    new JobLead { Title = "test8", Body = "body8", PostedTimestamp = new DateTime(2016, 01, 13) },
                    new JobLead { Title = "test9", Body = "body9", PostedTimestamp = new DateTime(2016, 01, 12) },
                    new JobLead { Title = "test10", Body = "body10", PostedTimestamp = new DateTime(2016, 01, 11) },
                    new JobLead { Title = "test11", Body = "body11", PostedTimestamp = new DateTime(2016, 01, 10) },
                    new JobLead { Title = "test12", Body = "body12", PostedTimestamp = new DateTime(2016, 01, 9) },
                    new JobLead { Title = "test13", Body = "body13", PostedTimestamp = new DateTime(2016, 01, 8) },
                    new JobLead { Title = "test14", Body = "body14", PostedTimestamp = new DateTime(2016, 01, 7) }
                };

                return leads.Skip(index).Take(count).ToList();
            }
        }
    }
}