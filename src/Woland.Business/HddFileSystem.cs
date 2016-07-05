namespace Woland.Business
{
    using System.IO;
    using Domain;

    public class HddFileSystem : IFileSystem
    {
        private readonly ILog log;

        public HddFileSystem(ILog log)
        {
            this.log = log;
        }

        public string ReadFile(string fileName)
        {
            this.log.Info($"Reading file: '{fileName}'");
            return File.ReadAllText(fileName);
        }

        public void SaveFile(string fileName, string content)
        {
            this.log.Info($"Saving file: '{fileName}'");
            var directory = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(directory))
            {
                this.log.Info("File directory doesn't exist, creating");
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fileName, content);
        }
    }
}