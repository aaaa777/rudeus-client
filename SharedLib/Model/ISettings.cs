﻿using Microsoft.Win32;

namespace Rudeus.Model
{
    /// <summary>
    /// 設定をレジストリに保管するモデル
    /// </summary>
    public interface ISettings
    {
        //static abstract string Get(string key, string defaultValue, bool isDefaultKey, RegistryKey regKey);

        //static abstract void Set(string key, string value, bool isDefaultKey, RegistryKey regKey);

        string AccessTokenP { get; set; }
        string DeviceIdP { get; set; }
        string FirstHostnameP { get; set; }
        string HostnameP { get; set; }
        string UsernameP { get; set; }

        string LabelIdP { get; set; }
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
    }
}