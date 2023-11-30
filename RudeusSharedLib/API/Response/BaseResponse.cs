using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.API.Response
{
    internal class BaseResponse : IResponse
    {
        [JsonPropertyName(nameof(status))]
        public string status { get; set; } = string.Empty;

        [JsonPropertyName(nameof(response_data))]
        public BaseResponse? response_data { get; set; }

        public BaseResponse()
        {
            status = "unknown";
        }

    }

    internal class BaseResponseData
    {
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(BaseResponse))]
    internal partial class BaseResponseContext : JsonSerializerContext
    {
    }
}
