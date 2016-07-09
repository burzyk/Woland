namespace Woland.Domain
{
    using System.Collections.Generic;

    public interface ICommandLineArgumentsProvider
    {
        IList<string> GetArguments();
    }
}