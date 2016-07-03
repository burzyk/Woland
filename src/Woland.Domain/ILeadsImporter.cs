namespace Woland.Domain
{
    using System.Collections.Generic;

    public interface ILeadsImporter
    {
        void Import(string keyword, string location, IList<ILeadsProvider> providers);
    }
}