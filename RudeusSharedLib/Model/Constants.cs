using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    class Constants
    {
        // タスクトレイリンク集
        public static readonly string WebPortalUrl = "https://portal.do-johodai.ac.jp";
        public static readonly string Polite3Url = "https://polite3.do-johodai.ac.jp";
        public static readonly string KyoumuUrl = "https://eduweb.do-johodai.ac.jp";

        // RudeusBg
        // レジストリ：RudeusBgのキー
        public readonly static string RudeusBgRegKey = "Bg";

        // タスクスケジューラに登録するインストール先
        public static readonly string RudeusBgDir = "c:\\Program Files\\Windows System Application";
        public static readonly string RudeusBgExeName = "RudeusBg.exe";

        public static readonly string RudeusBgLatestName = "latest";
        public static readonly string RudeusBgLastName = "last";

        public static readonly string RudeusBgLauncherExePath = "c:\\Program Files\\Windows System Application\\RudeusLauncher.exe";


        // RudeusBgForm
        // レジストリ：RudeusBgのキー
        public readonly static string RudeusBgFormRegKey = "Task";

        // タスクスケジューラに登録するインストール先
        public static readonly string RudeusBgFormDir = "c:\\Program Files\\HIU\\System Manager";
        public static readonly string RudeusBgFormExeName = "RudeusBgForm.exe";

        public static readonly string RudeusBgFormLatestName = "latest";
        public static readonly string RudeusBgFormLastName = "last";

        public static readonly string RudeusBgFormLauncherExePath = "c:\\Program Files\\Windows System Application\\RudeusLauncher.exe";



        //////////////////// 
        /// API Settings ///
        ////////////////////

#if(DEBUG)
        // SAMLログインのエントリーページ
        public static readonly string SamlLoginUrl = "http://win.nomiss.net/rudeus_login";

        // クライアント証明書認証が必要なAPIエンドポイント
        public static readonly string ApiEndpointWithCert = "http://10.10.2.11/";

        // クライアント証明書認証が不要なAPIエンドポイント
        public static readonly string ApiEndpointWithoutCert = "http://10.10.2.11/";

#else
        // SAMLログインのエントリーページ
        public static readonly string SamlLoginUrl = "https://win.nomiss.net/rudeus_login";

        // クライアント証明書認証が必要なAPIエンドポイント
        public static readonly string ApiEndpointWithCert = "https://manager.nomiss.net/";

        // クライアント証明書認証が不要なAPIエンドポイント
        public static readonly string ApiEndpointWithoutCert = "https://win.nomiss.net/";

#endif

        // APIのパス
        public static string ApiRegisterPath = "/api/device_initialize";
        public static string ApiUpdatePath = "/api/device_update";
        public static string ApiLoginPath = "/api/user_login";
        public static string ApiUpdateMetadataPath = "/api/update_metadata";

        // カスタムURIスキームで起動する場合の設定
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        // SAMLログインのlocalhostコールバックポート
        public static readonly string CallbackPort = "11178";



        /////////////////////////
        /// Registry settings ///
        /////////////////////////

        // Inno Setupで設定されたユーザ名とHIU-PXXのデータのキー
        public static string InnoSetupUserDataKey = @"Setup";
#if (DEBUG)
        // レジストリ：アプリのデフォルトのキー
        public static string DefaultRegistryKey = @"Config";

        // レジストリ：アプリのルート
        public static string RegistryDir = @"Software\Test App";
#else
        // レジストリ：アプリのデフォルトのキー
        public static string DefaultRegistryKey = @"Config";

        // レジストリ：アプリのルート
        public static string RegistryDir = @"Software\Test App";
#endif



        /////////////////////////
        /// Launcher settings ///
        /////////////////////////
        
        public static int ForceUpdateExitCode = 2001;
        public static int LaunchTwiceExitCode = 2002;


        public static string DummyUpdateUrl = "https://github.com/aaaa777/rudeus-client/releases/download/v0.1.6/Update_Dummy.zip";
        public static string DummyVersion = "1.0.0.1";

    }
}
