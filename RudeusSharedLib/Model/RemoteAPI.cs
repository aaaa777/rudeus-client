using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.Diagnostics;
using System.Net;

using System.Text.Json;
using Rudeus.Model.Response;
using Rudeus.Model.Request;

using System.Runtime.ConstrainedExecution;
using System.Collections.Specialized;




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



        public static string ApiEndpoint { get; set; } = "https://manager.nomiss.net/";


        public static string ApiRegisterPath = "/api/device_initialize";
        public static string ApiUpdatePath = "/api/device_update";
        public static string ApiLoginPath = "/api/user_login";
        
        // カスタムURIスキームで起動する場合の設定
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        private static string Request(string accessToken, string requestPath, string payload)
        {
            Console.WriteLine($"Request: {payload}");


            // HTTPリクエスト作成
            HttpRequestMessage request = new (HttpMethod.Post, requestPath);
            
            // トークンが存在する場合はヘッダーに付与
            if(accessToken != null) 
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            // BodyにJSONをセット
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            // リクエスト送信
            // HttpResponseMessage response = ApiClient.PostAsync(requestPath, new StringContent(payload, Encoding.UTF8, "application/json")).Result;
            HttpResponseMessage response = ApiClient.SendAsync(request).Result;


            // ToDo: サーバサイドエラーの例外処理
            // APIリクエスト失敗時の例外だが、無効化してある
            if(false && response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Request failed: {response.StatusCode}");
            }

            // レスポンスボディを取得
            string responseString = response.Content.ReadAsStringAsync().Result;

            // DebugBoxのデータバインドを通してウィンドウに表示させる
            Uri requestUri = new (new Uri(ApiEndpoint), requestPath);
            string requestUrlString = requestUri.ToString();
            string accessTokenHeader = "";
            if(accessToken != null)
            {
                accessTokenHeader = $"Authorization: Bearer {accessToken}";
            }
            string logMessage = $"リクエスト: POST {requestUrlString}\nリクエストヘッダ：\"{accessTokenHeader}\"\nボディ: {payload}\n\nレスポンスステータス: {response.StatusCode}\nレスポンスボディ: {responseString}";
            DebugBox.Load().LastText = logMessage;

            return responseString;

            // レスポンス内容にかかわらずJSONのStringを返す
            var dummyResponse = $"{{\"status\":\"ok\",\"response_data\": {{\"access_token\": \"abcvgjsdfghdsadsa\"}}}}";
            return dummyResponse;
        }



        /// <summary>
        /// デバイスを登録する
        /// accesstokenを取得する
        /// </summary>
        /// <returns>RegisterResponse</returns>
        public static RegisterResponse RegisterDevice(string deviceId, string hostname)
        {
            RegisterRequest req = new(deviceId, hostname);
            var payload = JsonSerializer.Serialize(req);
            var response = Request(null, ApiRegisterPath, payload);
            try
            {
                return JsonSerializer.Deserialize<RegisterResponse>(response);
            } catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// デバイスIDとアクセストークンを利用してデバイス情報を更新する
        /// </summary>
        /// 
        public static UpdateResponse UpdateDevice(string accessToken, string username)
        {
            UpdateRequest req = new(accessToken, username);
            var payload = JsonSerializer.Serialize(req);
            var response = Request(accessToken, ApiUpdatePath, payload);
            try
            {
                return JsonSerializer.Deserialize<UpdateResponse>(response);
            } catch (Exception e)
            {
                throw;
            }
            
        }

        /// <summary>
        /// ログインして紐づけを行ったUserIdを取得
        /// localhostを使うのでWindowsのみ対応している
        /// </summary>
        /// 
        public static async Task<LoginResponse> LoginDevice(string accessToken)
        {
            // SAML認証を行う
            // 管理サーバがSPとなり、アプリにユーザ名を渡して管理サーバに戻す
            
            // 一時トークン
            string oneTimeToken = "testtoken";

            // ブラウザを起動
            // RemoteAPIの管轄外になった
            //await StartSamlLoginAsync(oneTimeToken);

            // HTTPリスナ起動→userの取得→返り
            string userId = await ReceiveSamlLoginAsync(oneTimeToken);
            

            // 取得したユーザー名を送信する
            LoginRequest req = new(accessToken, userId);
            var payload = JsonSerializer.Serialize(req);
            var response = Request(accessToken, ApiLoginPath, payload);

            try
            {
                // レスポンスをパースしUserIdを取得
                LoginResponse loginResponse = JsonSerializer.Deserialize<LoginResponse>(response);
                loginResponse.response_data.username = userId;
                return loginResponse;
            } catch (Exception ex)
            {
                throw;
            }
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
        /*
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
        */
    }
}
