namespace Woland.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Business;
    using DataAccess;
    using DataAccess.Logging;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Ninject;
    using Ninject.Modules;

    public class ApplicationNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISettingsProvider>().To<JsonSettingsProvider>().InSingletonScope();
            this.Bind<EfDataContext>().ToSelf().InOncePerCallScope();
            this.Bind<Func<EfDataContext>>().ToMethod(x => () => x.Kernel.Get<EfDataContext>());
            this.Bind<ILeadsProvider>().To<JobServeLeadsProvider>();
            this.Bind<IWebClient>().To<DefaultWebClient>();
            this.Bind<ITimeProvider>().To<UtcTimeProvider>();
            this.Bind<ILeadsImporter>().To<LatestLeadsImporter>();
            this.Bind<IImportManager>().To<ScheduleBasedImportManager>();
            this.Bind<ILog>().ToMethod(x => new AggregateLog(new ILog[] { x.Kernel.Get<ConsoleLog>(), x.Kernel.Get<DbBasedLog>() }));
            this.Bind<IDataRepository>().To<EfDataRepository>();
            this.Bind<IFileSystem>().To<HddFileSystem>();
            this.Bind<ICommandLineArgumentsProvider>().To<EnvironmentArgumentsProvider>();
        }
    }
}