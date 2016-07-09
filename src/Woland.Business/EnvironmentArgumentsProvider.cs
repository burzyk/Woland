namespace Woland.Business
{
    using System;
    using System.Collections.Generic;
    using Domain;

    public class EnvironmentArgumentsProvider : ICommandLineArgumentsProvider
    {
        public IList<string> GetArguments()
        {
            return Environment.GetCommandLineArgs();
        }
    }
}