namespace Scheduler.Core.Configuration
{
    internal interface IConfigurationProvider
    {
        ICredentialsConfiguration CredentialsSettings { get; }
        IJobConfiguration JobSettings { get; }

        IJobSettings GetJobSettings(string name);
        ICredentialSettings GetCredentials(string id);
    }
}