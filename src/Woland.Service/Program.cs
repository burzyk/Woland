namespace Woland.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.Practices.Unity;

    using Business;

    public class Program : IDisposable
    {
        private readonly Thread workerThread = new Thread(WorkerRoutine);

        private readonly CancellationTokenSource source = new CancellationTokenSource();

        public static void Main(string[] args)
        {
            using (var program = new Program())
            {
                Console.WriteLine(@"|                    _                 _      |");
                Console.WriteLine(@"|     __      _____ | | __ _ _ __   __| |     |");
                Console.WriteLine(@"|     \ \ /\ / / _ \| |/ _` | '_ \ / _` |     |");
                Console.WriteLine(@"|      \ V  V / (_) | | (_| | | | | (_| |     |");
                Console.WriteLine(@"|       \_/\_/ \___/|_|\__,_|_| |_|\__,_|     |");
                Console.WriteLine(@"|                                             |");

                Console.WriteLine(@"Press any key to exit");

                program.workerThread.Start(program.source.Token);

                Console.ReadLine();
            }
        }

        public void Dispose()
        {
            this.source.Cancel();
            this.workerThread.Join();
            this.source.Dispose();
        }

        private static void WorkerRoutine(object arg)
        {
            var token = (CancellationToken)arg;

            using (var container = new UnityContainer())
            {
                UnityConfiguration.ConfigureBindings(container);
                var log = container.Resolve<ILog>();

                log.Info("========== Initializing service ==========");

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        using (var scope = container.CreateChildContainer())
                        {
                            var manager = scope.Resolve<IImportManager>();

                            log.Info("Running importer");
                            manager.Import();
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Unhandled exception when running importer: {ex}");
                    }

                    try
                    {
                        Task.WaitAll(Task.Delay(TimeSpan.FromHours(1), token));
                    }
                    catch (AggregateException)
                    {
                    }
                }
            }
        }
    }
}
