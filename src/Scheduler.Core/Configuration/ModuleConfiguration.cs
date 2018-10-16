using Microsoft.Extensions.DependencyInjection;
using Scheduler.Core.Communication;

namespace Scheduler.Core.Configuration
{
    public class ModuleConfiguration
    {
        public static void RegisterComponents(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(typeof(IConfigurationProvider), typeof(ConfigurationProvider));
            serviceCollection.AddTransient(typeof(ISchedulerConfigurator), typeof(SchedulerConfigurator));
            serviceCollection.AddTransient(typeof(ISchedulerApplication), typeof(SchedulerApplication));
            serviceCollection.AddSingleton(typeof(IRequestorFactory), typeof(RequestorFactory));
        }
    }
}