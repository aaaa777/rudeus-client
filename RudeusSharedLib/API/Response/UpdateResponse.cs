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
    internal class UpdateResponse : BaseResponse
    {
        [JsonPropertyName(nameof(response_data))]
        public UpdateResponseData response_data { get; set; }

        [JsonPropertyName(nameof(push_data))]
        public PushResponseData[] push_data { get; set; }
    }

    internal class UpdateResponseData
    {
        [JsonPropertyName(nameof(username))]
        public string? username { get; set; }
    }

    internal class PushResponseData
    {
        [JsonPropertyName(nameof(id))]
        public string? id { get; set; }

        [JsonPropertyName(nameof(type))]
        public string? type { get; set; }

        [JsonPropertyName(nameof(payload))]
        public string? payload { get; set; }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateResponse))]
    internal partial class UpdateResponseContext : JsonSerializerContext
    {
    }
}
