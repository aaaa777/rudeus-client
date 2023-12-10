using Rudeus.API;
using Rudeus.API.Request;
using Rudeus.API.Response;
using Rudeus.Model;
using SharedLib.Model.Settings;
using Windows.Networking;

namespace Rudeus.Procedure
{
    /// <summary>
    /// �A�N�Z�X�g�[�N���̗L�������m�F���A�����Ȃ�Ĕ��s����葱��
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
            // BgInitializer�����s��������Bg��RegisterDeviceAndSetData�����s����
            if (IsFirstRun())
            {
                // �f�o�C�XID�𔭍s
                RegisterDevice();
            }


            // �g�p�\�ȃA�N�Z�X�g�[�N�����Ȃ��ꍇ�A�Ĕ��s
            if (!RemoteAPI.IsAccessTokenAvailable(_settings.AccessTokenP))
            {
                // TODO: �A�N�Z�X�g�[�N���̍Ĕ��s�̂ݍs���悤�ɕύX����
                // NOTE: �Ǘ��T�[�o���̃f�[�^����񂾂Ƃ��ɕs�������N����
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
            // ���N�G�X�g���쐬
            var req = new RegisterRequest();
            string deviceId = LM.GetDeviceId();
            string productName = LM.GetProductName();
            req.request_data = new RegisterRequestData()
            {
                device_id = deviceId,
                product_name = productName,
            };

            // ���s
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