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

    public class TaskBasedImportManagerTests : BaseTest
    {


        [Fact]
        public void NotExecutedYetTaskTest()
        {
            var tasks = new[] {new ImportTask {SearchLocation = "London", SearchKeywords = "Java"}};

            BaseManagerTest(
                tasks,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Once);

                    Assert.Equal(new DateTime(2013, 3, 20), tasks[0].LastExecuted.Value);
                });
        }

        [Fact]
        public void OneTaskToExecuteOneToOmitTest()
        {
            var tasks = new[]
            {
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    LastExecuted = new DateTime(2012, 1, 1)
                },
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    LastExecuted = new DateTime(2014, 1, 1)
                }
            };

            BaseManagerTest(
                tasks,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Once);
                    i.Verify(x => x.Import("dotnet", "London", p), Times.Never);

                    Assert.Equal(new DateTime(2013, 3, 20), tasks[0].LastExecuted.Value);
                });
        }

        [Fact]
        public void TwoTasksToExecuteTest()
        {
            var tasks = new[]
            {
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    LastExecuted = new DateTime(2012, 1, 1)
                },
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    LastExecuted = new DateTime(2012, 10, 1)
                }
            };

            BaseManagerTest(
                tasks,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Once);
                    i.Verify(x => x.Import("dotnet", "London", p), Times.Once);

                    Assert.Equal(new DateTime(2013, 3, 20), tasks[0].LastExecuted.Value);
                    Assert.Equal(new DateTime(2013, 3, 20), tasks[1].LastExecuted.Value);
                });
        }

        [Fact]
        public void NoTasksToExecuteTest()
        {
            var tasks = new[]
            {
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    LastExecuted = new DateTime(2014, 1, 1)
                },
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    LastExecuted = new DateTime(2014, 10, 1)
                }
            };

            BaseManagerTest(
                tasks,
                (i, p) => i.Verify(x => x.Import(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IList<ILeadsProvider>>()), Times.Never));
        }

        [Fact]
        public void IntervalTestWithOnePassedOneNotAndOneOutsideIntervalTest()
        {
            var tasks = new[]
            {
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "Java",
                    LastExecuted = new DateTime(2013, 3, 1)
                },
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "dotnet",
                    LastExecuted = new DateTime(2014, 3, 20)
                },
                new ImportTask
                {
                    SearchLocation = "London",
                    SearchKeywords = "SCADA",
                    LastExecuted = new DateTime(2014, 3, 17)
                }
            };

            BaseManagerTest(
                tasks,
                (i, p) =>
                {
                    i.Verify(x => x.Import("Java", "London", p), Times.Once);
                    i.Verify(x => x.Import("dotnet", "London", p), Times.Never);
                    i.Verify(x => x.Import("SCADA", "London", p), Times.Never);

                    Assert.Equal(new DateTime(2013, 3, 20), tasks[0].LastExecuted.Value);
                });
        }

        private static void BaseManagerTest(IList<ImportTask> tasks, Action<Mock<ILeadsImporter>, IList<ILeadsProvider>> verifyCallback)
        {
            var tx = new Mock<IRepositoryTransaction>();

            var repo = new Mock<IDataRepository>();
            repo.Setup(x => x.ImportTasks).Returns(tasks.AsQueryable());
            repo.Setup(x => x.BeginTransaction()).Returns(tx.Object);

            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.Setup(x => x.Now).Returns(new DateTime(2013, 3, 20));

            var providers = new[] { new Mock<ILeadsProvider>().Object };

            var settings = new Mock<ISettingsProvider>();
            settings.Setup(x => x.ImportInterval).Returns(TimeSpan.FromDays(3));

            var importer = new Mock<ILeadsImporter>();

            var manager = new TaskBasedImportManager(
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