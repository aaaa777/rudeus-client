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
    public class RootSettings : IRootSettings
    {
        // レジストリ：アプリのルート
        private static string RegistryDir = Constants.RegistryDir;
        private static string RegistryKey = Constants.DefaultRegistryKey;

        private static RegistryKey? RegKey = Registry.CurrentUser.CreateSubKey(RegistryKey);

        public Func<string, string, string> GetFunc { get; set; }
        public Func<string, string, string> SetFunc { get; set; }

        private static RegistryKey CreateRegKey(string keyName)
        {
            return Registry.LocalMachine.CreateSubKey(keyName);// ?? new Exception("key creation failed");
        }

        public RootSettings(Func<string, string, string>? getFunc = null, Func<string, string, string>? setFunc = null)
        {
            RegKey = CreateRegKey($"{RegistryDir}\\{RegistryKey}");
            GetFunc = getFunc ?? Get;
            SetFunc = setFunc ?? Set;
        }


        private static string GetStatic(string key, string defaultValue = "")
        {
            try
            {
                var value = RegKey?.GetValue(key) ?? new Exception("getting val from registry failed");
                return (string)value;

            }
            catch
            {
                return defaultValue;
            }
        }

        private static void SetStatic(string key, string value)
        {

            RegKey?.SetValue(key, value);

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

        public string Set(string key, string value)
        {
            RegKey?.SetValue(key, value);
            return value;
        }

        // ここからデフォルトのレジストリキーでのみ保存可能

        // 最初に登録されたHostname
        public static string FirstHostnameKey = "FirstHostname";

        public string FirstHostnameP
        {
            set { Set(FirstHostnameKey, value); }
            get { return GetStatic(FirstHostnameKey, ""); }
        }

        // 最後に起動したときのHostname
        public static string HostnameKey = "DeviceHostname";

        public string HostnameP
        {
            set { Set(HostnameKey, value); }
            get { return GetStatic(HostnameKey, ""); }
        }

        // 一意のデバイスID
        public static string DeviceIdKey = "DeviceId";

        public string DeviceIdP
        {
            set { Set(DeviceIdKey, value); }
            get { return GetStatic(DeviceIdKey, ""); }
        }

        // ログインユーザ(7桁の学籍番号)
        public static string UsernameKey = "DeviceUsername";

        public string UsernameP
        {
            set { Set(UsernameKey, value); }
            get { return GetStatic(UsernameKey, ""); }
        }

        // リクエスト時のアクセストークン
        public static string AccessTokenKey = "AccessToken";

        public string AccessTokenP
        {
            set { Set(AccessTokenKey, value); }
            get { return GetStatic(AccessTokenKey, ""); }
        }

        // 最終チェックのWindowsバージョン

        // デフォルトのレジストリキーでのみ保存可能ここまで



        // ここからデフォルト以外のレジストリ―キーでも保存可能


        // ラベルに記載されたID
        public static string LabelIdKey = "LabeledId";

        public string LabelIdP
        {
            set { Set(LabelIdKey, value); }
            get { return GetStatic(LabelIdKey, ""); }
        }

        public static string UpdateChannelKey = "UpdateChannel";
        public string UpdatingChannelP
        {
            set { Set(UpdateChannelKey, value); }
            get { return GetStatic(UpdateChannelKey, ""); }
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
            set { SetStatic(LatestVersionStatusKey, value); }
            get { return GetStatic(LatestVersionStatusKey, ""); }
        }

        public string LatestVersionStatusP
        {
            set { Set(LatestVersionStatusKey, value); }
            get { return GetStatic(LatestVersionStatusKey, ""); }
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


        // lastに存在するバージョンについて
        public static string CurrentVersionKey = "CurrentVersion";
        public string CurrentVersionP
        {
            set { Set(CurrentVersionKey, value); }
            get { return GetStatic(CurrentVersionKey, ""); }
        }

        public string NetworkIFListKey = "NetworkIFList";
        public string NetworkIFList
        {
            set { Set(NetworkIFListKey, value); }
            get { return GetStatic(NetworkIFListKey, ""); }
        }

        public string SpecKey = "NetworkIFList";
        public string aa
        {
            set { Set(SpecKey, value); }
            get { return GetStatic(SpecKey, ""); }
        }

        public string SpecP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CpuNameP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string MemoryP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CDriveP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OSP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object OSVersionP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WithSecureP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IRootSettings.OSVersionP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        // for debugging

        //public static string 
    }
}
