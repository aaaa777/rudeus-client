using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    /// <summary>
    /// レジストリの設定を保持するモデルのFake実装
    /// </summary>
    public class FakeSettings : IAppSettings, IRootSettings
    {
        // TODO: ダミーのレジストリを作成する

        private Dictionary<string, string> _data = new();
        private static IAppSettings _fakeSettings = new FakeSettings();
        public static IAppSettings GetInstance() { return _fakeSettings; }

        public static IAppSettings Create() { return new FakeSettings(); }

        private string Get(string key)
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

        private void Set(string key, string value)
        {
            if (_data.ContainsKey(key))
            {
                _data[key] = value;
            }
            else
            {
                _data.Add(key, value);
            }
        }

        public string LabelIdP { get => Get("LI"); set => Set("LI", value); }
        public string CurrentVersionP { get => Get("CV"); set => Set("CV", value); }
        public string LastDirNameP { get => Get("LD"); set => Set("LD", value); }
        public string LastVersionDirPathP { get => Get("LVD"); set => Set("LVD", value); }
        public string LastVersionExeNameP { get => Get("LVEN"); set => Set("LVEN", value); }

        public string LastVersionExePathP { get => Get("LVEP"); set => Set("LVEP", value); }

        public string LatestDirNameP { get => Get("LTD"); set => Set("LTD", value); }
        public string LatestVersionDirPathP { get => Get("LTVDP"); set => Set("LTVPD", value); }
        public string LatestVersionExeNameP { get => Get("LTVEN"); set => Set("LTVEN", value); }

        public string LatestVersionExePathP { get => Get("LTVEP"); set => Set("LTTVEP", value); }

        public string LatestVersionStatusP { get => Get("LTVS"); set => Set("LTVS", value); }
        public string UpdatingChannelP { get => Get("UC"); set => Set("UC", value); }

        public string AccessTokenP { get => Get("AT"); set => Set("AT", value); }
        public string DeviceIdP { get => Get("DI"); set => Set("DI", value); }
        public string FirstHostnameP { get => Get("FH"); set => Set("FH", value); }
        public string HostnameP { get => Get("H"); set => Set("H", value); }
        public string UsernameP { get => Get("U"); set => Set("U", value); }
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
