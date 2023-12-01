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

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // バグの可能性が高い
                throw new ServerUnavailableException(response.Content.ReadAsStringAsync().Result);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new UnexpectedResponseException($"`{message.RequestUri.ToString()}`で予期しないステータスコード`{response.StatusCode}`が返されました");
            }

            return response;
        }

        public string RequestString(HttpRequestMessage message)
        {
            HttpResponseMessage res = Request(message);
            return  res.Content.ReadAsStringAsync().Result;
            
        }
    }
}