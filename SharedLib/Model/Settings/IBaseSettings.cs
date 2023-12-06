using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Model.Settings
{
    public interface IBaseSettings
    {
        //static abstract string GetStatic(string key, string defaultValue, bool isDefaultKey, RegistryKey regKey);
        //static abstract void SetStatic(string key, string value, bool isDefaultKey, RegistryKey regKey);

        string Get(string key, string defaultString);
        bool Set(string key, string value);

        // DI用のメソッドラッパー
        Func<string, string, string> GetFunc { get; set; }
        Func<string, string, bool> SetFunc { get; set; }
    }
}
