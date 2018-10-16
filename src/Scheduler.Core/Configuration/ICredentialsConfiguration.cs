namespace Scheduler.Core.Configuration
{
    interface ICredentialsConfiguration
    {
        ICredentialSettings[] Credentials { get; }
    }
}
