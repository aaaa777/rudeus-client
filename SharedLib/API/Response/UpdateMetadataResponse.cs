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

        [JsonPropertyName(nameof(request_data))]
        public UpdateMetadataResponseData request_data { get; set; }

        // JsonSerializerでparameterless constructorが必要
        // https://stackoverflow.com/questions/72000907/each-parameter-in-the-deserialization-constructor-on-type-must-bind-to-an-object
        public UpdateMetadataResponse() { }
        public UpdateMetadataResponse(string latestVersion, string url) 
        {
            request_data = new UpdateMetadataResponseData(latestVersion, url);
        }

    }

    public class UpdateMetadataResponseData
    {
        [JsonPropertyName(nameof(stable_version))]
        public string stable_version { get; set; }

        [JsonPropertyName(nameof(stable_zip_url))]
        public string stable_zip_url { get; set; }

        public UpdateMetadataResponseData() { }

        public UpdateMetadataResponseData(string stable_version, string stable_zip_url)
        {
            this.stable_version = stable_version;
            this.stable_zip_url = stable_zip_url;
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateMetadataResponse))]
    internal partial class UpdateMetadataResponseContext : JsonSerializerContext
    {
    }
}
