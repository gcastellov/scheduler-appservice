using System.Collections.Generic;
using System.Net.Http;
using Scheduler.Core.Communication.ResponseReaders;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class OAuthHttpRequestor : HttpRequestorBase
    {
        public OAuthHttpRequestor(ICredentialSettings credentialSettings, IResponseReader responseReader) 
            : base(credentialSettings, responseReader)
        {
        }

        public override HttpContent CreateAuthorizationContent(string username, string password, string endpoint)
        {
            var payload = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", username },
                { "password", password }
            };

            return new FormUrlEncodedContent(payload);
        }
    }
}
