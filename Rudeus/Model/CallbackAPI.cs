using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model
{
    /// <summary>
    /// シンプルなGET形式のコールバックツール
    /// </summary>
    internal class CallbackAPI
    {
        public static readonly string CallbackPort = "11178";
        /// <summary>
        /// 簡易mutex
        /// </summary>
        private static bool mutex = false;
        public static HttpListener CallbackListener
        { 
            get => CallbackListener;
            set
            {
                CallbackListener.Close();
                CallbackListener = value;
            }
        }
        /// <summary>
        /// コールバックを叩く用のクライアント
        /// </summary>
        private static HttpClient SharedClient = new()
        {
            BaseAddress = new Uri($"http://localhost:{CallbackPort}")
        };

        /// <summary>
        /// イベントリスナをスタートする
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<CallbackData> StartServer(string responseMessage="Request OK, Close browser.")
        {
            if(mutex)
            {
                throw new Exception("port already used");
            }
            mutex = true;

            CallbackData data = new();
            HttpListener listener = new();
            try
            {
                listener.Prefixes.Add($"http://localhost:{CallbackPort}/");
                listener.Start();

                HttpListenerContext context = await listener.GetContextAsync();

                data.RequestText = new System.IO.StreamReader(context.Request.InputStream, System.Text.Encoding.UTF8).ReadToEnd();
                data.Query = System.Web.HttpUtility.ParseQueryString(context.Request.Url.Query);

                HttpListenerResponse res = context.Response;
                res.StatusCode = 200;
                res.ContentType = "text/html";
                res.ContentEncoding = System.Text.Encoding.UTF8;
                res.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(responseMessage));
                res.OutputStream.Close();

                if(data.Query.Get("stop_server") == "1") 
                {
                    throw new Exception("callback listener stopped");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                listener.Close();
                mutex = false;
            }
            return data;
        }

        /// <summary>
        /// StartServer()で起動したリスナを停止する
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void StopServer()
        {
            HttpResponseMessage res = SharedClient.GetAsync("/?stop_server=1").Result;
            if(res?.StatusCode != HttpStatusCode.OK) {
                throw new Exception("failed to stop server");
            }
            mutex = false;
        }

        /// <summary>
        /// 汎用コールバック送信メソッド
        /// </summary>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public static bool SendCallback(Uri targetUri)
        {
            NameValueCollection queryDict = System.Web.HttpUtility.ParseQueryString(targetUri.Query);
            HttpResponseMessage res = SharedClient.GetAsync($"/?user={queryDict.Get("user")}").Result;

            return res.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// SAML用コールバック送信メソッド
        /// 成功した場合trueが返る
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool SendSamlCallback(string userId, string token)
        {
            Uri targetUri = new($"http://localhost:{CallbackPort}/?user={userId}&token={token}");
            return SendCallback(targetUri);
        }

    }
    
    internal class CallbackData
    {
        public NameValueCollection Query { get; set; }
        public string RequestText { get; set; }
    }
}
