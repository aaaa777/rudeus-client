using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Model.Settings
{
    public interface IBaseSettings
    {
        string Get(string key, string defaultString);
        bool Set(string key, string value);

        // DI用のメソッドラッパー
        Func<string, string, string> GetFunc { get; set; }
        Func<string, string, bool> SetFunc { get; set; }
    }
}
