using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        private static string DefaultRegistryKey = Constants.DefaultRegistryKey;

        // レジストリ：アプリのルート
        private static string RegistryDir = Constants.RegistryDir;
        private static string RegistryKey = DefaultRegistryKey;
        private static string RegistryPath { get { return $"{RegistryDir}\\{RegistryKey}"; } }

        private static RegistryKey? RegKey = Registry.CurrentUser.CreateSubKey(RegistryKey);


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

            RegKey = Registry.CurrentUser.CreateSubKey(RegistryPath);
        }


        private static string Get(string key, string defaultValue = "", bool isDefault = true)
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

        private static void Set(string key, string value, bool isDefault = true)
        {
            if (isDefault)
            {
                UpdateRegistryKey();
            }

            RegKey?.SetValue(key, value);
        }


        // ここからデフォルトのレジストリキーでのみ保存可能

        // 最初に登録されたHostname
        public static string FirstHostnameKey = "FirstHostname";
        public static string FirstHostname
        {
            set { Set(FirstHostnameKey, value); }
            get { return Get(FirstHostnameKey); }
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

        // デフォルトのレジストリキーでのみ保存可能ここまで



        // ここからデフォルト以外のレジストリ―キーでも保存可能

        // アップデートのチャンネルを指定
        public static string UpdateChannelKey = "UpdatingChannel";

        public static string UpdatingChannel
        {
            set { Set(UpdateChannelKey, value, false); }
            get { return Get(UpdateChannelKey, "", false); }
        }


        // チャンネルの判定メソッド

        // 各チャンネルについて説明
        // Beta     Stableの少し先のバージョンを試す事ができる
        // Test     未使用
        // Delelop  Visual Studioで実行できる設定
        // Stable   上記以外のキーだった場合
        public static bool IsBetaChannel() { return UpdatingChannel == "beta"; }
        public static bool IsTestChannel() { return UpdatingChannel == "test"; }
        public static bool IsDevelopChannel() { return UpdatingChannel == "develop"; }
        public static bool IsStableChannel() { return !(IsTestChannel() || IsDevelopChannel() || IsBetaChannel()); }

        public static void SetBetaChannel() { UpdatingChannel = "beta"; }
        public static void SetTestChannel() { UpdatingChannel = "test"; }
        public static void SetDevelopChannel() { UpdatingChannel = "develop"; }
        public static void SetStableChannel() { UpdatingChannel = "stable"; }


        // ダウンロード失敗やバージョンにバグがあった場合のフォールバック設定

        // latest   ダウンロードした最新バージョン
        // last     最後に実行できた正常なバージョン
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


        // アップデート対象にするディレクトリ等の設定

        private static string LastVersionDirPathKey = "LastVersionDirPath";

        public static string LastVersionDirPath
        {
            set { Set(LastVersionDirPathKey, value, false); }
            get { return Get(LastVersionDirPathKey, "", false); }
        }

        private static string LatestVersionDirPathKey = "LatestVersionDirPath";

        public static string LatestVersionDirPath
        {
            set { Set(LatestVersionDirPathKey, value, false); }
            get { return Get(LatestVersionDirPathKey, "", false); }
        }

        private static string LastVersionExeNameKey = "LastVersionExeName";

        public static string LastVersionExeName
        {
            set { Set(LastVersionExeNameKey, value, false); }
            get { return Get(LastVersionExeNameKey, "", false); }
        }

        private static string LatestVersionExeNameKey = "LatestVersionExeName";

        public static string LatestVersionExeName
        {
            set { Set(LatestVersionExeNameKey, value, false); }
            get { return Get(LatestVersionExeNameKey, "", false); }
        }

        private static string LastDirNameKey = "LastDirname";

        public static string LastDirName
        {
            set { Set(LastDirNameKey, value, false); }
            get { return Get(LastDirNameKey, "", false); }
        }

        private static string LatestDirNameKey = "LatestDirname";

        public static string LatestDirName
        {
            set { Set(LatestDirNameKey, value, false); }
            get { return Get(LatestDirNameKey, "", false); }
        }

        // exeを実行できる絶対パス
        public static string LastVersionExePath { get { return $"{LastVersionDirPath}\\{LastVersionExeName}"; } }
        public static string LatestVersionExePath { get { return $"{LatestVersionDirPath}\\{LatestVersionExeName}"; } }


        // LastUpdateFailedは未使用

        private static string LastUpdateFailedKey = "IsLastUpdateFailed";

        private static string LastUpdateFailed
        {
            set { Set(LastUpdateFailedKey, value, false); }
            get { return Get(LastUpdateFailedKey, "", false); }
        }

        public static bool IsLastUpdateFailed() { return LastUpdateFailed == "yes"; }
        public static void SetLastUpdateFailedYes() { LastUpdateFailed = "yes"; }
        public static void SetLastUpdateFailedNo() { LastUpdateFailed = "no"; }


        // lastに存在するバージョンについて
        public static string LastUpdateVersionKey = "LastUpdateVersion";

        public static string LastUpdateVersion
        {
            set { Set(LastUpdateVersionKey, value, false); }
            get { return Get(LastUpdateVersionKey, "", false); }
        }

        public static string UpdateCheckUrl1 = "http://";
    }
}
