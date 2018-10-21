namespace Scheduler.Core.Configuration
{
    public interface ICredentialSettings
    {
        string Id { get; }
        string Type { get; set; }
        string Username { get; }
        string Password { get; }
        string Endpoint { get; }
        string ResponseReader { get; set; }
    }
}
