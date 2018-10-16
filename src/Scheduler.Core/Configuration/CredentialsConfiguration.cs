using System.Configuration;
using System.Linq;

namespace Scheduler.Core.Configuration
{
    class CredentialsConfiguration : ConfigurationSection, ICredentialsConfiguration
    {
        
        [ConfigurationProperty("credentials", IsDefaultCollection = false)]
        private CredentialsSettingsCollection CredentialCollection => (CredentialsSettingsCollection)base["credentials"];

        public ICredentialSettings[] Credentials => CredentialCollection.Cast<ICredentialSettings>().ToArray();
    }
}
