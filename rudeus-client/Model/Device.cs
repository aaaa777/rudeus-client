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
using Microsoft.VisualBasic;

namespace rudeus_client.Model
{
    /// <summary>
    /// デバイスID、デバイス名、デバイスタイプを含んだPCのModel
    /// モデルに対する操作はリモートと同期する
    /// </summary>
    internal class Device : INotifyPropertyChanged
    {

        private string _deviceId;
        private string _hostname;
        private string _deviceType;

        // jsonには含まれない
        private string _username;
        private string _accessToken = "";

        public string DeviceId
        {
            get => _deviceId;
            set
            {
                _deviceId = value;
                OnPropertyChanged(nameof(DeviceId));
            }
        }

        public string Hostname
        {
            get => _hostname;
            set
            {
                _hostname = value;
                OnPropertyChanged(nameof(Hostname));
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
                OnPropertyChanged(nameof(DeviceType));
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
                OnPropertyChanged(nameof(AccessToken));
            }
        }

        [JsonIgnore]
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private Device(string deviceId, string deviceName, string deviceType)
        {
            DeviceId = deviceId;
            Hostname = deviceName;
            // DeviceOS = "windows";
            DeviceType = deviceType;
            // AccessToken = "";

            // ToDo：レジストリを確認して既に登録されている場合その情報を読み込む


        }

        // シングルトン
        private static Device _instance;
        public static Device Load()
        {
            if (_instance == null)
            {
                _instance = new Device("test_id", "HIU-P123", "pc");
            }
            return _instance;
        }

        public string Register()
        {
            // リモートに登録申請する
            RegisterResponse response = RemoteAPI.RegisterDevice(this);
            

            // アクセストークンの表示
            Console.WriteLine($"{response.response_data.access_token}");

            AccessToken = response.response_data.access_token;
            return AccessToken;
        }

        public bool Update(string deviceStorage="")
        {
            // リモートに更新申請する
            UpdateResponse response = RemoteAPI.UpdateDevice(this);

            // アクセストークンの表示
            Console.WriteLine($"{response.status}");

            return true;
        }


        public async Task<bool> LoginAsync()
        {
            // リモートにログイン申請する
            LoginResponse response = await RemoteAPI.LoginDevice(this);
            // SAMLから取得したユーザ名をセットする
            Username = response.response_data.username;

            // アクセストークンの表示
            Console.WriteLine($"{response.status}");

            Username = response.response_data.username;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
