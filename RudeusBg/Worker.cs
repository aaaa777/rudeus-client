using Rudeus.API;
using Rudeus.API.Response;
using Rudeus.Model;
using Rudeus;
using Rudeus.Model.Operations;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Diagnostics.CodeAnalysis;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Diagnostics;
using Windows.System.Inventory;

namespace RudeusBg
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string[] _args;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _args = Program.commandArgs;
            
        }
    

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker running at: {Settings.DeviceId}");

            var argsDict = _args.Select(arg => arg.Split('=')).Where(s => s.Length == 2).ToDictionary(v => v[0], v => v[1]);

            string mode = argsDict.GetValueOrDefault("mode", "default");

            if (!RemoteAPI.IsRemoteReachable())
            {
                _logger.LogInformation("server is not reachable");
                Environment.Exit(0);
            }


            // ������
            ValidateAccessToken();

            // �ʏ�N����
            if (mode == "default")
            {
                // UpdateDevice�̎��s
                UpdateResponse res = await SendRegularReport();
                
                HandlePushDataFromResponse(res);
            }

            if (mode == "login")
            {
                string userIdOld = Settings.Username;
                Console.WriteLine("old user id:" + userIdOld);
                if(userIdOld == "")
                {
                    await StartLoginFlow();
                }
            }

            // �e�X�g�����m�F�p
            if (mode == "test")
            {
                var apps = await InstalledApplications.LoadAsync();
                try
                {
                    RemoteAPI.SendInstalledApps(Settings.AccessToken, apps);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }



#if (!RELEASE)
            await Task.Delay(5000, stoppingToken);
#endif
            Environment.Exit(0);
        }

        private async Task StartLoginFlow()
        {
            try
            {
                // �w��ID���擾���邽��localhost�ŃR�[���o�b�N��ҋ@
                Task<string> userIdTask = RemoteAPI.ReceiveStudentIdAsync();

                // ���O�C����ʂ��J��
                OpenWebPage(RemoteAPI.SamlLoginUrl);
                string userId = await userIdTask;

                // �Ǘ��T�[�o�ɑ��M
                LoginResponse res = RemoteAPI.LoginDevice(Settings.AccessToken, userId);
                Settings.Username = userId;
            }
            catch
            {
                // ���O�C�����đ��M�Ɏ��s
            }
        }

        private void ValidateAccessToken()
        {
            // BgInitializer�����s��������Bg��RegisterDeviceAndSetData�����s����
            if (IsFirstRun())
            {
                // �f�o�C�XID�𔭍s
                Utils.RegisterDeviceAndSetData();
            }


            // �g�p�\�ȃA�N�Z�X�g�[�N�����Ȃ��ꍇ�A�Ĕ��s
            if (!RemoteAPI.IsAccessTokenAvailable(Settings.AccessToken))
            {
                // TODO: �A�N�Z�X�g�[�N���̍Ĕ��s�̂ݍs���悤�ɕύX����
                // NOTE: �Ǘ��T�[�o���̃f�[�^����񂾂Ƃ��ɕs�������N����
                Console.WriteLine("Reregistering as new device but this is not supported in the future");
                Utils.RegisterDeviceAndSetData();
            }
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
                OperationsController.Run(pd.type, pd.payload);
            }
        }


        // UpdateDevice�����s
        private async Task<UpdateResponse> SendRegularReport()
        {
            string accessToken = Settings.AccessToken;

            // set randomized hostname
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            string hostname = $"P{firstNumber}-{secondNumber}";
            Settings.Hostname = hostname;

            UpdateResponse? response = null;
            try
            { 
                response = RemoteAPI.UpdateDevice(accessToken, hostname);
                _logger.LogInformation($"req: changing hostname into `{hostname}` => res: {response.status}");
            }
            catch
            {
                _logger.LogInformation("server connection failed");
            }

            // �C���X�g�[���ς݃A�v�����M
            // TODO: WatchDogs
            try
            {
                List<ApplicationData> installedApps = await InstalledApplications.LoadAsync();
                RemoteAPI.SendInstalledApps(accessToken, installedApps);
            }
            catch
            {
                _logger.LogInformation("failed to send installed apps");
            }


            return response;
        }

        public bool IsFirstRun()
        {
            string accessToken = Settings.AccessToken;
            if(accessToken == null && accessToken == "")
            {
                return true;
            }
            return false;
        }

        private void OpenWebPage(string url)
        {
            new Process
            {
                StartInfo = new ProcessStartInfo(url)
                {
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}