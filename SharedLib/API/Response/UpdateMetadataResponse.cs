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
    public class UpdateMetadataResponse : IResponse
    {
        [JsonPropertyName(nameof(status))]
        public string status { get; set; }

        [JsonPropertyName(nameof(response_data))]
        public UpdateMetadataResponseData response_data { get; set; }

        // JsonSerializerでparameterless constructorが必要
        // https://stackoverflow.com/questions/72000907/each-parameter-in-the-deserialization-constructor-on-type-must-bind-to-an-object
        public UpdateMetadataResponse() { response_data = new UpdateMetadataResponseData(); }
    }

    public class UpdateMetadataResponseData
    {
        [JsonPropertyName(nameof(Application))]
        public UpdateMetadataChannels? Application { get; set; }

        [JsonPropertyName(nameof(Command))]
        public UpdateMetadataChannels? Command { get; set; }

        public UpdateMetadataResponseData() { }
    }

    public class UpdateMetadataChannels
    {
        [JsonPropertyName(nameof(stable))]
        public UpdateMetadataDetail? stable { get; set; }
    }

    public class UpdateMetadataDetail
    {
        [JsonPropertyName(nameof(version))]
        public string version { get; set; }

        [JsonPropertyName(nameof(download_url))]
        public string download_url { get; set; }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateMetadataResponse))]
    internal partial class UpdateMetadataResponseContext : JsonSerializerContext
    {
    }
}
