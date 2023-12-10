using Microsoft.Win32;

namespace SharedLib.Model.Settings
{
    /// <summary>
    /// 主要な設定をレジストリに保管するモデル
    /// </summary>
    public interface IRootSettings : IBaseSettings
    {
        //static abstract string GetStatic(string key, string defaultValue, bool isDefaultKey, RegistryKey regKey);

        //static abstract void SetStatic(string key, string value, bool isDefaultKey, RegistryKey regKey);

        //string InitedUsername { get; set; }
        //string InitedLabelId { get; set; }

        string AccessTokenP { get; set; }
        string DeviceIdP { get; set; }
        string FirstHostnameP { get; set; }
        string HostnameP { get; set; }
        string UsernameP { get; set; }

        string LabelIdP { get; set; }
        string CurrentVersionP { get; set; }
        string SpecP { get; set; }
        string CpuNameP { get; set; }
        string MemoryP { get; set; }
        string CDriveP { get; set; }
        string OSP { get; set; }
        string OSVersionP { get; set; }
        string WithSecureP { get; set; }
        string UpdatingChannelP { get; set; }

        bool IsBetaChannelP();
        bool IsDevelopChannelP();
        bool IsStableChannelP();
        bool IsTestChannelP();
        void SetBetaChannelP();
        void SetDevelopChannelP();
        void SetStableChannelP();
        void SetTestChannelP();

        string InterfacesHashP { get; set; }
    }
}