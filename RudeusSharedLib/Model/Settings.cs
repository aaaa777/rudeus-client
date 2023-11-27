using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Rudeus.Model
{

    internal class Settings : ISettings
    {
        // レジストリ：アプリのデフォルトのキー
        private static string DefaultRegistryKey = Constants.DefaultRegistryKey;

        // レジストリ：アプリのルート
        private static string RegistryDir = Constants.RegistryDir;
        private static string RegistryKey = DefaultRegistryKey;
        private static string RegistryPath { get { return $"{RegistryDir}\\{RegistryKey}"; } }

        private static RegistryKey? RegKey = Registry.CurrentUser.CreateSubKey(RegistryKey);

        private RegistryKey _regKey;

        private static RegistryKey CreateRegKey(string keyName)
        {
            return Registry.LocalMachine.CreateSubKey(keyName);// ?? new Exception("key creation failed");
        }

        public Settings()
        {
            _regKey = CreateRegKey($"{RegistryDir}\\{DefaultRegistryKey}");
        }

        public Settings(string registryKey)
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


        private static string Get(string key, string defaultValue = "", bool isDefaultKey = true, RegistryKey? regKey = null)
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

        private static void Set(string key, string value, bool isDefault = true, RegistryKey? regKey = null)
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
        public static string FirstHostname
        {
            set { Set(FirstHostnameKey, value); }
            get { return Get(FirstHostnameKey); }
        }

        public string FirstHostnameP 
        {
            set { Set(FirstHostnameKey, value, false, _regKey); }
            get { return Get(FirstHostnameKey, "", false, _regKey); }
        }

        // 最後に起動したときのHostname
        public static string HostnameKey = "DeviceHostname";
        public static string Hostname
        {
            set { Set(HostnameKey, value); }
            get { return Get(HostnameKey); }
        }

        // 一意のデバイスID
        public static string DeviceIdKey = "DeviceId";
        public static string DeviceId
        {
            set { Set(DeviceIdKey, value); }
            get { return Get(DeviceIdKey); }
        }

        // ログインユーザ(7桁の学籍番号)
        public static string UsernameKey = "DeviceUsername";
        public static string Username
        {
            set { Set(UsernameKey, value); }
            get { return Get(UsernameKey); }
        }

        // リクエスト時のアクセストークン
        public static string AccessTokenKey = "AccessToken";
        public static string AccessToken
        {
            set { Set(AccessTokenKey, value); }
            get { return Get(AccessTokenKey); }
        }

        // 最終チェックのWindowsバージョン

        // デフォルトのレジストリキーでのみ保存可能ここまで



        // ここからデフォルト以外のレジストリ―キーでも保存可能


        // ラベルに記載されたID
        public static string LabelIdKey = "LabeledId";
        public static string LabelId
        {
            set { Set(LabelIdKey, value); }
            get { return Get(LabelIdKey); }
        }

        public string LabelIdP
        {
            set { Set(LabelIdKey, value, false, _regKey); }
            get { return Get(LabelIdKey, "", false, _regKey); }
        }

        // アップデートのチャンネルを指定
        public static string UpdateChannelKey = "UpdatingChannel";

        public static string UpdatingChannel
        {
            set { Set(UpdateChannelKey, value, false); }
            get { return Get(UpdateChannelKey, "", false); }
        }

        public string UpdatingChannelP
        {
            set { Set(UpdateChannelKey, value, false, _regKey); }
            get { return Get(UpdateChannelKey, "", false, _regKey); }
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
        public static bool IsDevelopChannel() { return UpdatingChannel == "develop"; }
        public static bool IsStableChannel() { return !(IsTestChannel() || IsDevelopChannel() || IsBetaChannel()); }

        public static void SetBetaChannel() { UpdatingChannel = "beta"; }
        public static void SetTestChannel()
        {
#if(DEBUG)
            UpdatingChannel = "test";
#endif
        }
        public static void SetDevelopChannel() { UpdatingChannel = "develop"; }
        public static void SetStableChannel() { UpdatingChannel = "stable"; }

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
            set { Set(LatestVersionStatusKey, value, false); }
            get { return Get(LatestVersionStatusKey, "", false); }
        }

        public string LatestVersionStatusP
        {
            set { Set(LatestVersionStatusKey, value, false, _regKey); }
            get { return Get(LatestVersionStatusKey, "", false, _regKey); }
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
            set { Set(LastVersionDirPathKey, value, false); }
            get { return Get(LastVersionDirPathKey, "", false); }
        }

        public string LastVersionDirPathP
        {
            set { Set(LastVersionDirPathKey, value, false, _regKey); }
            get { return Get(LastVersionDirPathKey, "", false, _regKey); }
        }

        private static string LatestVersionDirPathKey = "LatestVersionDirPath";

        public static string LatestVersionDirPath
        {
            set { Set(LatestVersionDirPathKey, value, false); }
            get { return Get(LatestVersionDirPathKey, "", false); }
        }
        public string LatestVersionDirPathP
        {
            set { Set(LatestVersionDirPathKey, value, false, _regKey); }
            get { return Get(LatestVersionDirPathKey, "", false, _regKey); }
        }

        private static string LastVersionExeNameKey = "LastVersionExeName";

        public static string LastVersionExeName
        {
            set { Set(LastVersionExeNameKey, value, false); }
            get { return Get(LastVersionExeNameKey, "", false); }
        }

        public string LastVersionExeNameP
        {
            set { Set(LastVersionExeNameKey, value, false, _regKey); }
            get { return Get(LastVersionExeNameKey, "", false, _regKey); }
        }

        private static string LatestVersionExeNameKey = "LatestVersionExeName";

        public static string LatestVersionExeName
        {
            set { Set(LatestVersionExeNameKey, value, false); }
            get { return Get(LatestVersionExeNameKey, "", false); }
        }

        public string LatestVersionExeNameP
        {
            set { Set(LatestVersionExeNameKey, value, false, _regKey); }
            get { return Get(LatestVersionExeNameKey, "", false, _regKey); }
        }

        private static string LastDirNameKey = "LastDirname";

        public static string LastDirName
        {
            set { Set(LastDirNameKey, value, false); }
            get { return Get(LastDirNameKey, "", false); }
        }

        public string LastDirNameP
        {
            set { Set(LastDirNameKey, value, false, _regKey); }
            get { return Get(LastDirNameKey, "", false, _regKey); }
        }

        private static string LatestDirNameKey = "LatestDirname";

        public static string LatestDirName
        {
            set { Set(LatestDirNameKey, value, false); }
            get { return Get(LatestDirNameKey, "", false); }
        }

        public string LatestDirNameP
        {
            set { Set(LatestDirNameKey, value, false, _regKey); }
            get { return Get(LatestDirNameKey, "", false, _regKey); }
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
            set { Set(CurrentVersionKey, value, false); }
            get { return Get(CurrentVersionKey, "", false); }
        }

        public string CurrentVersionP
        {
            set { Set(CurrentVersionKey, value, false, _regKey); }
            get { return Get(CurrentVersionKey, "", false, _regKey); }
        }

        public string NetworkIFListKey = "NetworkIFList";
        public string NetworkIFList
        {
            set { Set(NetworkIFListKey, value, false, _regKey); }
            get { return Get(NetworkIFListKey, "", false, _regKey); }
        }
        // for debugging

        //public static string 
    }
}
