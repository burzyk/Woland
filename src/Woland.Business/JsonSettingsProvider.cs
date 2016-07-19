namespace Woland.Business
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Domain;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class JsonSettingsProvider : ISettingsProvider
    {
        private const string OverridePrefix = "--p:";

        private readonly Lazy<InternalSettings> settings;

        public JsonSettingsProvider(ILog log, IFileSystem fileSystem, ICommandLineArgumentsProvider argumentsProvider)
        {
            this.settings = new Lazy<InternalSettings>(() =>
            {
                log.Info("Resolving command line arguments");

                var args = argumentsProvider.GetArguments();
                var indexOfConfig = args.IndexOf("--config");
                var overrides = args
                    .Select((x, i) => new
                    {
                        Name = x.StartsWith(OverridePrefix) ? x.Substring(OverridePrefix.Length) : null,
                        ValueIndex = i + 1
                    })
                    .Where(x => x.Name != null)
                    .Select(x => new { x.Name, Value = args[x.ValueIndex] })
                    .ToList();

                var defaultConfigLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "config.json");
                var configFileName = indexOfConfig != -1 && indexOfConfig + 1 < args.Count ? args[indexOfConfig + 1] : defaultConfigLocation;

                log.Info($"Using configuration file: '{configFileName}'");
                var configFile = JObject.Parse(fileSystem.ReadFile(configFileName));

                foreach (var item in overrides)
                {
                    if (configFile.Properties().All(x => !x.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        log.Info($"Unknown setting: {item.Name}, ignoring");
                    }
                    else
                    {
                        log.Info($"overriding setting {item.Name}");
                        configFile[item.Name] = item.Value;
                    }
                }

                return JsonConvert.DeserializeObject<InternalSettings>(configFile.ToString());
            });
        }

        public string ConnectionString => this.settings.Value.ConnectionString;

        public TimeSpan WebClientDelay => TimeSpan.FromMilliseconds(this.settings.Value.WebClientDelay);

        public int ImporterDelta => this.settings.Value.ImporterDelta;

        private class InternalSettings
        {
            public string ConnectionString { get; set; }

            public int WebClientDelay { get; set; } = 3;

            public int ImporterDelta { get; set; } = 10;
        }
    }
}