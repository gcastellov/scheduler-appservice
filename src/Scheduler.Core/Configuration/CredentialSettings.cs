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

        [ConfigurationProperty("responseType", IsRequired = true)]
        public string ResponseType
        {
            get => (string)base["responseType"];
            set => base["responseType"] = value;
        }

        [ConfigurationProperty("responseTokenPayload", IsRequired = true)]
        public string ResponseTokenPayload
        {
            get => (string)base["responseTokenPayload"];
            set => base["responseTokenPayload"] = value;
        }
    }
}
