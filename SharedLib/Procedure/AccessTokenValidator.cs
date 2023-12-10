using Rudeus.API;
using Rudeus.API.Request;
using Rudeus.API.Response;
using Rudeus.Model;
using SharedLib.Model.Settings;
using Windows.Networking;

namespace Rudeus.Procedure
{
    /// <summary>
    /// アクセストークンの有効性を確認し、無効なら再発行する手続き
    /// </summary>
    public class AccessTokenValidator : IProcedure
    {

        public IRootSettings _settings { get; set; }
        public ILocalMachine LM { get; set; }

        public AccessTokenValidator(IRootSettings? cs = null, ILocalMachine? lm = null)
        {
            _settings = cs ?? new RootSettings();
            LM = lm ?? LocalMachine.GetInstance();
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            // BgInitializerが失敗した時にBgがRegisterDeviceAndSetDataを実行する
            if (IsFirstRun())
            {
                // デバイスIDを発行
                RegisterDevice();
            }


            // 使用可能なアクセストークンがない場合、再発行
            if (!RemoteAPI.IsAccessTokenAvailable(_settings.AccessTokenP))
            {
                // TODO: アクセストークンの再発行のみ行うように変更する
                // NOTE: 管理サーバ側のデータが飛んだときに不整合が起きる
                Console.WriteLine("Reregistering as new device but this is not supported in the future");
                RegisterDevice();
            }
        }

        public bool IsFirstRun()
        {
            string accessToken = _settings.AccessTokenP;
            return accessToken == null || accessToken == "";
        }

        private void RegisterDevice()
        {
            // リクエストを作成
            var req = new RegisterRequest();
            string deviceId = LM.GetDeviceId();
            string productName = LM.GetProductName();
            req.request_data = new RegisterRequestData()
            {
                device_id = deviceId,
                product_name = productName,
            };

            // 発行
            try
            {
                RegisterResponse response = RemoteAPI.RegisterDevice(req);

                _settings.AccessTokenP = response.response_data.access_token ?? throw new("AccessToken not set");
                Console.WriteLine($"registered device: `{productName}`: {response.status}");
                return;
            }
            catch
            {
                Console.WriteLine("server connection failed, device is not registered yet");
                throw;
            }
        }
    }
}