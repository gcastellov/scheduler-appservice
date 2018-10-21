using System.Net.Http;
using System.Threading.Tasks;

namespace Scheduler.Core.Communication
{
    public interface IResponseReader
    {
        Task<string> GetToken(HttpResponseMessage response);
    }
}