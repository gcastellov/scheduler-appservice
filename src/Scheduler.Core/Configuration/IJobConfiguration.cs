namespace Scheduler.Core.Configuration
{
    interface IJobConfiguration
    {
        IJobSettings[] Jobs { get; }
    }
}
