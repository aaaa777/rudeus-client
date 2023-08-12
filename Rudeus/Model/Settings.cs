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
        private Microsoft.Win32.RegistryKey RegKey;

        private static Settings _instanse;
        private Settings()
        {
            this.RegKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Software\test\sub");

            
#if WINDOWS
            // レジストリに対応していないプラットフォームの場合、別の場所に保存する必要がある
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
            var value = RegKey.GetValue(key);
            if (value == null)
            {
                return "";
            }
            return (string)value;
            
        }

        public void Set(string key, string value) 
        {
            RegKey.SetValue(key, value);
        }

    }
}
