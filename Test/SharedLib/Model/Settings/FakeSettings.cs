using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.SharedLib.Model.Settings
{
    /// <summary>
    /// レジストリの設定を保持するモデルのFake実装
    /// </summary>
    public class FakeSettings : IAppSettings, IRootSettings
    {
        public Func<string, string, string> GetFunc { get; set; }
        public Func<string, string, string> SetFunc { get; set; }

        public FakeSettings(Func<string, string, string>? getFunc = null, Func<string, string, string>? setFunc = null) 
        {
            GetFunc = getFunc ?? Get;
            SetFunc = setFunc ?? Set;
        }

        // TODO: ダミーのレジストリを作成する

        private Dictionary<string, string> _data = new();
        private static IAppSettings _fakeSettings = new FakeSettings();
        public static IAppSettings GetInstance() { return _fakeSettings; }

        public static IAppSettings Create() { return new FakeSettings(); }

        public string Get(string key, string defaultString = "")
        {
            if (_data.ContainsKey(key))
            {
                return _data[key];
            }
            else
            {
                return "";
            }
        }

        public string Set(string key, string value)
        {
            if (_data.ContainsKey(key))
            {
                _data[key] = value;
            }
            else
            {
                _data.Add(key, value);
            }
            return value;
        }

        public string LabelIdP { get => GetFunc("LI", ""); set => SetFunc("LI", value); }
        public string CurrentVersionP { get => GetFunc("CV", ""); set => SetFunc("CV", value); }
        public string LastDirNameP { get => GetFunc("LD", ""); set => SetFunc("LD", value); }
        public string LastVersionDirPathP { get => GetFunc("LVD", ""); set => SetFunc("LVD", value); }
        public string LastVersionExeNameP { get => GetFunc("LVEN", ""); set => SetFunc("LVEN", value); }

        public string LastVersionExePathP { get => GetFunc("LVEP", ""); set => SetFunc("LVEP", value); }

        public string LatestDirNameP { get => GetFunc("LTD", ""); set => SetFunc("LTD", value); }
        public string LatestVersionDirPathP { get => GetFunc("LTVDP", ""); set => SetFunc("LTVPD", value); }
        public string LatestVersionExeNameP { get => GetFunc("LTVEN", ""); set => SetFunc("LTVEN", value); }

        public string LatestVersionExePathP { get => GetFunc("LTVEP", ""); set => SetFunc("LTTVEP", value); }

        public string LatestVersionStatusP { get => GetFunc("LTVS", ""); set => SetFunc("LTVS", value); }
        public string UpdatingChannelP { get => GetFunc("UC", ""); set => SetFunc("UC", value); }

        public string AccessTokenP { get => GetFunc("AT", ""); set => SetFunc("AT", value); }
        public string DeviceIdP { get => GetFunc("DI", ""); set => SetFunc("DI", value); }
        public string FirstHostnameP { get => GetFunc("FH", ""); set => SetFunc("FH", value); }
        public string HostnameP { get => GetFunc("H", ""); set => SetFunc("H", value); }
        public string UsernameP { get => GetFunc("U", ""); set => SetFunc("U", value); }
        public string SpecP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CpuNameP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string MemoryP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CDriveP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OSP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OSVersionP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WithSecureP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsBetaChannelP()
        {
            return false;
        }

        public bool IsDevelopChannelP()
        {
            return false;
        }

        public bool IsLatestVersionStatusDownloadedP()
        {
            return false;
        }

        public bool IsLatestVersionStatusOkP()
        {
            return false;
        }

        public bool IsLatestVersionStatusUnlaunchableP()
        {
            return LatestVersionStatusP == "unlaunchable";
        }

        public bool IsStableChannelP()
        {
            return false;
        }

        public bool IsTestChannelP()
        {
            return false;
        }

        public void SetBetaChannelP()
        {

        }

        public void SetDevelopChannelP()
        {

        }

        public void SetLatestVersionStatusDownloadedP()
        {

        }

        public void SetLatestVersionStatusOkP()
        {

        }

        public void SetLatestVersionStatusUnlaunchableP()
        {
            LatestVersionStatusP = "unlaunchable";
        }

        public void SetStableChannelP()
        {

        }

        public void SetTestChannelP()
        {

        }
    }
}
