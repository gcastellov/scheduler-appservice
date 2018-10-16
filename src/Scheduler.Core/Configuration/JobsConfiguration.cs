using System.Configuration;
using System.Linq;

namespace Scheduler.Core.Configuration
{
    class JobsConfiguration : ConfigurationSection, IJobConfiguration
    {
        [ConfigurationProperty("jobs", IsDefaultCollection = false)]
        private JobSettingsCollection JobsCollection => (JobSettingsCollection)base["jobs"];

        public IJobSettings[] Jobs
        {
            get
            {
                var jobs = JobsCollection;
                return jobs.Cast<IJobSettings>().ToArray();
            }
        }
    }
}
