namespace Woland.Tests
{
    using System;
    using System.Reflection;
    using Business;
    using Core;
    using Domain;
    using Moq;
    using Xunit;

    public class JsonSettingsProviderTests : BaseTest
    {
        [Fact]
        public void AllSettingsTest()
        {
            var configJson = @"{ connectionString: ""kama"", webClientDelay: ""10"", importerDelta: ""88"", importIntervalMinutes: ""34""  }";

            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(configJson);

            var settings = new JsonSettingsProvider(new NullLog(), fs.Object);

            Assert.Equal("kama", settings.ConnectionString);
            Assert.Equal(TimeSpan.FromMilliseconds(10), settings.WebClientDelay);
            Assert.Equal(88, settings.ImporterDelta);
            Assert.Equal(TimeSpan.FromMinutes(34), settings.ImportInterval);
        }
    }
}