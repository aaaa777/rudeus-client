using Rudeus.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudeusTest.RudeusSharedLib.API
{
    internal class FakeRequestClient : IRequestClient
    {
        public HttpResponseMessage Request(HttpRequestMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
