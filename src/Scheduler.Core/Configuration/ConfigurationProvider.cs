using System.Configuration;
using System.Linq;

namespace Scheduler.Core.Configuration
{
    class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider()
        {
            CredentialsSettings = (CredentialsConfiguration)ConfigurationManager.GetSection("credentialStorage");
            JobSettings = (JobsConfiguration)ConfigurationManager.GetSection("jobSettings");
        }

        public ICredentialsConfiguration CredentialsSettings { get; }

        public IJobConfiguration JobSettings { get; }

        public IJobSettings GetJobSettings(string name)
        {
            return JobSettings.Jobs.FirstOrDefault(j => j.Name == name);
        }

        public ICredentialSettings GetCredentials(string id)
        {
            return CredentialsSettings.Credentials.FirstOrDefault(c => c.Id == id);
        }
    }
}
