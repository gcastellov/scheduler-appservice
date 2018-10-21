using Scheduler.Core.Configuration;

namespace Scheduler.Core.UnitTests
{
    internal class CredentialSettingsStub : ICredentialSettings
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Endpoint { get; set; }
        public string ResponseReader { get; set; }
    }
}
