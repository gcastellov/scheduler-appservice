using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Scheduler.Core;

namespace Scheduler.AppService
{
    internal class AppHostedService : IHostedService
    {
        private readonly ILogger<AppHostedService> _logger;
        private readonly ISchedulerApplication _scheduler;

        public AppHostedService(ILogger<AppHostedService> logger, ISchedulerApplication scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }

        //public AppHostedService(ILogger<AppHostedService> logger, IPollas pollas)
        //{
        //    _logger = logger;
        //    pollas.WritePollas();
        //}

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting service...");
            _scheduler.Initialize();
            _logger.LogInformation("Service started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping service...");
            _scheduler.Dispose();
            _logger.LogInformation("Service stopped");
            return Task.CompletedTask;
        }
    }
}
