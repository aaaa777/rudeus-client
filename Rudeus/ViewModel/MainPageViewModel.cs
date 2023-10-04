using Rudeus.View;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Rudeus.ViewModel
{
    /// <summary>
    /// モデルの変更をViewに通知するためのViewModel
    /// </summary>
    internal class MainPageViewModel : INotifyPropertyChanged
    {


        private Model.Device _myDevice;
        public Model.Device MyDevice
        {
            get => _myDevice;
            set
            {
                _myDevice = value;
                OnPropertyChanged(nameof(MyDevice));
                OnPropertyChanged(nameof(MyDeviceId));
                OnPropertyChanged(nameof(MyDeviceName));
                OnPropertyChanged(nameof(MyDeviceUsername));
                OnPropertyChanged(nameof(MyDeviceAccessToken));
                OnPropertyChanged(nameof(Text));
                OnPropertyChanged(nameof(Text2));
                OnPropertyChanged(nameof(Text3));
            } 
        }

        public string MyDeviceId
        {
            get => MyDevice.DeviceId;
            set
            {
                MyDevice.DeviceId = value;
            }
        }
        public string MyDeviceName
        {
            get => MyDevice.Hostname;
            set
            {
                MyDevice.Hostname = value;
            }
        }
        public string MyDeviceAccessToken
        {
            get => MyDevice.AccessToken;
            set
            {
                MyDevice.AccessToken = value;
            }
        }
        public string MyDeviceUsername
        {
            get => MyDevice.Username;
            set
            {
                MyDevice.Username = value;
            }
        }

        public string ApiRegisterPath
        {
            get => RemoteAPI.ApiRegisterPath;
            set
            {
                RemoteAPI.ApiRegisterPath = value;
                OnPropertyChanged(nameof(ApiRegisterPath));
            }
        }
        public string ApiUpdatePath
        {
            get => RemoteAPI.ApiUpdatePath;
            set
            {
                RemoteAPI.ApiUpdatePath = value;
                OnPropertyChanged(nameof(ApiUpdatePath));
            }
        }
        public string ApiLoginPath
        {
            get => RemoteAPI.ApiLoginPath;
            set
            {
                RemoteAPI.ApiLoginPath = value;
                OnPropertyChanged(nameof(ApiLoginPath));
            }
        }

        public string ApiEndpoint
        {
            get => RemoteAPI.ApiEndpoint;
            set
            {
                RemoteAPI.ApiEndpoint = value;
                OnPropertyChanged(nameof(ApiEndpoint));
            }
        }

        // Requestなどを表示するためのデータバインド
        public string DebugBoxLastText
        {
            get => DebugBox.Load().LastText;
            set
            {
                DebugBox.Load().LastText = value;
            }
        }

        public string Text { get => $"Hostname: {MyDevice?.Hostname}"; }
        public string Text2 { get => $"Username: {MyDevice?.Username}"; }
        public string Text3 { get => $"AccessToken: {MyDevice?.AccessToken}"; }

        public void ReloadDevice()
        {
            MyDevice = Model.Device.Load();
        }
        public void ReloadDebugBox()
        {
            OnPropertyChanged(nameof(DebugBoxLastText));
        }

        public MainPageViewModel()
        {
            ApiEndpoint = RemoteAPI.ApiEndpoint;
            ApiRegisterPath = RemoteAPI.ApiRegisterPath;
            ApiUpdatePath = RemoteAPI.ApiUpdatePath;
            ApiLoginPath = RemoteAPI.ApiLoginPath;

            ReloadDevice();
        }

        public void RegisterDevice()
        {
            MyDevice.Register();
            ReloadDevice();
            DebugBox db = DebugBox.Load();
            ReloadDebugBox();
        }

        public void UpdateDevice()
        {
            MyDevice.Update();
            ReloadDevice();
            ReloadDebugBox();
        }

        public void UpdateDebugBoxText(string text)
        {
            DebugBoxLastText = text;
            ReloadDebugBox();
        }

        /// <summary>
        /// ブラウザログインを行う
        /// 中断されるとTaskがfalseを返す
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoginDeviceAsync()
        {
            await MyDevice.LoginAsync();

            ReloadDevice();
            ReloadDebugBox();
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

