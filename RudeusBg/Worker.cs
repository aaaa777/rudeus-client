using Rudeus.Model;
using Rudeus.Model.Response;
using Rudeus.Model.Operations;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Diagnostics.CodeAnalysis;

namespace RudeusBg
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker running at: {Settings.DeviceId}");

            // ������
            Initialize();

            // UpdateDevice�̎��s
            UpdateResponse? res = PostInformation();
            
            HandleResponseData(res);
#if (DEBUG)
            await Task.Delay(5000, stoppingToken);
#endif
            Environment.Exit(0);
        }

        private void Initialize()
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

        private void HandleResponseData(UpdateResponse? res)
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
        private UpdateResponse? PostInformation()
        {
            string accessToken = Settings.AccessToken;

            // set randomized hostname
            Random r1 = new Random();
            string firstNumber = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            string hostname = $"HIU-P{firstNumber}-{secondNumber}";
            Settings.Hostname = hostname;

            try
            { 
                UpdateResponse response = RemoteAPI.UpdateDevice(accessToken, hostname);
                _logger.LogInformation($"req: changing hostname into `{hostname}` => res: {response.status}");
                return response;
            }
            catch
            {
                _logger.LogInformation("server connection failed");
            }
            return null;
        }

        // deprecated
        public void Register()
        {
            // �N������GUID�𐶐�����DevideId�Ƃ��Ă���
            Guid g = System.Guid.NewGuid();
            string guid8 = g.ToString().Substring(0, 8);
            string deviceId = $"000000-{guid8}";
            string hostname = "HIU-P12-234";
            string username = "9999999";

            RegisterResponse response = RemoteAPI.RegisterDevice(deviceId, hostname);
            string accessToken = response.response_data?.access_token ?? throw new Exception("Access Token not found in response");

            RemoteAPI.LoginDevice(accessToken, username);

            Settings.UpdateRegistryKey();
            Settings.AccessToken = accessToken;
            Settings.FirstHostname = hostname;
            Settings.Hostname = hostname;
            Settings.DeviceId = deviceId;
            Settings.Username = username;
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

        private void CallOperation(string opcode)
        {
            //OperationsController.Run(type);
        }
    }
}