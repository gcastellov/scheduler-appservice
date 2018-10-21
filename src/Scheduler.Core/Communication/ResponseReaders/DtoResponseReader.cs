using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scheduler.Core.Communication.ResponseReaders
{
    public abstract class DtoResponseReader<T> : IResponseReader where T:class 
    {
        public Task<string> GetToken(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().ContinueWith(c =>
            {
                var dto = JsonConvert.DeserializeObject<T>(c.Result);
                return ReadToken(dto);
            });
        }

        protected abstract string ReadToken(T dto);
    }
}