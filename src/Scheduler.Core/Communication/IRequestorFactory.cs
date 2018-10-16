using Scheduler.Core.Configuration;

namespace Scheduler.Core.Communication
{
    public interface IRequestorFactory
    {
        IHttpRequestor Create(ICredentialSettings settings);
    }
}