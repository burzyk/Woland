namespace Woland.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Business;
    using Core;
    using Domain;
    using Domain.Entities;
    using Microsoft.Practices.Unity;
    using Moq;
    using Xunit;

    public class ScheduleBasedImportManagerTests : BaseTest
    {
        [Fact]
        public void NoNextExecuteDate()
        {
            var schedules = new[]
            {
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java"
                }
            };

            BaseManagerTest(
                schedules,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Never);

                    Assert.Equal(null, schedules[0].NextRunDate);
                });
        }

        [Fact]
        public void OneScheduleToExecuteOneToOmitTest()
        {
            var schedules = new[]
            {
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    Hour = 13,
                    Minute = 45,
                    NextRunDate = new DateTime(2012, 1, 1, 12, 45, 45)
                },
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    Hour = 14,
                    Minute = 11,
                    NextRunDate = new DateTime(2014, 1, 1, 15, 43, 23)
                }
            };

            BaseManagerTest(
                schedules,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Once);
                    i.Verify(x => x.Import("dotnet", "London", p), Times.Never);

                    Assert.Equal(new DateTime(2013, 3, 21, 13, 45, 0), schedules[0].NextRunDate.Value);
                });
        }

        [Fact]
        public void TwoSchedulesToExecuteTest()
        {
            var schedules = new[]
            {
                 new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    Hour = 13,
                    Minute = 45,
                    NextRunDate = new DateTime(2013, 3, 20, 12, 14, 54)
                },
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    Hour = 14,
                    Minute = 11,
                    NextRunDate = new DateTime(2013, 3, 19, 15, 43, 23)
                }
            };

            BaseManagerTest(
                schedules,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Once);
                    i.Verify(x => x.Import("dotnet", "London", p), Times.Once);

                    Assert.Equal(new DateTime(2013, 3, 21, 13, 45, 0), schedules[0].NextRunDate.Value);
                    Assert.Equal(new DateTime(2013, 3, 21, 14, 11, 0), schedules[1].NextRunDate.Value);
                });
        }

        [Fact]
        public void NoSchedulesToExecuteTest()
        {
            var schedules = new[]
            {
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    NextRunDate = new DateTime(2014, 1, 1)
                },
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    NextRunDate = new DateTime(2014, 10, 1)
                }
            };

            BaseManagerTest(
                schedules,
                (i, p) => i.Verify(x => x.Import(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IList<ILeadsProvider>>()), Times.Never));
        }

        [Fact]
        public void IntervalTestWithOnePassedOneNotAndOneOutsideIntervalTest()
        {
            var schedules = new[]
            {
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    Hour = 14,
                    Minute = 11,
                    NextRunDate = new DateTime(2013, 3, 19)
                },
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    NextRunDate = new DateTime(2013, 3, 20, 12, 14, 55)
                },
                new ImportSchedule
                {
                    SearchLocation = "London",
                    SearchKeywords = "SCADA",
                    NextRunDate = new DateTime(2014, 3, 17)
                }
            };

            BaseManagerTest(
                schedules,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Once);
                    i.Verify(x => x.Import("dotnet", "London", p), Times.Never);
                    i.Verify(x => x.Import("SCADA", "London", p), Times.Never);

                    Assert.Equal(new DateTime(2013, 3, 21, 14, 11, 0), schedules[0].NextRunDate.Value);
                });
        }

        private static void BaseManagerTest(IList<ImportSchedule> schedules, Action<Mock<ILeadsImporter>, IList<ILeadsProvider>> verifyCallback)
        {
            var tx = new Mock<IRepositoryTransaction>();

            var repo = new Mock<IDataRepository>();
            repo.Setup(x => x.ImportSchedules).Returns(schedules.AsQueryable());
            repo.Setup(x => x.BeginTransaction()).Returns(tx.Object);

            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.Setup(x => x.Now).Returns(new DateTime(2013, 3, 20, 12, 14, 55));

            var providers = new[] { new Mock<ILeadsProvider>().Object };

            var settings = new Mock<ISettingsProvider>();
            settings.Setup(x => x.ImportInterval).Returns(TimeSpan.FromDays(3));

            var importer = new Mock<ILeadsImporter>();

            var manager = new ScheduleBasedImportManager(
                repo.Object,
                new NullLog(),
                timeProvider.Object,
                importer.Object,
                providers,
                settings.Object);
            manager.Import();

            tx.Verify(x => x.Commit(), Times.Once);

            verifyCallback(importer, providers);
        }
    }
}