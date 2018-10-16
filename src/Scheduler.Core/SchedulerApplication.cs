using System.Linq;
using Microsoft.Extensions.Logging;
using Scheduler.Core.Configuration;

namespace Scheduler.Core
{
    internal class SchedulerApplication : ISchedulerApplication
    {
        private readonly ISchedulerConfigurator _scheduler;
        private readonly IConfigurationProvider _configuration;
        private readonly ILogger _logger;

        public SchedulerApplication(
            ISchedulerConfigurator scheduler, 
            IConfigurationProvider configuration, 
            ILogger<SchedulerApplication> logger)
        {
            _scheduler = scheduler;
            _configuration = configuration;
            _logger = logger;
        }

        public void Initialize()
        {
            var jobs = _configuration.JobSettings.Jobs.ToArray();

            if (jobs.Length > 0)
            {
                foreach (var job in jobs)
                {
                    _scheduler.Schedule(job.Name, job.Expression);
                }

                _scheduler.Start();
                _logger.LogInformation("All jobs have been scheduled");
            }
            else
            {
                _logger.LogWarning("Nothing to schedule. Check your configuration file");
            }
        }

        public void Dispose()
        {
            _scheduler?.Dispose();
        }
    }
}
