namespace Woland.Domain.Entities
{
    using System.Collections.Generic;

    public interface IComplexObject<TProperty> where TProperty : BaseProperty
    {
        IList<TProperty> Properties { get; set; }

        TDetails GetDetails<TDetails>();

        void SaveDetails<TDetails>(TDetails source);
    }
}