namespace Woland.Domain
{
    public interface IImporter<in T>
    {
        void Import(T details);
    }
}