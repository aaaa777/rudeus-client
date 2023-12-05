using System;
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
        private static string RegistryDir = Constants.RegistryDir;
        private static string RegistryKey = DefaultRegistryKey;

        public Func<string, string, string> GetFunc { get; set; }
        public Func<string, string, string> SetFunc { get; set; }

        private static string RegistryPath { get { return $"{RegistryDir}\\{RegistryKey}"; } }

        private static RegistryKey? RegKey = Registry.CurrentUser.CreateSubKey(RegistryKey);

        private RegistryKey _regKey;

        private static RegistryKey CreateRegKey(string keyName)
        {
            return Registry.LocalMachine.CreateSubKey(keyName);// ?? new Exception("key creation failed");
        }

        public AppSettings()
        {
            _regKey = CreateRegKey($"{RegistryDir}\\{DefaultRegistryKey}");
        }

        public AppSettings(string registryKey)
        {
            _regKey = CreateRegKey($"{RegistryDir}\\{registryKey}");
        }

        public static void UpdateRegistryKey(string? registryKey = null)
        {
            if (registryKey != null)
            {
                RegistryKey = registryKey;
            }
            else
            {
                // nullの場合デフォルト値にリセット
                RegistryKey = DefaultRegistryKey;
            }

            RegKey = CreateRegKey(RegistryPath);
        }

        public string Get(string key, string defaultValue = "")
        {
            try
            {
                var value = _regKey.GetValue(key) ?? new Exception("getting val from registry failed");
                return (string)value;
            }
            catch
            {
                return defaultValue;
            }
        }

        public string Set(string key, string value)
        {
            _regKey.SetValue(key, value);
            return value;
        }

        private static string GetStatic(string key, string defaultValue = "", bool isDefaultKey = true, RegistryKey? regKey = null)
        {
            if (isDefaultKey)
            {
                UpdateRegistryKey();
            }

            try
            {
                if (regKey != null)
                {
                    var value = regKey.GetValue(key) ?? new Exception("getting val from registry failed");
                    return (string)value;
                }
                else
                {
                    var value = RegKey?.GetValue(key) ?? new Exception("getting val from registry failed");
                    return (string)value;
                }

            }
            catch
            {
                return defaultValue;
            }
        }

        private static void SetStatic(string key, string value, bool isDefault = true, RegistryKey? regKey = null)
        {
            if (isDefault)
            {
                UpdateRegistryKey();
            }

            if (regKey != null)
            {
                regKey.SetValue(key, value);
            }
            else
            {
                RegKey?.SetValue(key, value);
            }
        }

        // Inno Setup UserData
        public static string InitedUsername
        {
            get
            {
                return (string)(CreateRegKey(Constants.InnoSetupUserDataKey).GetValue("Username") ?? new Exception("Setup Username missing"));
            }
        }

        // Pxx-xxx
        public static string InitedLabelId
        {
            get
            {
                return (string)(CreateRegKey(Constants.InnoSetupUserDataKey).GetValue("LabelId") ?? new Exception("Setup LabelId missing"));
            }
        }

        // ここからデフォルトのレジストリキーでのみ保存可能

        // 最初に登録されたHostname
        public static string FirstHostnameKey = "FirstHostname";

        public string FirstHostnameP
        {
            set { Set(FirstHostnameKey, value); }
            get { return Get(FirstHostnameKey, ""); }
        }

        // 最後に起動したときのHostname
        public static string HostnameKey = "DeviceHostname";

        public string HostnameP
        {
            set { Set(HostnameKey, value); }
            get { return Get(HostnameKey, ""); }
        }

        // 一意のデバイスID
        public static string DeviceIdKey = "DeviceId";
        public static string DeviceId
        {
            set { SetStatic(DeviceIdKey, value); }
            get { return GetStatic(DeviceIdKey); }
        }

        public string DeviceIdP
        {
            set { Set(DeviceIdKey, value); }
            get { return Get(DeviceIdKey, ""); }
        }

        // ログインユーザ(7桁の学籍番号)
        public static string UsernameKey = "DeviceUsername";
        public static string Username
        {
            set { SetStatic(UsernameKey, value); }
            get { return GetStatic(UsernameKey); }
        }

        public string UsernameP
        {
            set { Set(UsernameKey, value); }
            get { return Get(UsernameKey, ""); }
        }

        // リクエスト時のアクセストークン
        public static string AccessTokenKey = "AccessToken";
        public static string AccessToken
        {
            set { SetStatic(AccessTokenKey, value); }
            get { return GetStatic(AccessTokenKey); }
        }

        public string AccessTokenP
        {
            set { Set(AccessTokenKey, value); }
            get { return Get(AccessTokenKey, ""); }
        }

        // 最終チェックのWindowsバージョン

        // デフォルトのレジストリキーでのみ保存可能ここまで



        // ここからデフォルト以外のレジストリ―キーでも保存可能


        // ラベルに記載されたID
        public static string LabelIdKey = "LabeledId";
        public static string LabelId
        {
            set { SetStatic(LabelIdKey, value); }
            get { return GetStatic(LabelIdKey); }
        }

        public string LabelIdP
        {
            set { Set(LabelIdKey, value); }
            get { return Get(LabelIdKey, ""); }
        }

        // アップデートのチャンネルを指定
        public static string UpdateChannelKey = "UpdatingChannel";

        public static string UpdatingChannel
        {
            set { SetStatic(UpdateChannelKey, value, false); }
            get { return GetStatic(UpdateChannelKey, "", false); }
        }

        public string UpdatingChannelP
        {
            set { Set(UpdateChannelKey, value); }
            get { return Get(UpdateChannelKey, ""); }
        }


        // チャンネルの判定メソッド

        // 各チャンネルについて説明
        // Stable   安定版(デフォルト) 下記以外のキーだった場合
        // Beta     Stableの少し先のバージョンを試す事ができる
        // Delelop  Visual StudioのDebugビルドで実行できる設定
        // Test     (Developのみ)レジストリで値を変更可能な設定
        public static bool IsBetaChannel() { return UpdatingChannel == "beta"; }
        public static bool IsTestChannel()
        {
#if(DEBUG)
            return UpdatingChannel == "test";
#else
            return false;
#endif
        }

        public static void SetBetaChannel() { UpdatingChannel = "beta"; }
        public static void SetTestChannel()
        {
#if(DEBUG)
            UpdatingChannel = "test";
#endif
        }

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

        public static string LatestVersionStatus
        {
            set { SetStatic(LatestVersionStatusKey, value, false); }
            get { return GetStatic(LatestVersionStatusKey, "", false); }
        }

        public string LatestVersionStatusP
        {
            set { Set(LatestVersionStatusKey, value); }
            get { return Get(LatestVersionStatusKey, ""); }
        }

        public static bool IsLatestVersionStatusOk() { return LatestVersionStatus == "ok"; }
        public static bool IsLatestVersionStatusDownloaded() { return LatestVersionStatus == "downloaded"; }
        public static bool IsLatestVersionStatusUnlaunchable() { return LatestVersionStatus == "unlaunchable"; }

        public static void SetLatestVersionStatusOk() { LatestVersionStatus = "ok"; }
        public static void SetLatestVersionStatusDownloaded() { LatestVersionStatus = "downloaded"; }
        public static void SetLatestVersionStatusUnlaunchable() { LatestVersionStatus = "unlaunchable"; }

        public bool IsLatestVersionStatusOkP() { return LatestVersionStatus == "ok"; }
        public bool IsLatestVersionStatusDownloadedP() { return LatestVersionStatus == "downloaded"; }
        public bool IsLatestVersionStatusUnlaunchableP() { return LatestVersionStatus == "unlaunchable"; }

        public void SetLatestVersionStatusOkP() { LatestVersionStatus = "ok"; }
        public void SetLatestVersionStatusDownloadedP() { LatestVersionStatus = "downloaded"; }
        public void SetLatestVersionStatusUnlaunchableP() { LatestVersionStatus = "unlaunchable"; }

        // アップデート対象にするディレクトリ等の設定

        private static string LastVersionDirPathKey = "LastVersionDirPath";

        public static string LastVersionDirPath
        {
            set { SetStatic(LastVersionDirPathKey, value, false); }
            get
            {
#if (DEVELOPMENT)
                string dir;
                if (RegistryKey == "Bg")
                {
                    dir = "RudeusBg";
                }
                else
                {
                    dir = "RudeusBgForm";
                }
                return $"{Environment.CurrentDirectory}\\..\\..\\..\\..\\{dir}\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
#else
                return GetStatic(LastVersionDirPathKey, "", false);
#endif
            }
        }

        public string LastVersionDirPathP
        {
            set { SetStatic(LastVersionDirPathKey, value, false, _regKey); }
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
                return GetStatic(LastVersionDirPathKey, "", false, _regKey);
#endif
            }
        }

        private static string LatestVersionDirPathKey = "LatestVersionDirPath";

        public static string LatestVersionDirPath
        {
            set { SetStatic(LatestVersionDirPathKey, value, false); }
            get
            {
#if (DEVELOPMENT)
                string dir;
                if (RegistryKey == "Bg")
                {
                    dir = "RudeusBg";
                }
                else
                {
                    dir = "RudeusBgForm";
                }
                return $"{Environment.CurrentDirectory}\\..\\..\\..\\..\\{dir}\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
#else
                return GetStatic(LatestVersionDirPathKey, "", false);
#endif
            }
        }
        public string LatestVersionDirPathP
        {
            set { SetStatic(LatestVersionDirPathKey, value, false, _regKey); }
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
                return GetStatic(LatestVersionDirPathKey, "", false, _regKey);
#endif
            }
        }

        private static string LastVersionExeNameKey = "LastVersionExeName";

        public static string LastVersionExeName
        {
            set { SetStatic(LastVersionExeNameKey, value, false); }
            get { return GetStatic(LastVersionExeNameKey, "", false); }
        }

        public string LastVersionExeNameP
        {
            set { SetStatic(LastVersionExeNameKey, value, false, _regKey); }
            get { return GetStatic(LastVersionExeNameKey, "", false, _regKey); }
        }

        private static string LatestVersionExeNameKey = "LatestVersionExeName";

        public static string LatestVersionExeName
        {
            set { SetStatic(LatestVersionExeNameKey, value, false); }
            get { return GetStatic(LatestVersionExeNameKey, "", false); }
        }

        public string LatestVersionExeNameP
        {
            set { SetStatic(LatestVersionExeNameKey, value, false, _regKey); }
            get { return GetStatic(LatestVersionExeNameKey, "", false, _regKey); }
        }

        private static string LastDirNameKey = "LastDirname";

        public static string LastDirName
        {
            set { SetStatic(LastDirNameKey, value, false); }
            get { return GetStatic(LastDirNameKey, "", false); }
        }

        public string LastDirNameP
        {
            set { SetStatic(LastDirNameKey, value, false, _regKey); }
            get { return GetStatic(LastDirNameKey, "", false, _regKey); }
        }

        private static string LatestDirNameKey = "LatestDirname";

        public static string LatestDirName
        {
            set { SetStatic(LatestDirNameKey, value, false); }
            get { return GetStatic(LatestDirNameKey, "", false); }
        }

        public string LatestDirNameP
        {
            set { SetStatic(LatestDirNameKey, value, false, _regKey); }
            get { return GetStatic(LatestDirNameKey, "", false, _regKey); }
        }

        // exeを実行できる絶対パス
        public static string LastVersionExePath { get { return $"{LastVersionDirPath}\\{LastVersionExeName}"; } }
        public static string LatestVersionExePath { get { return $"{LatestVersionDirPath}\\{LatestVersionExeName}"; } }
        public string LastVersionExePathP { get { return $"{LastVersionDirPathP}\\{LastVersionExeNameP}"; } }
        public string LatestVersionExePathP { get { return $"{LatestVersionDirPathP}\\{LatestVersionExeNameP}"; } }


        // lastに存在するバージョンについて
        public static string CurrentVersionKey = "CurrentVersion";

        public static string CurrentVersion
        {
            set { SetStatic(CurrentVersionKey, value, false); }
            get { return GetStatic(CurrentVersionKey, "", false); }
        }

        public string CurrentVersionP
        {
            set { SetStatic(CurrentVersionKey, value, false, _regKey); }
            get { return GetStatic(CurrentVersionKey, "", false, _regKey); }
        }

        public string NetworkIFListKey = "NetworkIFList";
        public string NetworkIFList
        {
            set { SetStatic(NetworkIFListKey, value, false, _regKey); }
            get { return GetStatic(NetworkIFListKey, "", false, _regKey); }
        }

        string IAppSettings.LastVersionExePathP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IAppSettings.LatestVersionExePathP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        // for debugging

        //public static string 
    }
}
