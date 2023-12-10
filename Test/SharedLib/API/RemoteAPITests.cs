using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLib.Exceptions;
using Rudeus.API;

namespace Test.SharedLib.API
{
    public class RemoteAPITests
    {
        // TODO: Remoteに影響されないテストの作成方法
        [Fact]
        public void TestRegisterDevice()
        {
            // Arrange
            RemoteAPI.RequestClient = new FakeRequestClient();
            string deviceId = "test_device_id";
            string hostname = "test_hostname";

            // Act
            var response = RemoteAPI.RegisterDevice(deviceId, hostname);

            // Assert
            Assert.Equal("ok", response.status);
        }

        [Fact]
        public void TestLoginDevice()
        {
            // Arrange
            RemoteAPI.RequestClient = new FakeRequestClient();
            string accessToken = "test_access_token";
            string username = "test_username";

            // Act
            var response = RemoteAPI.LoginDevice(accessToken, username);

            // Assert
            Assert.Equal("ok", response.status);
        }

        [Fact]
        public void TestUpdateDevice()
        {
            // Arrange
            RemoteAPI.RequestClient = new FakeRequestClient();
            string accessToken = "test_access_token";
            string hostname = "test_hostname";

            // Act
            var response = RemoteAPI.UpdateDevice(accessToken, hostname);

            // Assert
            Assert.Equal("ok", response.status);
            Assert.Equal(1, response.push_data[0].id);
        }

        [Fact]
        public void TestSendInstalledApps()
        {
            // Arrange
            RemoteAPI.RequestClient = new FakeRequestClient();
            string accessToken = "test_access_token";
            ApplicationData appData = new("Apex Legends", "123.456");
            List<ApplicationData> apps = new List<ApplicationData>() { appData };

            // Act
            var response = RemoteAPI.SendInstalledApps(accessToken, apps);

            // Assert
            Assert.Equal("ok", response.status);
        }


        [Fact]
        public void TestBuildHttpRequestMessage()
        {
            // Arrange
            string accessToken = "test_access_token";
            string requestPath = "test_path";
            string payload = @"{""test_payload"": ""test""}";

            // Act
            var message = RemoteAPI.BuildHttpRequestMessage(accessToken, requestPath, payload, HttpMethod.Get);

            // Assert
            Assert.Equal(HttpMethod.Get, message.Method);
            Assert.Equal(accessToken, message.Headers.Authorization?.Parameter);
            Assert.Equal(requestPath, message.RequestUri?.ToString());
            Assert.Equal(payload, message.Content?.ReadAsStringAsync().Result);
        }


        [Fact]
        public void TestRegisterDeviceException()
        {
            // Arrange
            RemoteAPI.RequestClient = new FakeRequestClient()
            {
                RaiseUnreachableException = true
            };

            // Act
            var exception = Record.Exception(() => RemoteAPI.RegisterDevice("test_device_id", "test_hostname"));

            // Assert
            Assert.IsType<ServerUnavailableException>(exception);
        }
    }
}
