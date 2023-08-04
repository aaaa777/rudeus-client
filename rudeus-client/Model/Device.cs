using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using rudeus_client.Model.Response;
using System.Diagnostics;

namespace rudeus_client.Model
{
    /// <summary>
    /// デバイスID、デバイス名、デバイスタイプを含んだPCのModel
    /// モデルに対する操作はリモートと同期する
    /// </summary>
    internal class Device : INotifyPropertyChanged
    {

        private string _deviceId;
        private string _deviceName;
        private string _deviceType;

        // jsonには含まれない
        private string _accessToken = "";

        public string DeviceId
        {
            get => _deviceId;
            set
            {
                _deviceId = value;
                OnPropertyChanged();
            }
        }

        public string DeviceName
        {
            get => _deviceName;
            set
            {
                _deviceName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// pc|mobile
        /// 必要？
        /// </summary>
        public string DeviceType
        {
            get => _deviceType;
            set
            {
                _deviceType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string AccessToken
        {
            get => _accessToken;
            set
            {
                _accessToken = value;
                OnPropertyChanged();
            }
        }

        public Device(string deviceId, string deviceName, string deviceType)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
            // DeviceOS = "windows";
            DeviceType = deviceType;
            // AccessToken = "";
        }

        public string Register()
        {
            // リモートに登録申請する
            RegisterResponse response = RemoteAPI.RegisterDevice(this);
            

            // アクセストークンの表示
            Console.WriteLine($"{response.ResponseData.AccessToken}");

            AccessToken = response.ResponseData.AccessToken;
            return AccessToken;
        }

        public string Update()
        {
            // リモートに更新申請する
            UpdateResponse response = RemoteAPI.UpdateDevice(this);

            // アクセストークンの表示
            Console.WriteLine($"{response.Status}");

            return AccessToken;
        }

        public string Login()
        {
            // リモートにログイン申請する
            LoginResponse response = RemoteAPI.LoginDevice(this);

            // アクセストークンの表示
            Console.WriteLine($"{response.Status}");

            AccessToken = response.Status;
            return AccessToken;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
