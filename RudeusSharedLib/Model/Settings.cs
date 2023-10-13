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
        // ToDo: いつかシングルトンじゃなくてStaticに変更したい

        private static RegistryKey? RegKey;

        private static Settings? _instanse;
        private Settings()
        {
            // レジストリに対応していないプラットフォームの場合、別の場所に保存する必要がある

            RegKey = Registry.CurrentUser.CreateSubKey(@"Software\test\sub");
            //RegKey = Registry.LocalMachine.CreateSubKey(@"Software\test\sub");

        }

        public static Settings Load()
        {
            if(_instanse == null)
            {
                _instanse = new Settings();
            }
            return _instanse;
        }

        private string Get(string key, string defaultValue="")
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

        private void Set(string key, string value) { RegKey?.SetValue(key, value); }

        public string FirstHostnameKey = "FirstHostname";
        public string FirstHostname
        {
            set { Set(FirstHostnameKey, value); }
            get { return Get(FirstHostnameKey); }
        }

        public string HostnameKey = "DeviceHostname";
        public string Hostname
        {
            set { Set(HostnameKey, value); }
            get { return Get(HostnameKey); }
        }

        public string DeviceIdKey = "DeviceId";
        public string DeviceId
        {
            set { Set(DeviceIdKey, value); }
            get { return Get(DeviceIdKey); }
        }

        public string UsernameKey = "DeviceUsername";
        public string Username
        {
            set { Set(UsernameKey, value); }
            get { return Get(UsernameKey); }
        }

        public string AccessTokenKey = "AccessToken";
        public string AccessToken
        {
            set { Set(AccessTokenKey, value); }
            get { return Get(AccessTokenKey); }
        }
    }
}
