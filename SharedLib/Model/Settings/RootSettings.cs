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

        private static RegistryKey? RegKey;

        public Func<string, string, string> GetFunc { get; set; }
        public Func<string, string, bool> SetFunc { get; set; }

        private static RegistryKey CreateRegKey(string keyName)
        {
            return Registry.LocalMachine.CreateSubKey(keyName);// ?? new Exception("key creation failed");
        }

        public RootSettings(Func<string, string, string>? getFunc = null, Func<string, string, bool>? setFunc = null, Func<string, RegistryKey>? createRegFunc = null)
        {
            GetFunc = getFunc ?? Get;
            SetFunc = setFunc ?? Set;
            RegKey = createRegFunc != null ? createRegFunc($"{RegistryDir}\\{RegistryKey}") : CreateRegKey($"{RegistryDir}\\{RegistryKey}");
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
            RegKey?.SetValue(key, value);
            return true;
        }

        // ここからデフォルトのレジストリキーでのみ保存可能

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


        // ラベルに記載されたID
        public static string LabelIdKey = "LabeledId";

        public string LabelIdP
        {
            set { SetFunc(LabelIdKey, value); }
            get { return GetFunc(LabelIdKey, ""); }
        }

        public static string UpdateChannelKey = "UpdateChannel";
        public string UpdatingChannelP
        {
            set { SetFunc(UpdateChannelKey, value); }
            get { return GetFunc(UpdateChannelKey, ""); }
        }

        public bool IsBetaChannelP() { return UpdatingChannelP == "beta"; }
        public bool IsTestChannelP()
        {
            return UpdatingChannelP == "test";
        }
        public bool IsDevelopChannelP() { return UpdatingChannelP == "develop"; }
        public bool IsStableChannelP() { return !(IsTestChannelP() || IsDevelopChannelP() || IsBetaChannelP()); }

        public void SetBetaChannelP() { UpdatingChannelP = "beta"; }
        public void SetTestChannelP()
        {
            UpdatingChannelP = "test";
        }
        public void SetDevelopChannelP() { UpdatingChannelP = "develop"; }
        public void SetStableChannelP() { UpdatingChannelP = "stable"; }


        // lastに存在するバージョンについて
        public static string CurrentVersionKey = "CurrentVersion";
        public string CurrentVersionP
        {
            set { SetFunc(CurrentVersionKey, value); }
            get { return GetFunc(CurrentVersionKey, ""); }
        }

        public string NetworkIFListKey = "NetworkIFList";
        public string NetworkIFListP
        {
            set { SetFunc(NetworkIFListKey, value); }
            get { return GetFunc(NetworkIFListKey, ""); }
        }

        public string SpecKey = "Spec";
        public string SpecP
        {
            set { SetFunc(SpecKey, value); }
            get { return GetFunc(SpecKey, ""); }
        }

        public string CpuNameKey = "CpuName";
        public string CpuNameP
        {
            set { SetFunc(CpuNameKey, value); }
            get { return GetFunc(CpuNameKey, ""); }
        }

        public string MemoryKey = "Memory";
        public string MemoryP
        {
            set { SetFunc(MemoryKey, value); }
            get { return GetFunc(MemoryKey, ""); }
        }
        
        public string CDriveKey = "CDrive";
        public string CDriveP
        {
            set { SetFunc(CDriveKey, value); }
            get { return GetFunc(CDriveKey, ""); }
        }

        public string OSKey = "OS";
        public string OSP
        {
            set { SetFunc(OSKey, value); }
            get { return GetFunc(OSKey, ""); }
        }

        public string OSVersionKey = "OSVersion";
        public string OSVersionP
        {
            set { SetFunc(OSVersionKey, value); }
            get { return GetFunc(OSVersionKey, ""); }
        }

        public string WithSecureKey = "WithSecure";
        public string WithSecureP
        {
            set { SetFunc(WithSecureKey, value); }
            get { return GetFunc(WithSecureKey, ""); }
        }
        
        // for debugging

        //public static string 
    }
}
