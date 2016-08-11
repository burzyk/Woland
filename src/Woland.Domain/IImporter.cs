namespace Woland.Domain
{
    using Entities;

    public interface IImporter
    {
        void Import(ImportSchedule schedule);
    }
}