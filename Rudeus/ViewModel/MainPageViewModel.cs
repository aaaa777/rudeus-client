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

        public string Text { get => $"DeviceName: {MyDevice?.Hostname}"; }
        public string Text2 { get => $"Username: {MyDevice?.Username}"; }
        public string Text3 { get => $"AccessToken: {MyDevice?.AccessToken}"; }

        public void ReloadDevice()
        {
            MyDevice = Model.Device.Load();
        }

        public MainPageViewModel()
        {
            ReloadDevice();
        }

        public void RegisterDevice()
        {
            MyDevice.Register();
            ReloadDevice();
        }

        public void UpdateDevice()
        {
            MyDevice.Update();
            ReloadDevice();
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
            return true;
        }

        public async void OpenBrowser()
        {

            // await Browser.Default.OpenAsync(Model.Device.LoginUri);
            // await RemoteAPI.StartSamlLoginAsync();

            await RemoteAPI.StartSamlLoginAsync("");
            // await MyDevice.LoginAsync();
            MyDevice = Model.Device.Load();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

