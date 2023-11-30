using Microsoft.VisualBasic;
using Rudeus;
using Rudeus.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants = Rudeus.Constants;

namespace RudeusTest.RudeusSharedLib.API
{
    internal class FakeRequestClient : IRequestClient
    {
        public HttpResponseMessage Request(HttpRequestMessage message)
        {
            throw new NotImplementedException();
        }

        public string RequestString(HttpRequestMessage message)
        {
            string? path = message.RequestUri?.AbsolutePath;

            if (path == null)
            {
                throw new Exception("Request Path not specified");
            }

            if(path == Constants.ApiRegisterPath)
            {
                return RegisterDeviceResponseString;
            }

            if(path == Constants.ApiLoginPath)
            {
                return OKResponseString;
            }

            if(path == Constants.ApiUpdatePath)
            {
                return OKWithPushDataResponseString;
            }
            
            if(path == Constants.ApiUpdateMetadataPath)
            {
                throw new NotImplementedException();
                return OKResponseString;
            }

            if(path == Constants.ApiCheckStatusPath)
            {
                return OKResponseString;
            }

            throw new Exception("Unexpected Request Path");
        }

        public string OKResponseString = @"{""status"": ""OK""}";
        public string RegisterDeviceResponseString = @"{""status"": ""OK"", ""device_id"": ""test_device_id""}";
        public string OKWithPushDataResponseString = @"{""status"": ""OK"", ""push_data"": {""id"": ""1"", ""type"": ""notify_toast"", ""body"": ""test message""}}";
    }
}
