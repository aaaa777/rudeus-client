using Microsoft.VisualBasic;
using Rudeus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants = Rudeus.Constants;
using SharedLib.Exceptions;
using Rudeus.API;

namespace Test.SharedLib.API
{
    public class FakeRequestClient : IRequestClient
    {
        public bool RaiseUnreachableException;
        public bool RaiseUnauthorizedException;
        public bool RaiseUnexceptResponseException;

        public FakeRequestClient()
        {
            RaiseUnreachableException = false;
            RaiseUnauthorizedException = false;
            RaiseUnexceptResponseException = false;
        }

        public FakeRequestClient(bool raiseUnreachableException, bool raiseUnauthorizedException, bool raiseUnexceptResponseException)
        {
            RaiseUnreachableException = raiseUnreachableException;
            RaiseUnauthorizedException = raiseUnauthorizedException;
            RaiseUnexceptResponseException = raiseUnexceptResponseException;
        }
        public HttpResponseMessage Request(HttpRequestMessage message)
        {
            throw new NotImplementedException();
        }

        public string RequestString(HttpRequestMessage message)
        {
            string? path = message.RequestUri.ToString();

            if (path == null)
            {
                throw new Exception("Request Path not specified");
            }


            // Dummy Exception

            if(RaiseUnreachableException)
            {
                throw new ServerUnavailableException("Server Unreachable");
            }

            if(RaiseUnauthorizedException)
            {
                throw new AccessTokenUnavailableException("Access Token Unavailable");
            }

            if(RaiseUnexceptResponseException)
            {
                throw new UnexpectedResponseException("Unexpected Response");
            }


            // Dummy Response

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
                //return OKResponseString;
            }

            if(path == Constants.ApiCheckStatusPath)
            {
                return OKResponseString;
            }

            if(path == Constants.ApiSendInstalledAppsPath)
            {
                return OKResponseString;
            }

            throw new Exception("FakeRequestClientに設定されていないAPIパスです");
        }

        public string OKResponseString = @"{""status"": ""ok""}";
        public string RegisterDeviceResponseString = @"{""status"": ""ok"", ""product_id"": ""test_device_id""}";
        public string OKWithPushDataResponseString = @"{""status"": ""ok"", ""push_data"": [ {""id"": 1, ""type"": ""notify_toast"", ""body"": ""test message""} ] }";
    }
}
