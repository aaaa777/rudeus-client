// HTTPリクエストのスタブ作成用RESTクライアント

using Rudeus.API.Request;
using System.Net;
using static Rudeus.API.Exceptions;

namespace Rudeus.API
{
    public class RequestClient : IRequestClient
    {
        HttpClient Client { get; set; }

        public RequestClient(string endpoint)
        {
            Client = new HttpClient()
            {
                BaseAddress = new Uri(endpoint)
            };
        }

        public HttpResponseMessage Request(HttpRequestMessage message)
        {
            HttpResponseMessage response;
            try
            {
                response = Client.SendAsync(message).Result;
            }
            catch (HttpRequestException e)
            {
                throw new ServerUnavailableException(e.Message);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new AccessTokenUnavailableException("アクセストークンが無効です");
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new UnexpectedResponseException("予期しないレスポンスが返されました");
            }

            return response;
        }

        public string RequestString(HttpRequestMessage message)
        {
            try
            {
                return Request(message).Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                throw new Exceptions.UnexpectedResponseException(e.Message);
            }
        }
    }
}