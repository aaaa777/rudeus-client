// HTTPリクエストのスタブ作成用RESTクライアント

namespace Rudeus.API
{
    public interface IRequestClient
    {
        HttpResponseMessage Request(HttpRequestMessage message);

        string RequestString(HttpRequestMessage message);
    }
}