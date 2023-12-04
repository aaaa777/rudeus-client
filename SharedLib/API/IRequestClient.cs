// HTTPリクエストのスタブ作成用RESTクライアント

namespace Rudeus.API
{
    public interface IRequestClient
    {
        // 内部利用用
        HttpResponseMessage Request(HttpRequestMessage message);

        string RequestString(HttpRequestMessage message);
    }
}