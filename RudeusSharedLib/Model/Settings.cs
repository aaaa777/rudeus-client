using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Rudeus.Model
{
    /// <summary>
    /// 設定をレジストリに保管するモデル
    /// レジストリがない場合の変換や暗号化を行う予定
    /// </summary>
    internal class Settings
    {
        // レジストリ：アプリのデフォルトのキー
        private static string DefaultRegistryKey = @"Config";

        // レジストリ：アプリのルート
        private static string RegistryDir = @"Software\Test App";
        private static string RegistryKey = DefaultRegistryKey;
        private static string RegistryPath { get { return $"{RegistryDir}\\{RegistryKey}"; } }

        private static RegistryKey? RegKey = Registry.CurrentUser.CreateSubKey(RegistryKey);


        public static void UpdateRegistryKey(string? registryKey=null)
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

            RegKey = Registry.CurrentUser.CreateSubKey(RegistryPath);
        }


        private static string Get(string key, string defaultValue="", bool isDefault=true)
        {
            if (isDefault)
            {
                UpdateRegistryKey();
            }

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

        private static void Set(string key, string value, bool isDefault=true) 
        {
            if (isDefault)
            {
                UpdateRegistryKey();
            }

            RegKey?.SetValue(key, value); 
        }

        public static string FirstHostnameKey = "FirstHostname";
        public static string FirstHostname
        {
            set { Set(FirstHostnameKey, value); }
            get { return Get(FirstHostnameKey); }
        }

        public static string HostnameKey = "DeviceHostname";
        public static string Hostname
        {
            set { Set(HostnameKey, value); }
            get { return Get(HostnameKey); }
        }

        public static string DeviceIdKey = "DeviceId";
        public static string DeviceId
        {
            set { Set(DeviceIdKey, value); }
            get { return Get(DeviceIdKey); }
        }

        public static string UsernameKey = "DeviceUsername";
        public static string Username
        {
            set { Set(UsernameKey, value); }
            get { return Get(UsernameKey); }
        }

        public static string AccessTokenKey = "AccessToken";
        public static string AccessToken
        {
            set { Set(AccessTokenKey, value); }
            get { return Get(AccessTokenKey); }
        }

        public static string UpdateChannelKey = "UpdatingChannel";

        public static string UpdatingChannel
        {
            set { Set(UpdateChannelKey, value, false); }
            get { return Get(UpdateChannelKey, "", false); }
        }

        public static bool IsStableChannel() { return !(IsTestChannel() || IsDevelopChannel()); }
        public static bool IsTestChannel() { return UpdatingChannel == "test"; }
        public static bool IsDevelopChannel() { return UpdatingChannel == "develop"; }

        public static void SetStableChannel() { UpdatingChannel = "stable"; }
        public static void SetTestChannel() { UpdatingChannel = "test"; }
        public static void SetDevelopChannel() { UpdatingChannel = "develop"; }

        public static string LatestVersionStatusKey = "LatestVersionStatus";

        public static string LatestVersionStatus
        {
            set { Set(LatestVersionStatusKey, value, false); }
            get { return Get(LatestVersionStatusKey, "", false); }
        }

        public static bool IsLatestVersionStatusOk() { return LatestVersionStatus == "ok"; }
        public static bool IsLatestVersionStatusDownloaded() { return LatestVersionStatus == "downloaded"; }
        public static bool IsLatestVersionStatusUnlaunchable() { return LatestVersionStatus == "unlaunchable"; }

        public static void SetLatestVersionStatusOk() { LatestVersionStatus = "ok"; }
        public static void SetLatestVersionStatusDownloaded() { LatestVersionStatus = "downloaded"; }
        public static void SetLatestVersionStatusUnlaunchable() { LatestVersionStatus = "unlaunchable"; }

        public static string LastVersionExePathKey = "LastVersionExePath";

        public static string LastVersionExePath
        {
            set { Set(LastVersionExePathKey, value, false); }
            get { return Get(LastVersionExePathKey, "", false); }
        }

        public static string LatestVersionExePathKey = "LatestVersionExePath";

        public static string LatestVersionExePath
        {
            set { Set(LatestVersionExePathKey, value, false); }
            get { return Get(LatestVersionExePathKey, "", false); }
        }

        // LastUpdateFailedはキャッシュに利用？
        private static string LastUpdateFailedKey = "IsLastUpdateFailed";

        private static string LastUpdateFailed
        {
            set { Set(LastUpdateFailedKey, value, false); }
            get { return Get(LastUpdateFailedKey, "", false); }
        }

        public static bool IsLastUpdateFailed() { return LastUpdateFailed == "yes"; }
        public static void SetLastUpdateFailedYes() { LastUpdateFailed = "yes"; }
        public static void SetLastUpdateFailedNo() { LastUpdateFailed = "no"; }


        public static string LastUpdateVersionKey = "LastUpdateVersion";

        public static string LastUpdateVersion
        {
            set { Set(LastUpdateVersionKey, value, false); }
            get { return Get(LastUpdateVersionKey, "", false); }
        }

        public static string UpdateCheckUrl1 = "http://";
    }
}
