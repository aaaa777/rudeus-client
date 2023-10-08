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
//#if WINDOWS
        private static RegistryKey RegKey;
//#endif
        private static Settings _instanse;
        private Settings()
        {
            // レジストリに対応していないプラットフォームの場合、別の場所に保存する必要がある
//#if WINDOWS
            //RegKey = Registry.CurrentUser.CreateSubKey(@"Software\test\sub");
            RegKey = Registry.LocalMachine.CreateSubKey(@"Software\test\sub");
//#endif
        }

        public static Settings Load()
        {
            if(_instanse == null)
            {
                _instanse = new Settings();
            }
            return _instanse;
        }

        public string Get(string key, string defaultValue="")
        {
//#if WINDOWS
            var value = RegKey.GetValue(key);
            if (value == null)
            {
                return defaultValue;
            }
            return (string)value;
//#else
            return "";
//#endif
        }

        public void Set(string key, string value) 
        {
//#if WINDOWS
            RegKey.SetValue(key, value);
//#endif
        }


        public string GetAccessToken()
        {
            return Get("AccessToken");
        }

        public void SetAccessToken(string token)
        {
            Set("AccessToken", token);
        }


        public string GetDeviceId()
        {
            return Get("DeviceId");
        }

        public void SetDeviceId(string deviceId)
        {
            Set("DeviceId", deviceId);
        }

        
        public string GetUsername()
        {
            return Get("DeviceUsername");
        }

        public void SetUsername(string username)
        {
            Set("DeviceUsername", username);
        }


        public string GetHostname()
        {
            return Get("DeviceHostname");
        }

        public void SetHostname(string hostname)
        {
            Set("DeviceHostname", hostname);
        }

        public string FirstHostname
        {
            set { Set("FirstHostname", value); }
            get { return Get("FirstHostname"); }
        }

        public string Hostname
        {
            set { Set("DeviceHostname", value); }
            get { return Get("DeviceHostname"); }
        }

        public string DeviceId
        {
            set { Set("DeviceId", value); }
            get { return Get("DeviceId"); }
        }
        public string Username
        {
            set { Set("DeviceUsername", value); }
            get { return Get("DeviceUsername"); }
        }
        public string AccessToken
        {
            set { Set("AccessToken", value); }
            get { return Get("AccessToken"); }
        }
    }
}
