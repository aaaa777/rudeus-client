﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Rudeus;

namespace SharedLib.Model.Settings
{
    /// <summary>
    /// レジストリに設定を保持するモデル
    /// </summary>
    public class AppSettings : IAppSettings
    {
        // レジストリ：アプリのデフォルトのキー
        private static string DefaultRegistryKey = Constants.DefaultRegistryKey;

        // レジストリ：アプリのルート
        private string RegistryDir = Constants.RegistryDir;
        public string RegistryKey { get; }

        public Func<string, string, string> GetFunc { get; set; }
        public Func<string, string, bool> SetFunc { get; set; }

        private RegistryKey? RegKey;

        private static RegistryKey CreateRegKey(string keyName)
        {
            return Registry.LocalMachine.CreateSubKey(keyName);// ?? new Exception("key creation failed");
        }

        public void DeleteAll()
        {
            Registry.LocalMachine.DeleteSubKeyTree($"{RegistryDir}\\{RegistryKey}");
            RegKey = CreateRegKey($"{RegistryDir}\\{RegistryKey}");
        }

        // DI用コンストラクタ
        public AppSettings(string registryKey, Func<string, string, string>? getFunc = null, Func<string, string, bool>? setFunc = null)
        {
            RegistryKey = registryKey;
            GetFunc = getFunc ?? Get;
            SetFunc = setFunc ?? Set;

            // DI用メソッドが指定されていない場合はレジストリオブジェクトを作成(通常動作)
            if (getFunc == null || setFunc == null)
            {
                RegKey = CreateRegKey($"{RegistryDir}\\{RegistryKey}");
            }
        }

