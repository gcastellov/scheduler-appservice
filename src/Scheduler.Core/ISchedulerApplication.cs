using System;

namespace Scheduler.Core
{
    public interface ISchedulerApplication : IDisposable
    {
        void Initialize();
    }
}