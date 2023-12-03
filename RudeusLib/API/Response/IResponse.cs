// Requestクラスはクラス定義がインターフェースのような役割なので、共通インターフェースは小さくしてある

namespace Rudeus.API.Response
{
    internal interface IResponse
    {
        string status { get; set; }
    }
}