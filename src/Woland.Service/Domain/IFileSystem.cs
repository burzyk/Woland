namespace Woland.Service.Domain
{
    public interface IFileSystem
    {
        byte[] ReadFile(string fileName);

        void SaveFile(string fileName, byte[] content);
    }
}