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
using SharedLib.Exceptions;
//using Newtonsoft.Json;

namespace Rudeus.API
{

    /// <summary>
    /// 管理サーバとREST APTで通信する、モデルのプロパティを読み取るが書き込みはしない。
    /// TODO: インスタンス化してスタブ利用可にする
    /// </summary>
    public class RemoteAPI
    {

        // クライアント証明書を取得
        //private static X509Certificate2? ApiCertificate { get { return LocalCertificate.GetInstance().GetCertificate("manager.nomiss.net"); } }

        public static IRequestClient RequestClient { get; set; } = new RequestClient(Constants.ApiEndpointWithoutCert);

        public static readonly string SamlLoginUrl = Constants.SamlLoginUrl;

        public static string ApiCheckStatusPath { get; } = Constants.ApiCheckStatusPath;
        public static string ApiRegisterPath { get; } = Constants.ApiRegisterPath;
        public static string ApiUpdatePath { get; } = Constants.ApiUpdatePath;
        public static string ApiLoginPath { get; } = Constants.ApiLoginPath;
        public static string ApiUpdateMetadataPath { get; } = Constants.ApiUpdateMetadataPath;
        public static string ApiSendInstalledAppsPath { get; } = Constants.ApiSendInstalledAppsPath;
        public static string ApiUpdateMacAddressPath { get; } = Constants.ApiUpdateMacAddressPath;
        public static string ApiUpdateLabelIdPath { get; } = Constants.ApiUpdateLabelIdPath;

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
            // リクエスト送信
            HttpRequestMessage request = BuildHttpRequestMessage(accessToken, requestPath, payload, method);

            //HttpResponseMessage response;

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
            //Request.Headers.Add("Content-Type", "application/json");

            // トークンが存在する場合はヘッダーに付与
            if (accessToken != null && accessToken != "")
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            // BodyにJSONをセット
            request.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            

            return request;
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
                throw new UnexpectedResponseException("JSONSerializer return null");
            }
            catch
            {
                // JSONフォーマットが違った場合
                throw new UnexpectedResponseException("Server response invalid json");
            }
        }

        /// <summary>
        /// デバイスIDとアクセストークンを利用してデバイス情報を更新する
        /// </summary>
        /// 
        public static UpdateResponse UpdateDevice(
            string accessToken,
            string? hostname = null,
            string? spec = null,
            string? cpu_name = null,
            int? memory = null,
            int? c_drive = null,
            string? os = null,
            string? os_version = null,
            bool? withsecure = null,
            string? label_id = null
        )
        {
            UpdateRequestData data = new()
            {
                hostname = hostname,
                spec = spec,
                cpu_name = cpu_name,
                memory = memory,
                c_drive = c_drive,
                os = os,
                os_version = os_version,
                withsecure = withsecure
            };

            UpdateRequest req = new(data);
            return UpdateDevice(accessToken, req);
        }

        public static UpdateResponse UpdateDevice(string accessToken, UpdateRequest req) 
        { 
            
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
                throw new UnexpectedResponseException("JSONSerializer return null");
            } 
            catch
            {
                // JSONフォーマットが違った場合
                throw;
            }
            
        }


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
                throw new UnexpectedResponseException("JSONSerializer return null");
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
                throw new UnexpectedResponseException("JSONSerializer return null");
            }
            catch
            {
                // JSONフォーマットが違った場合
                throw new UnexpectedResponseException("server error");
            }
        }

        public static UpdateMetadataResponse GetUpdateMetadata(string accessToken)
        {
            //return new UpdateMetadataResponse(Constants.DummyVersion, Constants.DummyUpdateUrl);
            
            //var response = GetRequest(null, ApiUpdateMetadataPath);
            var response = GetRequest(accessToken, ApiUpdateMetadataPath);
            try
            {
                var con = UpdateMetadataResponseContext.Default.UpdateMetadataResponse;
                var jsonResponse = JsonSerializer.Deserialize(response, con);
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                throw new UnexpectedResponseException("JSONSerializer return null");
            }
            catch
            {
                // JSONフォーマットが違った場合
                throw;
            }
        }


        // Macアドレスの追加送信
        public static BaseResponse UpdateMacAddress(string accessToken, UpdateMacRequest req)
        {
            var payload = JsonSerializer.Serialize(req, UpdateMacRequestContext.Default.UpdateMacRequest);
            var response = PostRequest(accessToken, ApiUpdateMacAddressPath, payload);

            try
            {
                var con = BaseResponseContext.Default.BaseResponse;
                var jsonResponse = JsonSerializer.Deserialize(response, con);
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                throw new UnexpectedResponseException("JSONSerializer return null");
            }
            catch
            {
                // JSONフォーマットが違った場合
                throw;
            }
        }

        ///<summary>
        /// ラベルIDを登録する
        /// </summary>
        public static BaseResponse UpdateLabelId(string accessToken, string labelId)
        {
            var req = new UpdateLabelIdRequest(labelId);
            var payload = JsonSerializer.Serialize(req, UpdateLabelIdRequestContext.Default.UpdateLabelIdRequest);
            var response = PostRequest(accessToken, ApiUpdateLabelIdPath, payload);

            try
            {
                var con = BaseResponseContext.Default.BaseResponse;
                var jsonResponse = JsonSerializer.Deserialize(response, con);
                if (jsonResponse != null)
                {
                    return jsonResponse;
                }
                throw new UnexpectedResponseException("JSONSerializer return null");
            }
            catch
            {
                // JSONフォーマットが違った場合
                throw;
            }
        }


        /// <summary>
        /// アクセストークンが有効かどうかを確認する
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
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

            string requestUser = data.Query?.Get("mac_address") ?? throw new UnexpectedResponseException("invaild Request received");


            return requestUser;
        }
    }
}
