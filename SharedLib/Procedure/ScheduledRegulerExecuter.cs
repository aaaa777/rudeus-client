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
    public class ScheduledRegularExecuter : IProcedure
    {
        public UpdateWatcher Watcher { get; set; }

        public IRootSettings RootSettings { get; set; }
        public IProcedure LocalMachineInfoUpdater { get; set; }

        public ScheduledRegularExecuter(UpdateWatcher? watcher = null, IRootSettings? settings = null, IProcedure localMachineInfoUpdater = null)
        {
            RootSettings = settings ?? new RootSettings();
            Watcher = watcher ?? new UpdateWatcher();
            LocalMachineInfoUpdater = localMachineInfoUpdater ?? new LocalMachineInfoUpdater();
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            // UpdateDeviceの実行
            UpdateResponse res = await SendRegularReport();

            HandlePushDataFromResponse(res);
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

            // set randomized hostname
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            string hostname = $"P{firstNumber}-{secondNumber}";

            UpdateRequest request = Watcher.BuildUpdateRequestWithChanges();
            request.request_data.hostname = hostname;
            UpdateResponse response;
            try
            {
                response = RemoteAPI.UpdateDevice(accessToken, request);
                Console.WriteLine($"req: changing hostname into `{hostname}` => res: {response.status}");
            }
            catch
            {
                Console.WriteLine("server connection failed");
                throw;
            }
            LocalMachineInfoUpdater.Run();
            RootSettings.HostnameP = hostname;

            return response;
        }
    }
}