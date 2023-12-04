using Rudeus.API;
using Rudeus.Model;

namespace Rudeus.Procedure
{
    /// <summary>
    /// �A�N�Z�X�g�[�N���̗L�������m�F���A�����Ȃ�Ĕ��s����葱��
    /// </summary>
    public class AccessTokenValidator : IProcedure
    {

        public ISettings _settings { get; set; }

        public AccessTokenValidator(ISettings? cs = null)
        {
            _settings = cs ?? new Settings();
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            // BgInitializer�����s��������Bg��RegisterDeviceAndSetData�����s����
            if (IsFirstRun())
            {
                // �f�o�C�XID�𔭍s
                Utils.RegisterDeviceAndSetData();
            }


            // �g�p�\�ȃA�N�Z�X�g�[�N�����Ȃ��ꍇ�A�Ĕ��s
            if (!RemoteAPI.IsAccessTokenAvailable(_settings.AccessTokenP))
            {
                // TODO: �A�N�Z�X�g�[�N���̍Ĕ��s�̂ݍs���悤�ɕύX����
                // NOTE: �Ǘ��T�[�o���̃f�[�^����񂾂Ƃ��ɕs�������N����
                Console.WriteLine("Reregistering as new device but this is not supported in the future");
                Utils.RegisterDeviceAndSetData();
            }
        }

        public bool IsFirstRun()
        {
            string accessToken = _settings.AccessTokenP;
            return accessToken == null && accessToken == "";
        }
    }
}