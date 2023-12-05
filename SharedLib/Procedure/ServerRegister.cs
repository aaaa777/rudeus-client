using Rudeus;
using Rudeus.API;
using Rudeus.API.Response;
using Rudeus.Model;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Procedure
{
    /// <summary>
    /// 管理サーバにデバイスを登録する手続き
    /// </summary>
    public class ServerRegister : IProcedure
    {
        public ILocalMachine _localMachine { get; set; }
        public IRootSettings _settings { get; set; }

        public ServerRegister(ILocalMachine? lm = null, IRootSettings? cs = null)
        {
            _localMachine = lm ?? LocalMachine.GetInstance();
            _settings = cs ?? new RootSettings();
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            string hostname = _localMachine.GetHostname();
            string deviceId = _localMachine.GetDeviceId();

            // デフォルトのレジストリにセット
            
            _settings.HostnameP = hostname;
            _settings.FirstHostnameP = hostname;
            _settings.DeviceIdP = deviceId;

            // 発行
            try
            {
                RegisterResponse response = RemoteAPI.RegisterDevice(deviceId, hostname);

                _settings.AccessTokenP = response.response_data.access_token ?? throw new("AccessToken not set");
                Console.WriteLine($"registered device: `{hostname}`: {response.status}");
                return;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("server connection failed, device is not registered yet");
            }
        }
    }
}