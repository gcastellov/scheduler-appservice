using System.Net.Http;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class HttpRequestor : HttpRequestorBase
    {
        public HttpRequestor() : this(null, null) { }

        public HttpRequestor(ICredentialSettings credentialSettings, IResponseReader responseReader) 
            : base(credentialSettings, responseReader)
        {
        }

        public override HttpContent CreateAuthorizationContent(string username, string password, string endpoint)
        {
            throw new System.NotImplementedException();
        }
    }
}