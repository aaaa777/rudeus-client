using Rudeus.API;
using Rudeus.API.Request;
using Rudeus.API.Response;
using Rudeus.Model;
using Rudeus.Model.Operations;
using Rudeus.Watchdogs;
using SharedLib.Model.Settings;

namespace Rudeus.Procedure
{
    /// <summary>
    /// 定期実行する処理を実行する手続き
    /// デバイス情報の更新とサーバからのpush_dataの処理を行う
    /// </summary>
    public class RegularExecuter : IProcedure
    {
        public UpdateWatcher Watcher { get; set; }

        public IRootSettings RootSettings { get; set; }
        public IProcedure LocalMachineInfoUpdater { get; set; }

        public RegularExecuter(UpdateWatcher? watcher = null, IRootSettings? settings = null, IProcedure localMachineInfoUpdater = null)
        {
            RootSettings = settings ?? new RootSettings();
            Watcher = watcher ?? new UpdateWatcher();
            LocalMachineInfoUpdater = localMachineInfoUpdater ?? new CacheUpdater();
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            // UpdateDeviceの実行
            UpdateResponse res = await SendRegularReport();

            // var res = GetPushData();
            
            HandlePushDataFromResponse(res);

            await SendMacAddressReport();

            await SendInstalledApps();

            await LocalMachineInfoUpdater.Run();
        }

        private PushDataResponse GetPushData()
        {
            string accessToken = RootSettings.AccessTokenP;
            return RemoteAPI.GetPushDataResponse(accessToken);
        }

        private void HandlePushDataFromResponse(UpdateResponse? res)
        {
            // レスポンスがなかった場合
            if (res == null)
            {
                Environment.Exit(0);
            }

            // Operationの初期化
            OperationsController.InitializeDefaultOperations();


            // レスポンスのpush_dataのパース処理
            var pdList = res.push_data;
            if (pdList == null)
            {
                Environment.Exit(0);
            }

            foreach (PushResponseData pd in pdList)
            {
                if (pd.type == null)
                {
                    continue;
                }
                OperationsController.Run(pd.type, pd.payload);
            }
        }

        // UpdateDeviceを実行
        private async Task<UpdateResponse> SendRegularReport()
        {
            string accessToken = RootSettings.AccessTokenP;

            UpdateRequest request = Watcher.BuildUpdateRequest();

            // TODO: UpdateRequestとPushDataを分離する
            //if(request == null)
            //{
            //    return;
            //}
            //request.request_data.hostname = hostname;
            UpdateResponse response;
            try
            {
                response = RemoteAPI.UpdateDevice(accessToken, request);
                Console.WriteLine($"res: {response.status}");
            }
            catch
            {
                Console.WriteLine("server connection failed");
                throw;
            }

            return response;
        }

        private async Task SendMacAddressReport()
        {
            string accessToken = RootSettings.AccessTokenP;

            UpdateMacRequest? request = Watcher.BuildUpdateMacRequest();

            if(request == null)
            {
                return;
            }

            BaseResponse response;
            try
            {
                response = RemoteAPI.UpdateMacAddress(accessToken, request);
                Console.WriteLine($"req: changing mac_address into `{request.request_data.ToString()}` => res: {response.status}");
            }
            catch
            {
                Console.WriteLine("server connection failed");
                throw;
            }
        }

        private async Task SendInstalledApps()
        {
            var request = Watcher.BuildSendInstalledAppsRequest();
            if(request == null)
            {
                return;
            }

            string accessToken = RootSettings.AccessTokenP;
            // インストール済みアプリ送信

            try
            {
                RemoteAPI.SendInstalledApps(accessToken, request);
            }
            catch
            {
                Console.WriteLine("failed to send installed apps");
                throw;
            }
        }
    }
}