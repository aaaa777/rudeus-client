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
    /// ������s���鏈�������s����葱��
    /// �f�o�C�X���̍X�V�ƃT�[�o�����push_data�̏������s��
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
            // UpdateDevice�̎��s
            UpdateResponse res = await SendRegularReport();
            
            HandlePushDataFromResponse(res);

            await SendMacAddressReport();

            await SendInstalledApps();

            await LocalMachineInfoUpdater.Run();
        }


        private void HandlePushDataFromResponse(UpdateResponse? res)
        {
            // ���X�|���X���Ȃ������ꍇ
            if (res == null)
            {
                Environment.Exit(0);
            }

            // Operation�̏�����
            OperationsController.InitializeDefaultOperations();


            // ���X�|���X��push_data�̃p�[�X����
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
                OperationsController.Run(pd.type, pd.message);
            }
        }

        // UpdateDevice�����s
        private async Task<UpdateResponse> SendRegularReport()
        {
            string accessToken = RootSettings.AccessTokenP;

            UpdateRequest request = Watcher.BuildUpdateRequest();

            // TODO: UpdateRequest��PushData�𕪗�����
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
            // �C���X�g�[���ς݃A�v�����M

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