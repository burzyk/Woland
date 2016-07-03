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

        public byte[] ReadFile(string fileName)
        {
            this.log.Info($"Reading file: '{fileName}'");
            return File.ReadAllBytes(fileName);
        }

        public void SaveFile(string fileName, byte[] content)
        {
            this.log.Info($"Saving file: '{fileName}'");
            var directory = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(directory))
            {
                this.log.Info("File directory doesn't exist, creating");
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(fileName, content);
        }
    }
}