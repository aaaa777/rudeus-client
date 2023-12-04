using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Exceptions;

namespace Rudeus.API.Test
{
    public class RemoteAPITests
    {
        // TODO: Remoteに影響されないテストの作成方法
        [Fact]
        public void TestRegisterDevice()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();
            string deviceId = "test_device_id";
            string hostname = "test_hostname";

            var response = RemoteAPI.RegisterDevice(deviceId, hostname);

            Assert.Equal("ok", response.status);
        }

        [Fact]
        public void TestLoginDevice()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();
            string accessToken = "test_access_token";
            string username = "test_username";

            var response = RemoteAPI.LoginDevice(accessToken, username);

            Assert.Equal("ok", response.status);
        }

        [Fact]
        public void TestUpdateDevice()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();
            string accessToken = "test_access_token";
            string hostname = "test_hostname";

            var response = RemoteAPI.UpdateDevice(accessToken, hostname);

            Assert.Equal("ok", response.status);
            Assert.Equal("1", response.push_data[0].id);
        }

        [Fact]
        public void TestSendInstalledApps()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();
            string accessToken = "test_access_token";
            ApplicationData appData = new("Apex Legends", "123.456");
            List<ApplicationData> apps = new List<ApplicationData>() { appData };

            var response = RemoteAPI.SendInstalledApps(accessToken, apps);

            Assert.Equal("ok", response.status);
        }


        [Fact]
        public void TestBuildHttpRequestMessage()
        {
            string accessToken = "test_access_token";
            string requestPath = "test_path";
            string payload = @"{""test_payload"": ""test""}";

            var message = RemoteAPI.BuildHttpRequestMessage(accessToken, requestPath, payload, HttpMethod.Get);

            Assert.Equal(HttpMethod.Get, message.Method);
            Assert.Equal(accessToken, message.Headers.Authorization?.Parameter);
            Assert.Equal(requestPath, message.RequestUri?.ToString());
            Assert.Equal(payload, message.Content?.ReadAsStringAsync().Result);
        }


        [Fact]
        public void TestRegisterDeviceException()
        {
            RemoteAPI.RequestClient = new FakeRequestClient()
            {
                RaiseUnreachableException = true
            };

            var exception = Record.Exception(() => RemoteAPI.RegisterDevice("test_device_id", "test_hostname"));

            Assert.IsType<ServerUnavailableException>(exception);
        }
    }
}
