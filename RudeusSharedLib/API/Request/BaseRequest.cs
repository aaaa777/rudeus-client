using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// Sample code
#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.API.Request
{
    internal class BaseRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string? type { get; set; }

        [JsonPropertyName(nameof(request_data))]
        public BaseRequestData? request_data { get; set; }
    }
    internal class BaseRequestData
    {
    }
    
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(BaseRequest))]
    internal partial class BaseRequestContext : JsonSerializerContext
    {
    }
}
