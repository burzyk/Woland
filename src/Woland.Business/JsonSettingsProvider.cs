namespace Woland.Business
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Domain;
    using Newtonsoft.Json;

    public class JsonSettingsProvider : ISettingsProvider
    {
        private readonly Lazy<InternalSettings> settings;

        public JsonSettingsProvider(ILog log, IFileSystem fileSystem)
        {
            this.settings = new Lazy<InternalSettings>(() =>
            {
                log.Info("Resolving command line arguments");

                var args = Environment.GetCommandLineArgs().ToList();
                var indexOfConfig = args.IndexOf("--config");

                var defaultConfigLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "config.json");
                var configFileName = indexOfConfig != -1 && indexOfConfig + 1 < args.Count ? args[indexOfConfig + 1] : defaultConfigLocation;

                log.Info($"Using configuration file: '{configFileName}'");

                return JsonConvert.DeserializeObject<InternalSettings>(fileSystem.ReadFile(configFileName));
            });
        }

        public string ConnectionString => this.settings.Value.ConnectionString;

        public TimeSpan WebClientDelay => TimeSpan.FromMilliseconds(this.settings.Value.WebClientDelay);

        public int ImporterDelta => this.settings.Value.ImporterDelta;

        public TimeSpan ImportInterval => TimeSpan.FromMinutes(this.settings.Value.ImportIntervalMinutes);

        private class InternalSettings
        {
            public string ConnectionString { get; set; }

            public int WebClientDelay { get; set; } = 3;

            public int ImporterDelta { get; set; } = 10;

            public int ImportIntervalMinutes { get; set; } = 30;
        }
    }
}