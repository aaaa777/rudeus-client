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
using Rudeus.API.Response;
using Rudeus.API.Request;
using Rudeus.Model;

using System.Runtime.ConstrainedExecution;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using Windows.Networking;
using static Rudeus.API.Exceptions;
//using Newtonsoft.Json;

namespace Rudeus.API
{
 
    /// <summary>
    /// 管理サーバとREST APTで通信する、モデルのプロパティを読み取るが書き込みはしない
    /// </summary>
    internal class RemoteAPI
    {

        // クライアント証明書を取得
        private static X509Certificate2? ApiCertificate { get { return LocalCertificate.GetInstance().GetCertificate("manager.nomiss.net"); } }

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
        public static IRequestClient RequestClient { get; set; } = new RequestClient(Constants.ApiEndpointWithoutCert);

        public static readonly string SamlLoginUrl = Constants.SamlLoginUrl;
        
        public static readonly string ApiEndpointWithCert = Constants.ApiEndpointWithCert;
        public static readonly string ApiEndpointWithoutCert = Constants.ApiEndpointWithoutCert;


        public static string ApiCheckStatusPath { get; } = Constants.ApiCheckStatusPath;
        public static string ApiRegisterPath { get; } = Constants.ApiRegisterPath;
        public static string ApiUpdatePath { get; } = Constants.ApiUpdatePath;
        public static string ApiLoginPath { get; } = Constants.ApiLoginPath;
        public static string ApiUpdateMetadataPath { get; } = Constants.ApiUpdateMetadataPath;
        public static string ApiSendInstalledAppsPath { get; } = Constants.ApiSendInstalledAppsPath;

        // カスタムURIスキームで起動する場合の設定
        public static string AppCallbackUri = "rudeus.client://callback/?user=s2112";

        // POST送信メソッド
        private static string PostRequest(string? accessToken, string requestPath, string payload)
        {
            return Request(accessToken, requestPath, payload, HttpMethod.Post);
        }

        // GET送信メソッド
        private static string GetRequest(string? accessToken, string requestPath, string payload="")
        {
            return Request(accessToken, requestPath, payload, HttpMethod.Get);
        }


        // HTTP汎用送信メソッド
        private static string Request(string? accessToken, string requestPath, string payload, HttpMethod method)
        {
            HttpRequestMessage request = BuildHttpRequestMessage(accessToken, requestPath, payload, method);

            // リクエスト送信
            HttpResponseMessage response;

            // Todo: 証明書付きAPIエンドポイントと無しの区別を付けてメソッドを分離
            //try
            //{
            //    response = RequestEndpoints(request);
            //}
            //catch (TimeoutException ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //    throw new ServerUnavailableException(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //    throw new UnexpectedResponseException(ex.Message);
            //}

            //response = RequestClient.Request(request);
            

            // ToDo: サーバサイドエラーの例外処理
            //if (response.StatusCode != HttpStatusCode.OK)
            //{
            //    throw new Exception($"Request failed: {response.StatusCode}");
            //}

            // レスポンスボディを取得
            //string responseString = response.Content.ReadAsStringAsync().Result;
            string responseString = RequestClient.RequestString(request);

            Console.WriteLine($"Response: {responseString}");

            return responseString;

        }

        public static HttpRequestMessage BuildHttpRequestMessage(string? accessToken, string requestPath, string payload, HttpMethod method)
        {
            Console.WriteLine($"Request: {payload}");


            // HTTPリクエスト作成
            HttpRequestMessage request = new(method, requestPath);

            // ヘッダー付与
            request.Headers.Add("Accept", "application/json");
            //request.Headers.Add("Content-Type", "application/json");

            // トークンが存在する場合はヘッダーに付与
            if (accessToken != null && accessToken != "")
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            // BodyにJSONをセット
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            

            return request;
        }

        // HttpRequestをConstants.forceClientCertAuthに応じて送信する
        private static HttpResponseMessage RequestEndpoints(HttpRequestMessage request)
        {
            HttpResponseMessage? response = null;

            // クライアント認証を強制する設定の場合
            if (!Constants.disableClientCertAuth) 
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

            if(response == null)
            {
                throw new Exception("Unknown exception");
            }

            if(response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Request failed: {response.StatusCode}");
            }

            return response;
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
#if (DEVELOPMENT)
            // デバッグ用のダミーレスポンス
            var response = "{\r\n\"status\":\"ok\",\r\n\"response_data\":{\r\n\"access_token\":\"debug|dummy_access_token\"\r\n}\r\n}";
#else
            var response = PostRequest(null, ApiRegisterPath, payload);
#endif
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

#if(DEVELOPMENT)
            var response = "{\r\n\"status\":\"ok\",\r\n\"push_data\":[{\"id\":\"1\",\"type\":\"notify_toast\",\"payload\":\"Developmentビルドのため、ダミーメッセージを表示しています。\"}]\r\n}";
#else
            var response = PostRequest(accessToken, ApiUpdatePath, payload);
#endif
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


/* プロジェクト 'RudeusBgForm' からのマージされていない変更
前:
        public static BaseResponse SendInstalledApps(string accessToken, List<InstalledApplication> apps)
後:
        public static BaseResponse SendInstalledApps(string accessToken, List<Model.ApplicationData> apps)
*/
        public static BaseResponse SendInstalledApps(string accessToken, List<ApplicationData> apps)
        {
            SendInstalledAppsRequest req = new(apps);
            var payload = JsonSerializer.Serialize(req, SendInstalledAppsRequestContext.Default.SendInstalledAppsRequest);

#if(DEVELOPMENT)
            var response = "{\r\n\"status\":\"ok\"}";
#else
            var response = PostRequest(accessToken, ApiSendInstalledAppsPath, payload);
#endif
            try
            {
                var jsonResponse = JsonSerializer.Deserialize(response, BaseResponseContext.Default.BaseResponse);
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

        // TODO: CallbackAPIに移動する
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

            // バリデーションはまだしない
            //if(!Utils.IsStudentMailAddress(userIdBySaml))
            //{
            //    throw new Exception("invalid user");
            //}

            string userId = Utils.ConcatStudentNumberFromMail(userIdBySaml);
            return userId;
        }

        public static LoginResponse LoginDevice(string accessToken, string userId) {

            // 取得したユーザー名を送信する
            LoginRequest req = new(userId);
            var payload = JsonSerializer.Serialize(req, LoginRequestContext.Default.LoginRequest);

#if (DEVELOPMENT)
            var response = "{\r\n\"status\":\"ok\"\r\n}";
#else
            var response = PostRequest(accessToken, ApiLoginPath, payload);
#endif
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

        public static bool IsRemoteReachable()
        {
            try
            {
                GetRequest(null, Constants.ApiCheckStatusPath);
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
