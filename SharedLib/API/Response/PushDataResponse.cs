using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
namespace Rudeus.API.Response
{
    public class PushDataResponse : IResponse
    {
        [JsonPropertyName(nameof(status))]
        public string status { get; set; } = string.Empty;

        [JsonPropertyName(nameof(response_data))]
        public GetPushDataResponseData response_data { get; set; }

    }

    public class GetPushDataResponseData
    {
        [JsonPropertyName(nameof(push_data))]
        public List<PushResponseData2> push_data { get; set; }
    }

    public class PushResponseData2
    {
        [JsonPropertyName(nameof(id))]
        public int id { get; set; }

        [JsonPropertyName(nameof(type))]
        public string type { get; set; }

        [JsonPropertyName(nameof(payload))]
        public string payload { get; set; }

        [JsonPropertyName(nameof(expected_send_at))]
        public string expected_send_at { get; set; }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(PushDataResponse))]
    internal partial class PushDataResponseContext : JsonSerializerContext
    {
    }
}
