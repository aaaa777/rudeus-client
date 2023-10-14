using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    class Constants
    {
        public static readonly string WebPortalUrl = "https://portal.do-johodai.ac.jp";
        public static readonly string Polite3Url = "https://polite3.do-johodai.ac.jp";
        public static readonly string KyoumuUrl = "https://eduweb.do-johodai.ac.jp";

        public static readonly string RudeusBgDir = "c:\\Program Files\\Windows System Application\\";
        public static readonly string RudeusBgExe = "svrhost.exe";

        public static readonly string RudeusBgFormDir = "c:\\Program Files\\HIU\\System Manager\\";
        public static readonly string RudeusBgFormExe = "RudeusBgForm.exe";

        // レジストリ：アプリのデフォルトのキー
        private static string DefaultRegistryKey = @"Config";

        // レジストリ：アプリのルート
        private static string RegistryDir = @"Software\Test App";

        public static readonly string CallbackPort = "11178";

        public static string ApiRegisterPath = "/api/device_initialize";
        public static string ApiUpdatePath = "/api/device_update";
        public static string ApiLoginPath = "/api/user_login";

        // カスタムURIスキームで起動する場合の設定
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        public static readonly string SamlLoginUrl = "https://win.nomiss.net/rudeus_login";

        public static readonly string ApiEndpointWithCert = "https://manager.nomiss.net/";
        public static readonly string ApiEndpointWithoutCert = "https://win.nomiss.net/";
    }
}
