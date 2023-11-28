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
//using Newtonsoft.Json;

namespace Rudeus.Model
{
 
    /// <summary>
    /// 管理サーバとREST APTで通信する、モデルのプロパティを読み取るが書き込みはしない
    /// </summary>
    internal class RemoteAPI
    {

        // クライアント証明書を取得
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


        public static readonly string SamlLoginUrl = Constants.SamlLoginUrl;
        
        public static readonly string ApiEndpointWithCert = Constants.ApiEndpointWithCert;
        public static readonly string ApiEndpointWithoutCert = Constants.ApiEndpointWithoutCert;


        public static string ApiRegisterPath = Constants.ApiRegisterPath;
        public static string ApiUpdatePath = Constants.ApiUpdatePath;
        public static string ApiLoginPath = Constants.ApiLoginPath;
        public static string ApiUpdateMetadataPath = Constants.ApiUpdateMetadataPath;

        // カスタムURIスキームで起動する場合の設定
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        // POST送信メソッド
        private static string PostRequest(string? accessToken, string requestPath, string payload)
        {
            return Request(accessToken, requestPath, payload, HttpMethod.Post);
        }

        // GET送信メソッド
        private static string GetRequest(string? accessToken, string requestPath, string? payload=null)
        {
            return Request(accessToken, requestPath, payload, HttpMethod.Get);
        }


        // HTTP汎用送信メソッド
        private static string Request(string? accessToken, string requestPath, string? payload, HttpMethod method)
        {
            Console.WriteLine($"Request: {payload}");


            // HTTPリクエスト作成
            HttpRequestMessage request = new (method, requestPath);
            
            // ヘッダー付与
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Content-Type", "application/json");

            // トークンが存在する場合はヘッダーに付与
            if(accessToken != null) 
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            // JSON形式payloadがある場合
            if(payload != null)
            {
                // BodyにJSONをセット
                request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            }

            // リクエスト送信
            // HttpResponseMessage response = NoCertApiClient.PostAsync(requestPath, new StringContent(payload, Encoding.UTF8, "application/json")).Result;
            // HttpResponseMessage response = NoCertApiClient.SendAsync(request).Result;
            HttpResponseMessage response;

            // Todo: 証明書付きAPIエンドポイントと無しの区別を付けてメソッドを分離
            try
            {
                response = RequestEndpoints(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
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

        // HttpRequestをConstants.forceClientCertAuthに応じて送信する
        private static HttpResponseMessage RequestEndpoints(HttpRequestMessage request)
        {
            HttpResponseMessage? response = null;
            if (Constants.forceClientCertAuth) 
            {
                try
                {
                    // 証明書付きAPIエンドポイントにリクエスト
                    response = CliCertApiClient.SendAsync(request).Result;
                }
                catch 
                {
                    response = null;
                }
            }

            if(response == null)
            {
                try
                {
                    // 証明書無しAPIエンドポイントにフォールバック
                    response = NoCertApiClient.SendAsync(request).Result;
                }
                catch
                {
                    throw;
                }
            }
            return response ?? throw new Exception("Unknown exception");
        }


        /// <summary>
        /// デバイスを登録する
        /// accesstokenを取得する
        /// </summary>
        /// <returns>RegisterResponse</returns>
        public static RegisterResponse RegisterDevice(string deviceId, string hostname)
        {
            // TODO: manage_idは最初に取得する
            RegisterRequest req = new(deviceId, hostname, hostname);
            var payload = JsonSerializer.Serialize(req, RegisterRequestContext.Default.RegisterRequest);
            var response = PostRequest(null, ApiRegisterPath, payload);
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
        public static UpdateResponse UpdateDevice(string accessToken, string? hostname=null)
        {
            UpdateRequest req = new(hostname);
            var payload = JsonSerializer.Serialize(req, UpdateRequestContext.Default.UpdateRequest);

            var response = PostRequest(accessToken, ApiUpdatePath, payload);
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
            var response = PostRequest(accessToken, ApiLoginPath, payload);

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

        public static UpdateMetadataResponse GetUpdateMetadata(string accessToken)
        {
            //return new UpdateMetadataResponse(Constants.DummyVersion, Constants.DummyUpdateUrl);
            
            //var response = GetRequest(null, ApiUpdateMetadataPath);
            var response = PostRequest(accessToken, ApiUpdateMetadataPath, JsonSerializer.Serialize(new EmptyRequest(), EmptyRequestContext.Default.EmptyRequest));
            try
            {
                var con = UpdateMetadataResponseContext.Default.UpdateMetadataResponse;
                var jsonResponse = JsonSerializer.Deserialize(response, con);
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



        // ToDo
        public static bool IsAccessTokenAvailable(string accessToken)
        {
            try
            {
                GetUpdateMetadata(accessToken);
            }
            catch
            {
                return false;
            }

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
    }
}
