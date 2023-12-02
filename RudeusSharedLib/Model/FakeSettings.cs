using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model
{
    internal class FakeSettings : ISettings
    {
        // TODO: ダミーのレジストリを作成する

        private static ISettings _fakeSettings = new FakeSettings();
        public static ISettings GetInstance() { return _fakeSettings; }

        public static ISettings Create() { return new FakeSettings(); }

        public string LabelIdP { get => "P12-345"; set { } }
        public string CurrentVersionP { get => "1.0.0"; set { } }
        public string LastDirNameP { get => "test"; set { } }
        public string LastVersionDirPathP { get => "/test"; set { } }
        public string LastVersionExeNameP { get => "test.exe"; set { } }

        public string LastVersionExePathP => throw new NotImplementedException();

        public string LatestDirNameP { get => "test"; set { } }
        public string LatestVersionDirPathP { get => "/test"; set { } }
        public string LatestVersionExeNameP { get => "test.exe"; set { } }

        public string LatestVersionExePathP => throw new NotImplementedException();

        public string LatestVersionStatusP { get => "1.0.0"; set { } }
        public string UpdatingChannelP { get => "test"; set { } }

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
            return false;
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
            
        }

        public void SetStableChannelP()
        {
            
        }

        public void SetTestChannelP()
        {
            
        }
    }
}
