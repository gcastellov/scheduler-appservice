using System;
using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public class RequestorFactory : IRequestorFactory
    {
        public IHttpRequestor Create(ICredentialSettings credentialSettings)
        {
            if (credentialSettings == null)
            {
                return new HttpRequestor();
            }

            var responseReaderType = Type.GetType(credentialSettings.ResponseReader ?? string.Empty);
            if (responseReaderType == null)
            {
                throw new InvalidOperationException("Invalid response reader type");
            }

            var responseReader = (IResponseReader) Activator.CreateInstance(responseReaderType);

            switch (credentialSettings.Type)
            {
                case "jwt":
                    return new JwtHttpRequestor(credentialSettings, responseReader);
                case "oauth":
                    return new OAuthHttpRequestor(credentialSettings, responseReader);
                default:
                    throw new NotImplementedException("Invalid requestor type");
            }
        }
    }
}