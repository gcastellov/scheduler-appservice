using System.Configuration;

namespace Scheduler.Core.Configuration
{
    internal interface IJobSettings
    {
        [ConfigurationProperty("name", IsRequired = true)]
        string Name { get; set; }

        [ConfigurationProperty("expression", IsRequired = true)]
        string Expression { get; set; }

        [ConfigurationProperty("credentials", IsRequired = true)]
        string Credentials { get; set; }

        [ConfigurationProperty("endpoint", IsRequired = true)]
        string Endpoint { get; set; }
    }
}