        public string Get(string key, string defaultValue = "")
        {
            try
            {
                var value = RegKey.GetValue(key) ?? new Exception("getting val from registry failed");
                return (string)value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public bool Set(string key, string value)
        {
            RegKey.SetValue(key, value);
            return true;
        }



        // 最初に登録されたHostname
        public static string FirstHostnameKey = "FirstHostname";

        public string FirstHostnameP
        {
            set { SetFunc(FirstHostnameKey, value); }
            get { return GetFunc(FirstHostnameKey, ""); }
        }

        // 最後に起動したときのHostname
        public static string HostnameKey = "DeviceHostname";

        public string HostnameP
        {
            set { SetFunc(HostnameKey, value); }
            get { return GetFunc(HostnameKey, ""); }
        }

        // 一意のデバイスID
        public static string DeviceIdKey = "DeviceId";

        public string DeviceIdP
        {
            set { SetFunc(DeviceIdKey, value); }
            get { return GetFunc(DeviceIdKey, ""); }
        }

        // ログインユーザ(7桁の学籍番号)
        public static string UsernameKey = "DeviceUsername";

        public string UsernameP
        {
            set { SetFunc(UsernameKey, value); }
            get { return GetFunc(UsernameKey, ""); }
        }

        // リクエスト時のアクセストークン
        public static string AccessTokenKey = "AccessToken";

        public string AccessTokenP
        {
            set { SetFunc(AccessTokenKey, value); }
            get { return GetFunc(AccessTokenKey, ""); }
        }

        // 最終チェックのWindowsバージョン

        // デフォルトのレジストリキーでのみ保存可能ここまで



        // ここからデフォルト以外のレジストリ―キーでも保存可能


        // アップデートのチャンネルを指定
        public static string UpdateChannelKey = "UpdatingChannel";

        public string UpdatingChannelP
        {
            set { SetFunc(UpdateChannelKey, value); }
            get { return GetFunc(UpdateChannelKey, ""); }
        }


        // チャンネルの判定メソッド

        // 各チャンネルについて説明
        // Stable   安定版(デフォルト) 下記以外のキーだった場合
        // Beta     Stableの少し先のバージョンを試す事ができる
        // Delelop  Visual StudioのDebugビルドで実行できる設定
        // Test     (Developのみ)レジストリで値を変更可能な設定

        public bool IsBetaChannelP() { return UpdatingChannelP == "beta"; }
        public bool IsTestChannelP()
        {
#if (DEBUG)
            return UpdatingChannelP == "test";
#else
            return false;
#endif
        }
        public bool IsDevelopChannelP() { return UpdatingChannelP == "develop"; }
        public bool IsStableChannelP() { return !(IsTestChannelP() || IsDevelopChannelP() || IsBetaChannelP()); }

        public void SetBetaChannelP() { UpdatingChannelP = "beta"; }
        public void SetTestChannelP()
        {
#if(DEBUG)
            UpdatingChannelP = "test";
#endif
        }
        public void SetDevelopChannelP() { UpdatingChannelP = "develop"; }
        public void SetStableChannelP() { UpdatingChannelP = "stable"; }


        // ダウンロード失敗やバージョンにバグがあった場合のフォールバック設定

        // latest   ダウンロードした最新バージョン
        // last     最後に実行できた正常なバージョン
        public static string LatestVersionStatusKey = "LatestVersionStatus";

        public string LatestVersionStatusP
        {
            set { SetFunc(LatestVersionStatusKey, value); }
            get { return GetFunc(LatestVersionStatusKey, ""); }
        }


        public bool IsLatestVersionStatusOkP() { return LatestVersionStatusP == "ok"; }
        public bool IsLatestVersionStatusDownloadedP() { return LatestVersionStatusP == "downloaded"; }
        public bool IsLatestVersionStatusUnlaunchableP() { return LatestVersionStatusP == "unlaunchable"; }

        public void SetLatestVersionStatusOkP() { LatestVersionStatusP = "ok"; }
        public void SetLatestVersionStatusDownloadedP() { LatestVersionStatusP = "downloaded"; }
        public void SetLatestVersionStatusUnlaunchableP() { LatestVersionStatusP = "unlaunchable"; }

        // アップデート対象にするディレクトリ等の設定

        private static string LastVersionDirPathKey = "LastVersionDirPath";


        public string LastVersionDirPathP
        {
            set { SetFunc(LastVersionDirPathKey, value); }
            get
            {
#if (DEVELOPMENT)
                string dir;
                if (_regKey.Name == CreateRegKey($"{RegistryDir}\\Bg").Name)
                {
                    dir = "RudeusBg";
                }
                else
                {
                    dir = "RudeusBgForm";
                }
                return $"{Environment.CurrentDirectory}\\..\\..\\..\\..\\{dir}\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
#else
                return GetFunc(LastVersionDirPathKey, "");
#endif
            }
        }

        private static string LatestVersionDirPathKey = "LatestVersionDirPath";

        public string LatestVersionDirPathP
        {
            set { SetFunc(LatestVersionDirPathKey, value); }
            get
            {
#if (DEVELOPMENT)
                string dir;
                if (_regKey.Name == CreateRegKey($"{RegistryDir}\\Bg").Name)
                {
                    dir = "RudeusBg";
                }
                else
                {
                    dir = "RudeusBgForm";
                }
                return $"{Environment.CurrentDirectory}\\..\\..\\..\\..\\{dir}\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
#else
                return GetFunc(LatestVersionDirPathKey, "");
#endif
            }
        }

        private static string LastVersionExeNameKey = "LastVersionExeName";

        public string LastVersionExeNameP
        {
            set { SetFunc(LastVersionExeNameKey, value); }
            get { return GetFunc(LastVersionExeNameKey, ""); }
        }

        private static string LatestVersionExeNameKey = "LatestVersionExeName";

        public string LatestVersionExeNameP
        {
            set { SetFunc(LatestVersionExeNameKey, value); }
            get { return GetFunc(LatestVersionExeNameKey, ""); }
        }

        private static string LastDirNameKey = "LastDirname";

        public string LastDirNameP
        {
            set { SetFunc(LastDirNameKey, value); }
            get { return GetFunc(LastDirNameKey, ""); }
        }

        private static string LatestDirNameKey = "LatestDirname";

        public string LatestDirNameP
        {
            set { SetFunc(LatestDirNameKey, value); }
            get { return GetFunc(LatestDirNameKey, ""); }
        }

        // exeを実行できる絶対パス
        public string LastVersionExePathP 
        {
            get { return $"{LastVersionDirPathP}\\{LastVersionExeNameP}"; } 
        }
        public string LatestVersionExePathP 
        {
            get { return $"{LatestVersionDirPathP}\\{LatestVersionExeNameP}"; } 
        }


        // lastに存在するバージョンについて
        public static string CurrentVersionKey = "CurrentVersion";

        public string CurrentVersionP
        {
            set { SetFunc(CurrentVersionKey, value); }
            get { return GetFunc(CurrentVersionKey, ""); }
        }

        public string NetworkIFListKey = "NetworkIFListP";
        public string NetworkIFList
        {
            set { SetFunc(NetworkIFListKey, value); }
            get { return GetFunc(NetworkIFListKey, ""); }
        }

        // for debugging

        //public static string 
    }
}
