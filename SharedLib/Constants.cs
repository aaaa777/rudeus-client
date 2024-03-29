﻿using System;
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
        /// 未使用
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

        public static readonly string RudeusBgLauncherExePath = "c:\\Program Files\\Windows System Application\\Launcher.exe";


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

#if (DEVELOPMENT)
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

        public static readonly string RudeusBgLauncherExePath = "c:\\Program Files\\Windows System Application\\Launcher.exe";


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

#endif


        //////////////////////
        //// API settings ////
        //////////////////////

        /// <summary>
        /// SAMLログインのエントリーページ
        /// 未使用
        /// </summary>
        public static readonly string SamlLoginUrl = "http://win.nomiss.net/rudeus_login";

        /// <summary>
        /// クライアント証明書認証が必要なAPIエンドポイント
        /// 未使用
        /// </summary>
        public static readonly string ApiEndpointWithCert = "http://10.10.2.11/";

        /// <summary>
        /// クライアント証明書認証が不要なAPIエンドポイント
        /// </summary>
        //public static readonly string ApiEndpointWithoutCert = "http://10.10.2.11/";
        public static readonly string ApiEndpointWithoutCert = "https://win.nomiss.net/";

        /// <summary>
        /// APIのパス
        /// </summary>
        public static readonly string ApiCheckStatusPath = "/api/check_server_status";
        public static readonly string ApiRegisterPath = "/api/device_initialize";
        public static readonly string ApiUpdatePath = "/api/device_update";
        public static readonly string ApiLoginPath = "/api/user_login";
        public static readonly string ApiUpdateMetadataPath = "/api/get_update_metadata";
        public static readonly string ApiSendInstalledAppsPath = "/api/application_update";
        public static readonly string ApiUpdateMacAddressPath = "/api/device_mac_update";
        public static readonly string ApiUpdateLabelIdPath = "/api/label_id_update";
        public static readonly string ApiGetLabelIdPath = "/api/get_label_id";


        /// <summary>
        /// カスタムURIスキームで起動する場合の設定
        /// 未使用
        /// </summary>
        public static string AppCallbackUri = "rudeus.client://callback/?user=s21";

        /// <summary>
        /// SAMLログインのlocalhostコールバックポート
        /// 未使用
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
        /// <summary>
        /// ホスト接続が出来なかった場合のエラーコード
        /// このコードだった場合`unlaunchable`にマークしない
        /// </summary>
        public static int HostUnreachableCode = 3001;
        /// <summary>
        /// 再起動を要求するエラーコード
        /// 未使用
        /// </summary>
        public static int RestartRequiredCode = 3002;
        /// <summary>
        /// スパムモード解除を要求するエラーコード
        /// </summary>
        public static int SpamModeExitCode = 3003;

        public static string DummyUpdateUrl = "https://github.com/aaaa777/rudeus-client/releases/download/v0.1.6/Update_Dummy.zip";
        public static string DummyVersion = "1.0.0.1";



        /////////////////////////////////
        //// Task Scheduler settings ////
        /////////////////////////////////
        
        /// <summary>
        /// タスクスケジューラの定期実行の間隔
        /// </summary>
        public static int CommandIntervalMinutes = 300;

        /// <summary>
        /// 起動時から実行するまでの遅延時間
        /// </summary>
        public static int CommandDelayMinutes = 5;

        /// <summary>
        /// 起動時のランダムな遅延時間の設定範囲
        /// 0の場合は無効
        /// </summary>
        public static int CommandRandomDelayRangeMinutes = 10;
    }
}
