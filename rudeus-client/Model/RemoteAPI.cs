using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;

using System.Text.Json;
using rudeus_client.Model.Response;

namespace rudeus_client.Model
{
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

        private static string Request(string accessToken, string requestPath, string payload)
        {
            Console.WriteLine($"Request: {payload}");


            var request = new HttpRequestMessage(HttpMethod.Post, requestPath);
            using HttpResponseMessage response = sharedClient.GetAsync($"todos/1").Result;
            var dummyResponse = @"{""Status"":""ok"",""ResponseData"": {""AccessToken"": ""abcvgjsdfgh""}}";
            return dummyResponse;
            return response.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// デバイスを登録する
        /// accesstokenを取得する
        /// </summary>
        /// <returns>RegisterResponse</returns>
        public static RegisterResponse RegisterDevice(Device device)
        {
            var response = Request("", "/api/register", JsonSerializer.Serialize(device));
            RegisterResponse res = JsonSerializer.Deserialize<RegisterResponse>(response);
            return res;
        }

        /// <summary>
        /// デバイスIDとアクセストークンを利用してデバイス情報を更新する
        /// </summary>
        /// 
        public static UpdateResponse UpdateDevice(Device device)
        {
            var response = Request(device.AccessToken, "/api/update", "{}");
            return JsonSerializer.Deserialize<UpdateResponse>(response);
        }

        /// <summary>
        /// ログインして紐づけを行う
        /// </summary>
        /// 
        public static LoginResponse LoginDevice(Device device)
        {
            var response = Request(device.AccessToken, "/api/login", "{}");
            return JsonSerializer.Deserialize<LoginResponse>(response);
        }
    }
}
