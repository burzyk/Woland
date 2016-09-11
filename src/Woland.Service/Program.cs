namespace Woland.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Ninject;

    /// <summary>
    /// The main application class.
    /// </summary>
    public class Program : IDisposable
    {
        /// <summary>
        /// The worker thread
        /// </summary>
        private readonly Thread workerThread;

        /// <summary>
        /// The API thread.
        /// </summary>
        private readonly Thread apiThread;

        /// <summary>
        /// The source
        /// </summary>
        private readonly CancellationTokenSource source = new CancellationTokenSource();

        /// <summary>
        /// The dependency injection kernel.
        /// </summary>
        private readonly WolandKernel kernel = new WolandKernel(new ApplicationNinjectModule());

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        public Program()
        {
            this.apiThread = new Thread(this.ApiRoutine);
            this.workerThread = new Thread(this.WorkerRoutine);
        }

        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">The arguments.</param>
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

                // program.workerThread.Start(program.source.Token);
                program.apiThread.Start(program.source.Token);

                Console.ReadLine();
            }
        }

        /// <summary>
        /// Cleans up all worker threads.
        /// </summary>
        public void Dispose()
        {
            this.source.Cancel();

            // this.workerThread.Join();
            this.apiThread.Join();

            this.source.Dispose();
            this.kernel.Dispose();
        }

        /// <summary>
        /// The API routine.
        /// </summary>
        /// <param name="arg">The argument.</param>
        private void ApiRoutine(object arg)
        {
            var token = (CancellationToken)arg;

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureServices(x => x.AddTransient(typeof(IReadOnlyKernel), sp => this.kernel))
                .Build();

            host.Run(token);
        }

        /// <summary>
        /// The worker routine.
        /// </summary>
        /// <param name="arg"> The argument.</param>
        private void WorkerRoutine(object arg)
        {
            var token = (CancellationToken)arg;

            var log = this.kernel.Get<ILog>();
            log.Info("========== Initializing service ==========");

            while (!token.IsCancellationRequested)
            {
                IImportManager manager = null;

                try
                {
                    manager = this.kernel.Get<IImportManager>(new WolandKernel.OncePerCallParameter());

                    log.Info("Running importer");
                    manager.Import();
                }
                catch (Exception ex)
                {
                    log.Error($"Unhandled exception when running importer: {ex}");
                }
                finally
                {
                    this.kernel.Release(manager);
                }

                try
                {
                    Task.Delay(TimeSpan.FromHours(1), token).Wait(token);
                }
                catch (TaskCanceledException)
                {
                }
            }
        }
    }
}
