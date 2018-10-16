using System.Net.Http;
using System.Threading.Tasks;

namespace Scheduler.Core.Communication
{
    public interface IHttpRequestor
    {
        string Token { get; }
        Task Authorize(string username, string password, string endpoint);
        Task<HttpResponseMessage> Post(string endpoint);
    }
}