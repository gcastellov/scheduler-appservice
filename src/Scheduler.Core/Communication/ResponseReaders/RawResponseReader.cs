using System.Net.Http;
using System.Threading.Tasks;
using Scheduler.Core.Communication.ResponseReaders;

namespace Scheduler.Core.Communication
{
    public class RawResponseReader : IResponseReader
    {
        public Task<string> GetToken(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync();
        }
    }
}