using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Scheduler.Core.Communication;
using Scheduler.Core.Configuration;

namespace Scheduler.Core
{
    public class JobEvent : IJob
    {
        private ILogger _logger;
        private IConfigurationProvider _configuration;
        private IRequestorFactory _requestorFactory;

        public async Task Execute(IJobExecutionContext context)
        {
            GetDependencies(context);
            var job = _configuration.GetJobSettings(context.Trigger.Description);
            
            _logger.LogInformation("Executing: {0} at: {1}", job.Name, DateTime.UtcNow);

            try
            {
                if (!string.IsNullOrEmpty(job.Credentials))
                {                
                    var credential = _configuration.GetCredentials(job.Credentials);
                    var requestor = _requestorFactory.Create(credential);
                    await requestor.Authorize(credential.Username, credential.Password, credential.Endpoint);
                    await requestor.Post(job.Endpoint);
                }
                else
                {
                    var requestor = _requestorFactory.Create(null);
                    await requestor.Post(job.Endpoint);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An exception has been thrown whilst executing the jon: {0}", job.Name);
            }

            _logger.LogInformation("{0} completed at: {1}", job.Name, DateTime.UtcNow);
        }       

        private void GetDependencies(IJobExecutionContext context)
        {
            var loggerFactory = (ILoggerFactory)context.Scheduler.Context.Get(SchedulerConfigurator.LoggerContextKey);
            _configuration = (IConfigurationProvider)context.Scheduler.Context.Get(SchedulerConfigurator.ConfigurationKey);
            _requestorFactory = (IRequestorFactory) context.Scheduler.Context.Get(SchedulerConfigurator.RequestorKey);
            _logger = loggerFactory.CreateLogger<JobEvent>();
        }
    }
}
