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
using System.Diagnostics.CodeAnalysis;

namespace Rudeus.Model
{
 
    /// <summary>
    /// 管理サーバとREST APTで通信する、モデルのプロパティを読み取るが書き込みはしない
    /// </summary>
    internal class RemoteAPI
    {

        private static X509Certificate2? ApiCertificate { get { return CertificateAPI.GetCertificate("manager.nomiss.net"); } }

        /// <summary>
        /// Todo: クライアント証明書を追加する
        /// https://stackoverflow.com/questions/40014047/add-client-certificate-to-net-core-httpclient
        /// </summary>
        private static HttpClientHandler CliCertHandler
        {
            get
            {
                return new()
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    SslProtocols = SslProtocols.Tls12,
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
            }
        }


        private static HttpClient? _cliCertApiClient;

        // GetCliCertApiClientの方が良かったかも
        private static HttpClient CliCertApiClient
        {
            get
            {
                // 証明書ストアからクライアント証明書を取得
                HttpClientHandler hch = CliCertHandler;
                var cert = ApiCertificate ?? throw new Exception("certificate not found");

                // クライアント証明書をリクエストクライアントに追加
                hch.ClientCertificates.Add(cert);

                _cliCertApiClient = new(hch)
                {
                    BaseAddress = new Uri(ApiEndpointWithCert),
                };

                
                return _cliCertApiClient;
            }
        }

        private static HttpClient _apiClient = new();
        private static HttpClient NoCertApiClient
        {
            get
            {
                if (_apiClient.ToString() != ApiEndpointWithoutCert)
                {
                    _apiClient = new()
                    {
                        BaseAddress = new Uri(ApiEndpointWithoutCert),
                    };

                }
                return _apiClient;
            }
        }


        public static readonly string SamlLoginUrl = "https://win.nomiss.net/rudeus_login";
        
        public static readonly string ApiEndpointWithCert = "https://manager.nomiss.net/";
        public static readonly string ApiEndpointWithoutCert = "https://win.nomiss.net/";


        public static string ApiRegisterPath = "/api/device_initialize";
        public static string ApiUpdatePath = "/api/device_update";
        public static string ApiLoginPath = "/api/user_login";
        
        // カスタムURIスキームで起動する場合の設定
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        // POST送信メソッド
        private static string Request(string? accessToken, string requestPath, string payload)
        {
            Console.WriteLine($"Request: {payload}");


            // HTTPリクエスト作成
            HttpRequestMessage request = new (HttpMethod.Post, requestPath);
            
            // ヘッダー付与
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Content-Type", "application/json");

            // トークンが存在する場合はヘッダーに付与
            if(accessToken != null) 
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            // BodyにJSONをセット
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

            // リクエスト送信
            // HttpResponseMessage response = NoCertApiClient.PostAsync(requestPath, new StringContent(payload, Encoding.UTF8, "application/json")).Result;
            // HttpResponseMessage response = NoCertApiClient.SendAsync(request).Result;
            HttpResponseMessage response;
            try
            {
                // 証明書付きAPIエンドポイントにリクエスト
                response = CliCertApiClient.SendAsync(request).Result;
            }
            catch 
            {
                // 証明書無しAPIエンドポイントにフォールバック(例外補足無し)
                response = NoCertApiClient.SendAsync(request).Result;
            }


            // ToDo: サーバサイドエラーの例外処理
            if(response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Request failed: {response.StatusCode}");
            }

            // レスポンスボディを取得
            string responseString = response.Content.ReadAsStringAsync().Result;

            return responseString;
        }



        /// <summary>
        /// デバイスを登録する
        /// accesstokenを取得する
        /// </summary>
        /// <returns>RegisterResponse</returns>
        public static RegisterResponse RegisterDevice(string deviceId, string hostname)
        {
            RegisterRequest req = new(deviceId, hostname);
            var payload = JsonSerializer.Serialize(req, RegisterRequestContext.Default.RegisterRequest);
            var response = Request(null, ApiRegisterPath, payload);
            try
            {
                var jsonResponse = JsonSerializer.Deserialize(response, RegisterResponseContext.Default.RegisterResponse);
                if (jsonResponse != null) 
                { 
                    return jsonResponse;
                }
                throw new Exception("JSONSerializer return null");
            }
            catch
            {
                // JSONフォーマットが違った場合
                throw;
            }
        }

        /// <summary>
        /// デバイスIDとアクセストークンを利用してデバイス情報を更新する
        /// </summary>
        /// 
        public static UpdateResponse UpdateDevice(string accessToken, string hostname="", string username="")
        {
            UpdateRequest req = new(hostname);
            var payload = JsonSerializer.Serialize(req, UpdateRequestContext.Default.UpdateRequest);

            var response = Request(accessToken, ApiUpdatePath, payload);
            try
            {
                var jsonResponse = JsonSerializer.Deserialize(response, UpdateResponseContext.Default.UpdateResponse);
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                throw new Exception("JSONSerializer return null");
            } 
            catch
            {
                // JSONフォーマットが違った場合
                throw;
            }
            
        }

        /// <summary>
        /// ログインして紐づけを行ったUserIdを取得
        /// localhostを使うのでWindowsのみ対応している
        /// </summary>
        /// 
        public static async Task<string> ReceiveStudentIdAsync()
        {
            // SAML認証を行う
            // 管理サーバがSPとなり、アプリにユーザ名を渡して管理サーバに戻す

            // ブラウザを起動
            // RemoteAPIの管轄外になった
            //await StartSamlLoginAsync(oneTimeToken);

            // HTTPリスナ起動→userの取得→返り
            string userIdBySaml = await ReceiveSamlLoginAsync();
            // mockサーバ用の設定
            if (userIdBySaml == "jackson@example.com") {
                userIdBySaml = "s9999999@s.do-johodai.ac.jp";
            }
            string userId = Utils.ConcatStudentNumberFromMail(userIdBySaml);
            return userId;
        }

        public static LoginResponse LoginDevice(string accessToken, string userId) {

            // 取得したユーザー名を送信する
            LoginRequest req = new(userId);
            var payload = JsonSerializer.Serialize(req, LoginRequestContext.Default.LoginRequest);
            var response = Request(accessToken, ApiLoginPath, payload);

            try
            {
                // レスポンスをパースしUserIdを取得
                var jsonResponse = JsonSerializer.Deserialize(response, LoginResponseContext.Default.LoginResponse);
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                throw new Exception("JSONSerializer return null");
            }
            catch
            {
                // JSONフォーマットが違った場合
                throw new Exception("server error");
            }
        }


        public static bool IsAccessTokenAvailable(string accessToken)
        {
            return true;
        }

        private static async Task<string> ReceiveSamlLoginAsync()
        {
            string responseText = @"
                <!DOCTYPE html><html><head><title>Authorization Successful</title><script>window.close();</script></head><body><h1>Authorization done! Close browser</h1></body></html>
            ";

            // HTTPリスナを待機
            CallbackData data = await CallbackAPI.StartServer(responseText);

            string requestUser = data.Query?.Get("user_id") ?? throw new Exception("invaild request received");


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
