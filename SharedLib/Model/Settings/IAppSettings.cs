using Microsoft.Win32;

namespace SharedLib.Model.Settings
{
    /// <summary>
    /// 設定をレジストリに保管するモデル
    /// レジストリを指定して異なる保存先を指定することもできる
    /// </summary>
    public interface IAppSettings : IBaseSettings
    {
        //static abstract string GetStatic(string key, string defaultValue, bool isDefaultKey, RegistryKey regKey);

        //static abstract void SetStatic(string key, string value, bool isDefaultKey, RegistryKey regKey);

        string CurrentVersionP { get; set; }
        string LastDirNameP { get; set; }
        string LastVersionDirPathP { get; set; }
        string LastVersionExeNameP { get; set; }
        string LastVersionExePathP { get; }
        string LatestDirNameP { get; set; }
        string LatestVersionDirPathP { get; set; }
        string LatestVersionExeNameP { get; set; }
        string LatestVersionExePathP { get; }
        string LatestVersionStatusP { get; set; }
        string UpdatingChannelP { get; set; }

        bool IsLatestVersionStatusDownloadedP();
        bool IsLatestVersionStatusOkP();
        bool IsLatestVersionStatusUnlaunchableP();
        void SetLatestVersionStatusDownloadedP();
        void SetLatestVersionStatusOkP();
        void SetLatestVersionStatusUnlaunchableP();
        /*
        bool IsBetaChannelP();
        bool IsDevelopChannelP();
        bool IsLatestVersionStatusDownloadedP();
        bool IsLatestVersionStatusOkP();
        bool IsLatestVersionStatusUnlaunchableP();
        bool IsStableChannelP();
        bool IsTestChannelP();
        void SetBetaChannelP();
        void SetDevelopChannelP();
        void SetLatestVersionStatusDownloadedP();
        void SetLatestVersionStatusOkP();
        void SetLatestVersionStatusUnlaunchableP();
        void SetStableChannelP();
        void SetTestChannelP();
        */
    }
}