using System.Configuration;

namespace Scheduler.Core.Configuration
{
    class CredentialSettings : ConfigurationElement, ICredentialSettings
    {
        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get => (string) base["id"];
            set => base["id"] = value;
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get => (string)base["type"];
            set => base["type"] = value;
        }


        [ConfigurationProperty("username", IsRequired = true)]
        public string Username
        {
            get => (string)base["username"];
            set => base["username"] = value;
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get => (string)base["password"];
            set => base["password"] = value;
        }

        [ConfigurationProperty("endpoint", IsRequired = true)]
        public string Endpoint
        {
            get => (string)base["endpoint"];
            set => base["endpoint"] = value;
        }

        [ConfigurationProperty("responseReader", IsRequired = true)]
        public string ResponseReader
        {
            get => (string)base["responseReader"];
            set => base["responseReader"] = value;
        }
    }
}
