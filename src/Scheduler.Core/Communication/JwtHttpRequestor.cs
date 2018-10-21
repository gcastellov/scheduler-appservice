using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Scheduler.Core.Communication.ResponseReaders;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class JwtHttpRequestor : HttpRequestorBase
    {
        public JwtHttpRequestor(ICredentialSettings credentialSettings, IResponseReader responseReader) 
            : base(credentialSettings, responseReader)
        {
        }

        public override HttpContent CreateAuthorizationContent(string username, string password, string endpoint)
        {
            var payload = new { username, password };
            return new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        }
    }
}
