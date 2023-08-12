using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model
{
    /// <summary>
    /// 設定をレジストリに保管するモデル
    /// レジストリがない場合の変換や暗号化を行う予定
    /// </summary>
    internal class Settings
    {
#if WINDOWS
        private Microsoft.Win32.RegistryKey RegKey;
#endif
        private static Settings _instanse;
        private Settings()
        {
            // レジストリに対応していないプラットフォームの場合、別の場所に保存する必要がある
#if WINDOWS
            this.RegKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\test\sub");
#endif
        }

        public static Settings Load()
        {
            if(_instanse == null)
            {
                _instanse = new Settings();
            }
            return _instanse;
        }

        public string Get(string key)
        {
#if WINDOWS
            var value = RegKey.GetValue(key);
            if (value == null)
            {
                return "";
            }
            return (string)value;
#else
            return "";
#endif
        }

        public void Set(string key, string value) 
        {
#if WINDOWS
            RegKey.SetValue(key, value);
#endif
        }

    }
}
