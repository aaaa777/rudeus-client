// Requestクラスはクラス定義がインターフェースのような役割なので、共通インターフェースは小さくしてある

namespace Rudeus.API.Request
{
    public interface IRequest
    {
        string? type { get; }
    }

}