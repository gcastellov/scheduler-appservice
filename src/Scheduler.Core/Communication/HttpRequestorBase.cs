﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Scheduler.Core.Communication.ResponseReaders;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class HttpRequestorBase : IHttpRequestor
    {
        protected readonly HttpClient Client;
        protected readonly ICredentialSettings CredentialSettings;
        protected readonly IResponseReader ResponseReader;

        public string Token { get; protected set; }

        public HttpRequestorBase(): this(null, null) { }

        protected HttpRequestorBase(ICredentialSettings credentialSettings, IResponseReader responseReader)
        {
            Client = new HttpClient();
            CredentialSettings = credentialSettings;
            ResponseReader = responseReader;
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

        public async Task Authorize(string username, string password, string endpoint)
        {
            var content = CreateAuthorizationContent(username, password, endpoint);
            var response = await Client.PostAsync(endpoint, content);
            await ResponseReader.GetToken(response).ContinueWith(c =>
            {
                Token = c.Result;
            });
        }

        protected virtual HttpContent CreateAuthorizationContent(string username, string password, string endpoint)
        {
            throw new System.NotImplementedException("Must implement this method in case you use authorization");
        }
    }
}
