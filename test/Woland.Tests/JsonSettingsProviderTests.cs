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
        public void AllSettingsNoOverridesTest()
        {
            var configJson = @"{ connectionString: ""kama"", webClientDelay: ""10"", importerDelta: ""88"", importIntervalMinutes: ""34""  }";

            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(configJson);

            var args = new Mock<ICommandLineArgumentsProvider>();
            args.Setup(x => x.GetArguments()).Returns(new string[] { });

            var settings = new JsonSettingsProvider(new NullLog(), fs.Object, args.Object);

            Assert.Equal("kama", settings.ConnectionString);
            Assert.Equal(TimeSpan.FromMilliseconds(10), settings.WebClientDelay);
            Assert.Equal(88, settings.ImporterDelta);
        }

        [Fact]
        public void ValidOverrideTest()
        {
            var configJson = @"{ connectionString: ""kama"" }";

            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(configJson);

            var args = new Mock<ICommandLineArgumentsProvider>();
            args.Setup(x => x.GetArguments()).Returns(new[] { "--p:connectionString", "ala ma kota" });

            var settings = new JsonSettingsProvider(new NullLog(), fs.Object, args.Object);

            Assert.Equal("ala ma kota", settings.ConnectionString);
        }

        [Fact]
        public void ValidOverrideInvalidCasingTest()
        {
            var configJson = @"{ connectionString: ""kama"" }";

            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(configJson);

            var args = new Mock<ICommandLineArgumentsProvider>();
            args.Setup(x => x.GetArguments()).Returns(new[] { "--p:ConnectionString", "ala ma kota" });

            var settings = new JsonSettingsProvider(new NullLog(), fs.Object, args.Object);

            Assert.Equal("ala ma kota", settings.ConnectionString);
        }

        [Fact]
        public void UnknownOverrideTest()
        {
            var configJson = @"{ connectionString: ""kama"" }";

            var fs = new Mock<IFileSystem>();
            fs.Setup(x => x.ReadFile(It.IsAny<string>())).Returns(configJson);

            var args = new Mock<ICommandLineArgumentsProvider>();
            args.Setup(x => x.GetArguments()).Returns(new[] { "--p:ConnectionStringas", "ala ma kota" });

            var settings = new JsonSettingsProvider(new NullLog(), fs.Object, args.Object);

            Assert.Equal("kama", settings.ConnectionString);
        }
    }
}