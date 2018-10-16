using System;

namespace Scheduler.Core
{
    internal interface ISchedulerConfigurator : IDisposable
    {
        void Schedule(string id, string expression);
        void Start();
    }
}