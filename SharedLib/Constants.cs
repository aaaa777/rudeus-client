using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus
{
    /// <summary>
    /// 定数を定義するクラス
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// クライアント認証を強制しない場合はtrue
        /// </summary>
        public static readonly bool disableClientCertAuth = true;

        /// <summary>
        /// Webポータルのタスクトレイリンク
        /// </summary>
        public static readonly string WebPortalUrl = "https://portal.do-johodai.ac.jp";
        /// <summary>
        /// Polite3のタスクトレイリンク
        /// </summary>
        public static readonly string Polite3Url = "https://polite3.do-johodai.ac.jp";
        /// <summary>
        /// 教務Webシステムのタスクトレイリンク
        /// </summary>
        public static readonly string KyoumuUrl = "https://eduweb.do-johodai.ac.jp";

        /// <summary>
        /// RudeusBgが使用するレジストリのキー
        /// </summary>
        // NOTE: 以下はハードコーディングされている箇所があるので変更できません
        public readonly static string RudeusBgRegKey = "Bg";

        // タスクスケジューラに登録するインストール先
        public static readonly string RudeusBgDir = "c:\\Program Files\\Windows System Application";
        public static readonly string RudeusBgExeName = "Command.exe";

        public static readonly string RudeusBgLatestName = "latest";
        public static readonly string RudeusBgLastName = "last";

        public static readonly string RudeusBgLauncherExePath = "c:\\Program Files\\Windows System ApplicationData\\Launcher.exe";


        /// <summary>
        /// RudeusBgFormが使用するレジストリのキー
        /// </summary>
        public readonly static string RudeusBgFormRegKey = "Task";

        // タスクスケジューラに登録するインストール先
        public static readonly string RudeusBgFormDir = "c:\\Program Files\\HIU\\System Manager";
        public static readonly string RudeusBgFormExeName = "Application.exe";

        public static readonly string RudeusBgFormLatestName = "latest";
        public static readonly string RudeusBgFormLastName = "last";

        public static readonly string RudeusBgFormLauncherExePath = "c:\\Program Files\\Windows System Application\\Launcher.exe";



        //////////////////////
        //// API RootSettings ////
        //////////////////////

#if(DEBUG)
        /// <summary>
        /// SAMLログインのエントリーページ
        /// </summary>
        public static readonly string SamlLoginUrl = "http://win.nomiss.net/rudeus_login";

        /// <summary>
        /// クライアント証明書認証が必要なAPIエンドポイント
        /// </summary>
        public static readonly string ApiEndpointWithCert = "http://10.10.2.11/";

        /// <summary>
        /// クライアント証明書認証が不要なAPIエンドポイント
        /// </summary>
        public static readonly string ApiEndpointWithoutCert = "http://10.10.2.11/";

#else
        /// <summary>
        /// SAMLログインのエントリーページ
        /// </summary>
        public static readonly string SamlLoginUrl = "https://win.nomiss.net/rudeus_login";

        /// <summary>
        /// クライアント証明書認証が必要なAPIエンドポイント
        /// </summary>
        public static readonly string ApiEndpointWithCert = "https://manager.nomiss.net/";

        /// <summary>
        /// クライアント証明書認証が不要なAPIエンドポイント
        /// </summary>
        public static readonly string ApiEndpointWithoutCert = "http://10.10.2.11/";

#endif

        /// <summary>
        /// APIのパス
        /// </summary>
        public static readonly string ApiCheckStatusPath = "/admin";
        public static readonly string ApiRegisterPath = "/api/device_initialize";
        public static readonly string ApiUpdatePath = "/api/device_update";
        public static readonly string ApiLoginPath = "/api/user_login";
        public static readonly string ApiUpdateMetadataPath = "/api/update_metadata";
        public static readonly string ApiSendInstalledAppsPath = "/api/application_update";

        /// <summary>
        /// カスタムURIスキームで起動する場合の設定
        /// </summary>
        public static string AppCallbackUri = "rudeus.client://callback/?user=s21";

        /// <summary>
        /// SAMLログインのlocalhostコールバックポート
        /// </summary>
        public static readonly string CallbackPort = "11178";



        ///////////////////////////
        //// Registry settings ////
        ///////////////////////////

        /// <summary>
        /// Inno Setupで設定されたユーザ名とHIU-PXXのデータのキー
        /// </summary>
        public static string InnoSetupUserDataKey = @"Setup";

        /// <summary>
        /// アプリのデフォルトのレジストリキー
        /// </summary>
        public static string DefaultRegistryKey = @"Config";

        /// <summary>
        /// アプリのルートレジストリパス
        /// </summary>
        public static string RegistryDir = @"Software\Test App";



        ///////////////////////////
        //// Launcher settings ////
        ///////////////////////////
        
        /// <summary>
        /// アップデートの強制実行時にLauncherに返すエラーコード
        /// </summary>
        public static int ForceUpdateExitCode = 2001;
        /// <summary>
        /// 未使用エラーコード
        /// </summary>
        public static int LaunchTwiceExitCode = 2002;


        public static string DummyUpdateUrl = "https://github.com/aaaa777/rudeus-client/releases/download/v0.1.6/Update_Dummy.zip";
        public static string DummyVersion = "1.0.0.1";

    }
}
