using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class OAuthHttpRequestor : HttpRequestorBase
    {
        public OAuthHttpRequestor(ICredentialSettings credentialSettings) : base(credentialSettings)
        {
        }

        public override Task Authorize(string username, string password, string endpoint)
        {
            var payload = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", username },
                { "password", password }
            };

            return Client.PostAsync(endpoint, new FormUrlEncodedContent(payload)).ContinueWith(c =>
            {
                Token = GetTokenResponse(c.Result);
            });
        }
    }
}
