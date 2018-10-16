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
                return new HttpRequestorBase(null);
            }

            switch (credentialSettings.Type)
            {
                case "jwt":
                    return new JwtHttpRequestor(credentialSettings);
                case "oauth":
                    return new OAuthHttpRequestor(credentialSettings);
                default:
                    throw new NotImplementedException("Invalid requestor type");
            }
        }
    }
}