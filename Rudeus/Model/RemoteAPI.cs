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

        private static HttpClient _apiClient = new();
        private static HttpClient ApiClient
        {
            get
            {
                if (_apiClient.ToString() != ApiEndpoint)
                {
                    _apiClient = new()
                    {
                        BaseAddress = new Uri(ApiEndpoint),
                    };

                }
                return _apiClient;
            }
        }



        public static string ApiEndpoint { get; set; } = "http://win.nomiss.net/";


        public static string ApiRegisterPath = "/api/register";
        public static string ApiUpdatePath = "/api/update";
        public static string ApiLoginPath = "/api/login";
        
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        private static string Request(string accessToken, string requestPath, string payload)
        {
            Console.WriteLine($"Request: {payload}");

            // HTTPリクエスト送信
            var request = new HttpRequestMessage(HttpMethod.Post, requestPath);

            // ToDo: サーバサイドエラーの例外処理
            HttpResponseMessage response = ApiClient.PostAsync(requestPath, new StringContent(payload, Encoding.UTF8, "application/json")).Result;


            // APIリクエスト失敗時の例外だが、無効化してある
            if(false && response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Request failed: {response.StatusCode}");
            }

            // レスポンスボディを取得
            string responseString = response.Content.ReadAsStringAsync().Result;

            // DebugBoxのデータバインドを通してウィンドウに表示させる
            Uri requestUri = new Uri(new Uri(ApiEndpoint), requestPath);
            string requestUrlString = requestUri.ToString();
            string logMessage = $"リクエスト: POST {requestUrlString}\nボディ: {payload}\n\nレスポンスステータス: {response.StatusCode}\nレスポンスボディ: {responseString}";
            DebugBox.Load().LastText = logMessage;


            // レスポンス内容にかかわらずJSONのStringを返す
            var dummyResponse = $"{{\"status\":\"ok\",\"response_data\": {{\"access_token\": \"abcvgjsdfghdsadsa\"}}}}";
            return dummyResponse;
        }



        /// <summary>
        /// デバイスを登録する
        /// accesstokenを取得する
        /// </summary>
        /// <returns>RegisterResponse</returns>
        public static RegisterResponse RegisterDevice(Device device)
        {
            RegisterRequest req = new(device.DeviceId, device.Hostname);
            var payload = JsonSerializer.Serialize(req);
            var response = Request("", ApiRegisterPath, payload);
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
            var payload = JsonSerializer.Serialize(req);
            var response = Request(device.AccessToken, ApiUpdatePath, payload);
            return JsonSerializer.Deserialize<UpdateResponse>(response);
        }

        /// <summary>
        /// ログインして紐づけを行ったUserIdを取得
        /// localhostを使うのでWindowsのみ対応している
        /// </summary>
        /// 
        public static async Task<LoginResponse> LoginDevice(Device device)
        {
            // SAML認証を行う
            // 管理サーバがSPとなり、アプリにユーザ名を渡して管理サーバに戻す
            
            // 一時トークン
            string oneTimeToken = "testtoken";

            // ブラウザを起動
            await StartSamlLoginAsync(oneTimeToken);

            // HTTPリスナ起動→userの取得→返り
            string userId = await ReceiveSamlLoginAsync(oneTimeToken);

            // 取得したユーザー名を送信する
            LoginRequest req = new(device.AccessToken, userId);
            var payload = JsonSerializer.Serialize(req);
            var response = Request(device.AccessToken, ApiLoginPath, payload);

            // レスポンスをパースしUserIdを取得
            LoginResponse loginResponse = JsonSerializer.Deserialize<LoginResponse>(response);
            loginResponse.response_data.username = userId;
            return loginResponse;
        }

        /// <summary>
        /// Saml SP initiated loginを開始する
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> StartSamlLoginAsync(string token="")
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

        public static async Task<string> ReceiveSamlLoginAsync(string token="")
        {
            string responseText = @"
                <!DOCTYPE html><html><head><title>Authorization Successful</title><script>window.close();</script></head><body><h1>Authorization done! Close browser</h1></body></html>
            ";

            // HTTPリスナを待機
            CallbackData data = await CallbackAPI.StartServer(responseText);

            string requestToken = data.Query.Get("token");
            string requestUser = data.Query.Get("user_id");

            return requestUser;
        }

        /// <summary>
        /// WebAuthenticatorはWindowsでは動作しない
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SamlLoginWebAuthenticator()
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

    }
}
