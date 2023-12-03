using Rudeus.API;
using Rudeus.Model;

namespace Rudeus.Procedure
{
    /// <summary>
    /// アクセストークンの有効性を確認し、無効なら再発行する手続き
    /// </summary>
    internal class AccessTokenValidator : IProcedure
    {

        public ISettings _settings { get; set; }

        public AccessTokenValidator(ISettings? cs = null)
        {
            _settings = cs ?? new Settings();
        }

        /// <inheritdoc/>
        public async Task Run()
        {
            // BgInitializerが失敗した時にBgがRegisterDeviceAndSetDataを実行する
            if (IsFirstRun())
            {
                // デバイスIDを発行
                Utils.RegisterDeviceAndSetData();
            }


            // 使用可能なアクセストークンがない場合、再発行
            if (!RemoteAPI.IsAccessTokenAvailable(_settings.AccessTokenP))
            {
                // TODO: アクセストークンの再発行のみ行うように変更する
                // NOTE: 管理サーバ側のデータが飛んだときに不整合が起きる
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