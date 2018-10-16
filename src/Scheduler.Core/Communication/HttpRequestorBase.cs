using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class HttpRequestorBase : IHttpRequestor
    {
        protected readonly HttpClient Client;
        protected readonly ICredentialSettings CredentialSettings;

        public string Token { get; protected set; }

        public HttpRequestorBase(ICredentialSettings credentialSettings)
        {
            Client = new HttpClient();
            CredentialSettings = credentialSettings;
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> Post(string endpoint)
        {
            if (!string.IsNullOrEmpty(Token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }

            var response = await Client.PostAsync(endpoint, new StringContent(""));
            response.EnsureSuccessStatusCode();
            return response;
        }

        public virtual Task Authorize(string username, string password, string endpoint)
        {
            throw new NotImplementedException();
        }

        protected string GetTokenResponse(HttpResponseMessage authResponse)
        {
            var content = authResponse.Content.ReadAsStringAsync().Result;
            var type = Type.GetType(CredentialSettings.ResponseType);
            var authorization = JsonConvert.DeserializeObject(content, type);
            return type.GetProperty(CredentialSettings.ResponseTokenPayload).GetValue(authorization).ToString();
        }
    }
}
