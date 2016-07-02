namespace Woland.Business
{
    using System.Text;
    using Domain;

    public static class FileSystemExtensions
    {
        public static string ReadFileAsString(this IFileSystem fs, string fileName)
        {
            return Encoding.UTF8.GetString(fs.ReadFile(fileName));
        }
    }
}