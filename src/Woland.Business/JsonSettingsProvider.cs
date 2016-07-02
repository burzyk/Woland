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
        private readonly IServiceLog log;

        private readonly Lazy<InternalSettings> settings;

        public JsonSettingsProvider(IServiceLog log)
        {
            this.log = log;
            this.settings = new Lazy<InternalSettings>(() =>
            {
                this.log.Info("Resolving command line arguments");

                var args = Environment.GetCommandLineArgs().ToList();
                var indexOfConfig = args.IndexOf("config");

                var defaultConfigLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "config.json");
                var configFileName = indexOfConfig != -1 && indexOfConfig + 1 < args.Count ? args[indexOfConfig + 1] : defaultConfigLocation;

                this.log.Info("Using configuration file: '{0}'", configFileName);

                return JsonConvert.DeserializeObject<InternalSettings>(File.ReadAllText(configFileName));
            });
        }

        public string ConnectionString => this.settings.Value.ConnectionString;

        private class InternalSettings
        {
            public string ConnectionString { get; set; }
        }
    }
}