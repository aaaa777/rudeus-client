using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Rudeus.Model.Response;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Rudeus.Model
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
        public static readonly Uri LoginUri = new ("https://manager.nomiss.net/rudeus_login");

        private Device()
        {
            //DeviceId = deviceId;
            //Hostname = deviceName;
            // DeviceOS = "windows";
            //DeviceType = deviceType;
            // AccessToken = "";

            // ToDo：レジストリを確認して既に登録されている場合その情報を読み込む
            Settings settings = Settings.Load();

            // 起動毎にGUIDを生成してDevideIdとしている
            Guid g = System.Guid.NewGuid();
            string guid = g.ToString().Substring(0, 8);
            settings.Set("DeviceId", guid);

            // 起動毎にHostnameを生成
            Random r1 = new Random();

            string firstNumber  = r1.Next(10, 100).ToString();
            string secondNumber = r1.Next(100, 1000).ToString();
            string hostname = $"HIU-P{firstNumber}-{secondNumber}";
            settings.Set("Hostname", hostname);

            Username = settings.Get("DeviceUsername");
            DeviceId = settings.Get("DeviceId");
            Hostname = settings.Get("Hostname");
            AccessToken = settings.Get("AccessToken");
        }

        // シングルトン
        private static Device _instance;
        public static Device Load()
        {
            if (_instance == null)
            {
                //settings.Set("DeviceId", "1q2w3e4r5t6y7u");
                _instance = new Device();

            }
            return _instance;
        }

        public string Register()
        {
            RegisterResponse response;
            try
            {
                // リモートに登録申請する
                response = RemoteAPI.RegisterDevice(this.DeviceId, this.Hostname);
            } catch (Exception ex)
            {
                return null;
            }
            

            // アクセストークンの表示
            Console.WriteLine($"{response.response_data.access_token}");

            AccessToken = response.response_data.access_token;
            Settings.Load().SetAccessToken( AccessToken );
            return AccessToken;
        }

        public bool Update(string deviceStorage="")
        {
            UpdateResponse response;
            try
            {
                // リモートに更新申請する
                response = RemoteAPI.UpdateDevice(this.AccessToken, this.Hostname, this.Username);
            } catch (Exception ex)
            {
                return false;
            }


            // アクセストークンの表示
            Console.WriteLine($"{response.status}");

            return true;
        }

        /// <summary>
        /// SAML認証を行いユーザ名を取得し更新する
        /// </summary>
        /// <returns></returns>
        public async Task<string> LoginAsync()
        {

            try
            {
                // リモートにログイン申請する
                Task<string> studentIdTask = RemoteAPI.ReceiveStudentIdAsync();
                //Task<LoginResponse> responseTask = RemoteAPI.LoginDevice(this.AccessToken);

                // ブラウザを開く
                BrowserLaunchOptions options = new BrowserLaunchOptions()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Colors.Violet,
                    PreferredControlColor = Colors.SandyBrown
                };
                
                await Browser.Default.OpenAsync(Model.Device.LoginUri, options);

                string studentId = await studentIdTask;

                // Usernameを保存
                Settings.Load().Set("DeviceUsername", Username);


                // サーバーに情報送信
                LoginResponse response = RemoteAPI.LoginDevice(this.AccessToken, studentId);

                // SAMLから取得したユーザ名をセットする
                Username = response.response_data.username;

                // アクセストークンの表示
                Console.WriteLine($"{response.status}");

                Username = response.response_data.username;
            }
            catch (Exception ex)
            {
                // An unexpected error occurred. No browser may be installed on the device.
                
            }
            return Username;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
