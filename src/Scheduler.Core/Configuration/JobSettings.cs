using System.Configuration;

namespace Scheduler.Core.Configuration
{
    class JobSettings : ConfigurationElement, IJobSettings
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get => (string)base["name"];
            set => base["name"] = value;
        }

        [ConfigurationProperty("expression", IsRequired = true)]
        public string Expression
        {
            get => (string)base["expression"];
            set => base["expression"] = value;
        }

        [ConfigurationProperty("credentials", IsRequired = true)]
        public string Credentials
        {
            get => (string)base["credentials"];
            set => base["credentials"] = value;
        }

        [ConfigurationProperty("endpoint", IsRequired = true)]
        public string Endpoint
        {
            get => (string)base["endpoint"];
            set => base["endpoint"] = value;
        }
    }
}
