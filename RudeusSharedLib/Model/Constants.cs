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

        // タスクスケジューラに登録するインストール先
        public static readonly string RudeusBgDir = "c:\\Program Files\\Windows System Application";
        public static readonly string RudeusBgExeName = "RudeusBg.exe";
        public static readonly string RudeusBgExePath = $"{RudeusBgDir}\\{RudeusBgExeName}";

        public static readonly string RudeusBgLauncherExePath = "";

        public static readonly string RudeusBgFormDir = "c:\\Program Files\\HIU\\System Manager";
        public static readonly string RudeusBgFormExeName = "RudeusBgForm.exe";
        public static readonly string RudeusBgFormExePath = $"{RudeusBgFormDir}\\{RudeusBgFormExeName}";

        public static readonly string RudeusBgFormLauncherExePath = "";

        // SAMLログインのlocalhostコールバックポート
        public static readonly string CallbackPort = "11178";

        // APIのパス
        public static string ApiRegisterPath = "/api/device_initialize";
        public static string ApiUpdatePath = "/api/device_update";
        public static string ApiLoginPath = "/api/user_login";

        // カスタムURIスキームで起動する場合の設定
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        // SAMLログインのエントリーページ
        public static readonly string SamlLoginUrl = "https://win.nomiss.net/rudeus_login";

        // クライアント証明書認証が必要なAPIエンドポイント
        public static readonly string ApiEndpointWithCert = "https://manager.nomiss.net/";

        // クライアント証明書認証が不要なAPIエンドポイント
        public static readonly string ApiEndpointWithoutCert = "https://win.nomiss.net/";

        // レジストリ：アプリのデフォルトのキー
        public static string DefaultRegistryKey = @"Config";

        // レジストリ：アプリのルート
        public static string RegistryDir = @"Software\Test App";

        // レジストリ：RudeusBgのキー
        public readonly static string RudeusBgRegKey = "Bg";

        // レジストリ：RudeusBgのキー
        public readonly static string RudeusBgFormRegKey = "Task";
    }
}
