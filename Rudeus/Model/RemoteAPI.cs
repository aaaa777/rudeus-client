using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using System.Net;

using System.Text.Json;
using Rudeus.Model.Response;
using Rudeus.Model.Request;
using System.Xml;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Runtime.ConstrainedExecution;
using System.Collections.Specialized;

using CommunityToolkit.Maui.Alerts;


namespace Rudeus.Model
{
 
    /// <summary>
    /// 管理サーバとREST APTで通信する、モデルのプロパティを読み取るが書き込みはしない
    /// </summary>
    internal class RemoteAPI
    {

        

        /// <summary>
        /// Todo: クライアント証明書を追加する
        /// https://stackoverflow.com/questions/40014047/add-client-certificate-to-net-core-httpclient
        /// </summary>
        private static readonly HttpClientHandler handler = new()
        {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            SslProtocols = SslProtocols.Tls12,
        //    ClientCertificates = new X509CertificateCollection(),
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        private static readonly HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };

        private static readonly HttpClient ApiClient = new()
        {
            BaseAddress = new Uri("http://win.nomiss.net"),
        };

        private static readonly HttpClient SamlClient = new()
        {
            BaseAddress = new Uri("http://win.nomiss.net"),
        };


        public static string ApiEndpoint
        {
            get => ApiClient.BaseAddress.ToString();
            set
            {
                ApiClient.BaseAddress = new Uri(value);
            }
        }

        public static string SamlEndpoint
        {
            get => SamlClient.BaseAddress.ToString();
            set
            {
                SamlClient.BaseAddress = new Uri(value);
            }
        }

        public static string ApiRegisterPath = "/api/register";
        public static string ApiUpdatePath = "/api/update";
        public static string ApiLoginPath = "/api/login";

        public static string SamlLoginPath = "/api/test/login";
        
        public static string AppCallbackUri = "io.identitymodel.native://callback/?user=s2112";

        private static string Request(string accessToken, string requestPath, BaseRequest requestStruct)
        {
            var payload = JsonSerializer.Serialize(requestStruct);
            Console.WriteLine($"Request: {payload}");

            // HTTPリクエスト送信
            var request = new HttpRequestMessage(HttpMethod.Post, requestPath);
            HttpResponseMessage response = ApiClient.PostAsync(requestPath, new StringContent(payload, Encoding.UTF8, "application/json")).Result;


            if(false && response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Request failed: {response.StatusCode}");
            }

            string responseString = response.Content.ReadAsStringAsync().Result;

            Uri requestUri = new Uri(new Uri(ApiEndpoint), requestPath);
            string requestUrlString = requestUri.ToString();
            string logMessage = $"リクエスト: POST {requestUrlString}\nボディ: {payload}\n\nレスポンスステータス: {response.StatusCode}\nレスポンスボディ: {responseString}";
            DebugBox.Load().LastText = logMessage;


            var dummyResponse = $"{{\"status\":\"ok\",\"response_data\": {{\"access_token\": \"abcvgjsdfghdsadsa\"}}}}";
            return dummyResponse;
            // return response.Content.ReadAsStringAsync().Result;
        }



        /// <summary>
        /// デバイスを登録する
        /// accesstokenを取得する
        /// </summary>
        /// <returns>RegisterResponse</returns>
        public static RegisterResponse RegisterDevice(Device device)
        {
            RegisterRequest req = new(device.DeviceId, device.Hostname);
            var response = Request("", ApiRegisterPath, req);
            RegisterResponse res = JsonSerializer.Deserialize<RegisterResponse>(response);
            return res;
        }

        /// <summary>
        /// デバイスIDとアクセストークンを利用してデバイス情報を更新する
        /// </summary>
        /// 
        public static UpdateResponse UpdateDevice(Device device)
        {
            UpdateRequest req = new(device.AccessToken, device.Username);
            var response = Request(device.AccessToken, ApiUpdatePath, req);
            return JsonSerializer.Deserialize<UpdateResponse>(response);
        }

        /// <summary>
        /// ログインして紐づけを行う
        /// localhostを使うのでWindowsのみ対応している
        /// </summary>
        /// 
        public static async Task<LoginResponse> LoginDevice(Device device)
        {
            // SAML認証を行う
            // Todo：SAMLで取れたユーザー名か管理サーバで取れたユーザー名のどちらを利用する？
            // OpenBrowser("http://www.ipentec.com");
            // Request(device.AccessToken, "/api/saml_listener", @"{""type"": ""アサーションの送信""}");
            //string userId = await SamlFlow.SAMLLoginAsync();
            string oneTimeToken = "testtoken";

            // ブラウザを起動→HTTPリスナを起動
            await StartSamlLoginAsync(oneTimeToken);

            // HTTPリスナ待ち→userの取得→返り
            string userId = await ReceiveSamlLoginAsync(oneTimeToken);

            //string userId = "test_user";

            // 取得したユーザー名を送信する
            LoginRequest req = new(device.AccessToken, userId);
            //var response = Request(device.AccessToken, "/api/update", $"{{\"type\": \"update\", \"request_data\": {{ \"username\": \"{device.Username}\"}} }}");
            var response = Request(device.AccessToken, ApiLoginPath, req);

            LoginResponse loginResponse = JsonSerializer.Deserialize<LoginResponse>(response);
            loginResponse.response_data.username = userId;
            return loginResponse;
        }

        /// <summary>
        /// Saml SP initiated loginを開始する
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> StartSamlLoginAsync(string token)
        {
            try
            {
                BrowserLaunchOptions options = new BrowserLaunchOptions()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Show,
                    PreferredToolbarColor = Colors.Violet,
                    PreferredControlColor = Colors.SandyBrown
                };

                await Browser.Default.OpenAsync(Model.Device.LoginUri, options);
            }
            catch (Exception ex)
            {
                // An unexpected error occurred. No browser may be installed on the device.
                return false;
            }
            return true;
        }

        /// <summary>
        /// Windowsでは動作しない
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SamlLoginAsync()
        {
            try
            {
                WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                new WebAuthenticatorOptions()
                {
                    Url = new Uri("http://win.nomiss.net"),
                    CallbackUrl = new Uri("io.identitymodel.native://callback"),
                    PrefersEphemeralWebBrowserSession = true,
                });
                var res = authResult;
                string accessToken = authResult?.AccessToken;

                // Do something with the token
            }
            catch (TaskCanceledException e)
            {
                // Use stopped auth
                return false;
            }
            return true;
        }

        public static async Task<string> ReceiveSamlLoginAsync(string token = "")
        {
            // HTTPリスナを待機
            CallbackData data = await CallbackAPI.StartServer();
 
            string requestToken = data.Query.Get("token");
            string requestUser = data.Query.Get("user");

            return requestUser;
        }
    }
}
