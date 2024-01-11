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
    public class LabelIdResponse : IResponse
    {
        [JsonPropertyName(nameof(status))]
        public string status { get; set; } = string.Empty;

        [JsonPropertyName(nameof(response_data))]
        public LoginResponseData response_data { get; set; }

    }

    public class LabelIdResponseData
    {
        [JsonPropertyName(nameof(label_id))]
        public string label_id { get; set; }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(LabelIdResponse))]
    internal partial class LabelIdResponseContext : JsonSerializerContext
    {
    }
}
