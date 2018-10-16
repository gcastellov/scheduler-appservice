using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Scheduler.Core.Communication;
using Scheduler.Core.Configuration;

namespace Scheduler.Core
{
    internal class SchedulerConfigurator : ISchedulerConfigurator
    {
        private readonly IScheduler _scheduler;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfigurationProvider _configuration;
        private readonly IRequestorFactory _requestorFactory;

        public static string LoggerContextKey = "logger";
        public static string ConfigurationKey = "configuration";
        public static string RequestorKey = "requestor";

        public SchedulerConfigurator(
            ILoggerFactory loggerFactory, 
            IConfigurationProvider configuration, 
            IRequestorFactory requestorFactory)
        {
            _loggerFactory = loggerFactory;
            _configuration = configuration;
            _requestorFactory = requestorFactory;
            _scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
        }
        
        public void Schedule(string id, string expression)
        {
            var job = JobBuilder.Create<JobEvent>().Build();
            var builder = TriggerBuilder.Create()
                .WithIdentity(id)
                .WithDescription(id)
                .StartNow()
                .WithSchedule(CronScheduleBuilder.CronSchedule(expression))
                .Build();

            _scheduler.ScheduleJob(job, builder);
            SetSchedulerContext();
        }

        public void Start()
        {
            _scheduler.Start();
        }
       
        public void Dispose()
        {
            _scheduler.Shutdown();
        }

        private void SetSchedulerContext()
        {
            SetObjectContext(LoggerContextKey, _loggerFactory);
            SetObjectContext(ConfigurationKey, _configuration);
            SetObjectContext(RequestorKey, _requestorFactory);
        }

        private void SetObjectContext(string key, object value)
        {
            if (!_scheduler.Context.ContainsKey(key))
            {
                _scheduler.Context.Put(key, value);
            }
        }
    }
}
