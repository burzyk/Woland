namespace Woland.Domain
{
    public interface IFileSystem
    {
        string ReadFile(string fileName);

        void SaveFile(string fileName, string content);
    }
}