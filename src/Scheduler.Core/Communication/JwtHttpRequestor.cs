using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class JwtHttpRequestor : HttpRequestorBase
    {
        public JwtHttpRequestor(ICredentialSettings credentialSettings) : base(credentialSettings)
        {
        }

        public override Task Authorize(string username, string password, string endpoint)
        {
            var payload = new { username, password };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            return Client.PostAsync(endpoint, content).ContinueWith(c =>
            {
                Token = GetTokenResponse(c.Result);
            });
        }
    }
}
