using Rudeus.API;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rudeus.API.Exceptions;

namespace RudeusTest.RudeusSharedLib.API
{
    public class TestRemoteAPI
    {
        // TODO: Remoteに影響されないテストの作成方法
        [Fact]
        public void TestRegisterDevice()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();

            var response = RemoteAPI.RegisterDevice("test_device_id", "test_hostname");

            Assert.Equal("ok", response.status);
        }

        [Fact]
        public void TestLoginDevice()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();

            var response = RemoteAPI.LoginDevice("test_access_token", "test_user_id");

            Assert.Equal("ok", response.status);
        }

        [Fact]
        public void TestUpdateDevice()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();

            var response = RemoteAPI.UpdateDevice("test_access_token", "test_hostname");

            Assert.Equal("ok", response.status);
            Assert.Equal("1", response.push_data[0].id);
        }

        [Fact]
        public void TestSendInstalledApps()
        {
            RemoteAPI.RequestClient = new FakeRequestClient();

            List<ApplicationData> apps = new List<ApplicationData>() { new("Apex Legends", "123.456") };

            var response = RemoteAPI.SendInstalledApps("test_access_token", apps);

            Assert.Equal("ok", response.status);
        }


        [Fact]
        public void TestBuildHttpRequestMessage()
        {
            var message = RemoteAPI.BuildHttpRequestMessage("test_access_token", "test_path", "test_payload", HttpMethod.Get);

            Assert.Equal(HttpMethod.Get, message.Method);
            Assert.Equal("test_access_token", message.Headers.Authorization.Parameter);
            Assert.Equal("test_path", message.RequestUri.ToString());
            Assert.Equal("test_payload", message.Content.ReadAsStringAsync().Result);
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
