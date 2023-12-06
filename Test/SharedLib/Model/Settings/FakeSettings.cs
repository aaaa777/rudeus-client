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
        public Func<string, string, bool> SetFunc { get; set; }

        public FakeSettings(Func<string, string, string>? getFunc = null, Func<string, string, bool>? setFunc = null) 
        {
            GetFunc = getFunc ?? Get;
            SetFunc = setFunc ?? Set;
        }

        // TODO: ダミーのレジストリを作成する

        private Dictionary<string, string> _data = new() { };
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

        public bool Set(string key, string value)
        {
            if (_data.ContainsKey(key))
            {
                _data[key] = value;
            }
            else
            {
                _data.Add(key, value);
            }
            return true;
        }

        public string LabelIdP { get => GetFunc("LI", ""); set => SetFunc("LI", value); }
        public string CurrentVersionP { get => GetFunc("CV", ""); set => SetFunc("CV", value); }
        public string LastDirNameP { get => GetFunc("LD", ""); set => SetFunc("LD", value); }
        public string LastVersionDirPathP { get => GetFunc("LVD", ""); set => SetFunc("LVD", value); }
        public string LastVersionExeNameP { get => GetFunc("LVEN", ""); set => SetFunc("LVEN", value); }

        public string LastVersionExePathP { get => $"{GetFunc("LVD", "")}\\{GetFunc("LVEN", "")}"; }

        public string LatestDirNameP { get => GetFunc("LTD", ""); set => SetFunc("LTD", value); }
        public string LatestVersionDirPathP { get => GetFunc("LTVDP", ""); set => SetFunc("LTVDP", value); }
        public string LatestVersionExeNameP { get => GetFunc("LTVEN", ""); set => SetFunc("LTVEN", value); }

        public string LatestVersionExePathP { get => $"{GetFunc("LTVDP", "")}\\{GetFunc("LTVEN", "")}"; }

        public string LatestVersionStatusP { get => GetFunc("LTVS", ""); set => SetFunc("LTVS", value); }
        public string UpdatingChannelP { get => GetFunc("UC", ""); set => SetFunc("UC", value); }

        public string AccessTokenP { get => GetFunc("AT", ""); set => SetFunc("AT", value); }
        public string DeviceIdP { get => GetFunc("DI", ""); set => SetFunc("DI", value); }
        public string FirstHostnameP { get => GetFunc("FH", ""); set => SetFunc("FH", value); }
        public string HostnameP { get => GetFunc("H", ""); set => SetFunc("H", value); }
        public string UsernameP { get => GetFunc("U", ""); set => SetFunc("U", value); }
        public string SpecP { get => GetFunc("S", ""); set => SetFunc("S", value); }
        public string CpuNameP { get => GetFunc("CN", ""); set => SetFunc("CN", value); }
        public string MemoryP { get => GetFunc("M", ""); set => SetFunc("M", value); }
        public string CDriveP { get => GetFunc("CD", ""); set => SetFunc("CD", value); }
        public string OSP { get => GetFunc("OS", ""); set => SetFunc("OS", value); }
        public string OSVersionP { get => GetFunc("OSV", ""); set => SetFunc("OSV", value); }
        public string WithSecureP { get => GetFunc("WS", ""); set => SetFunc("WS", value); }

        public bool IsBetaChannelP()
        {
            return UpdatingChannelP == "beta";
        }

        public bool IsDevelopChannelP()
        {
            return UpdatingChannelP == "develop";
        }

        public bool IsLatestVersionStatusDownloadedP()
        {
            return LatestVersionStatusP == "downloaded";
        }

        public bool IsLatestVersionStatusOkP()
        {
            return LatestVersionStatusP == "ok";
        }

        public bool IsLatestVersionStatusUnlaunchableP()
        {
            return LatestVersionStatusP == "unlaunchable";
        }

        public bool IsStableChannelP()
        {
            return !(IsBetaChannelP() || IsDevelopChannelP() || IsTestChannelP());
        }

        public bool IsTestChannelP()
        {
            return UpdatingChannelP == "test";
        }

        public void SetBetaChannelP()
        {
            UpdatingChannelP = "beta";
        }

        public void SetDevelopChannelP()
        {
            UpdatingChannelP = "develop";
        }

        public void SetLatestVersionStatusDownloadedP()
        {
            LatestVersionStatusP = "downloaded";
        }

        public void SetLatestVersionStatusOkP()
        {
            LatestVersionStatusP = "ok";
        }

        public void SetLatestVersionStatusUnlaunchableP()
        {
            LatestVersionStatusP = "unlaunchable";
        }

        public void SetStableChannelP()
        {
            UpdatingChannelP = "stable";
        }

        public void SetTestChannelP()
        {
            UpdatingChannelP = "test";
        }
    }
}
