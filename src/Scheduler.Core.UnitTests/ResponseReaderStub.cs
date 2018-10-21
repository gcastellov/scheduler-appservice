using System;
using System.Net.Http;
using System.Threading.Tasks;
using Scheduler.Core.Communication;

namespace Scheduler.Core.UnitTests
{
    public class ResponseReaderStub : IResponseReader
    {
        public Task<string> GetToken(HttpResponseMessage response)
        {
            throw new NotImplementedException();
        }
    }
}