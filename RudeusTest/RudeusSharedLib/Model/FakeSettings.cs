using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RudeusSharedLibTest.RudeusSharedLib.Model
{
    internal class FakeSettings : ISettings
    {

        private static ISettings _fakeSettings = new FakeSettings();
        public static ISettings GetInstance() { return _fakeSettings; }

        public static ISettings Create() { return new FakeSettings(); }

        public string LabelIdP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CurrentVersionP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LastDirNameP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LastVersionDirPathP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LastVersionExeNameP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string LastVersionExePathP => throw new NotImplementedException();

        public string LatestDirNameP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LatestVersionDirPathP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LatestVersionExeNameP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string LatestVersionExePathP => throw new NotImplementedException();

        public string LatestVersionStatusP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UpdatingChannelP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsBetaChannelP()
        {
            throw new NotImplementedException();
        }

        public bool IsDevelopChannelP()
        {
            throw new NotImplementedException();
        }

        public bool IsLatestVersionStatusDownloadedP()
        {
            throw new NotImplementedException();
        }

        public bool IsLatestVersionStatusOkP()
        {
            throw new NotImplementedException();
        }

        public bool IsLatestVersionStatusUnlaunchableP()
        {
            throw new NotImplementedException();
        }

        public bool IsStableChannelP()
        {
            throw new NotImplementedException();
        }

        public bool IsTestChannelP()
        {
            throw new NotImplementedException();
        }

        public void SetBetaChannelP()
        {
            throw new NotImplementedException();
        }

        public void SetDevelopChannelP()
        {
            throw new NotImplementedException();
        }

        public void SetLatestVersionStatusDownloadedP()
        {
            throw new NotImplementedException();
        }

        public void SetLatestVersionStatusOkP()
        {
            throw new NotImplementedException();
        }

        public void SetLatestVersionStatusUnlaunchableP()
        {
            throw new NotImplementedException();
        }

        public void SetStableChannelP()
        {
            throw new NotImplementedException();
        }

        public void SetTestChannelP()
        {
            throw new NotImplementedException();
        }
    }
}
