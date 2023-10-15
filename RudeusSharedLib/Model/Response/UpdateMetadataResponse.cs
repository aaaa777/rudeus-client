using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
namespace Rudeus.Model.Response
{
    internal class UpdateMetadataResponse : BaseResponse
    {
        [JsonPropertyName(nameof(response_data))]
        public UpdateMetadataResponseData response_data { get; set; }

        public UpdateMetadataResponse(string latest_version, string url) 
        {
            response_data = new UpdateMetadataResponseData(latest_version, url);
        }

    }

    internal class UpdateMetadataResponseData
    {
        [JsonPropertyName(nameof(latest_version))]
        public string latest_version { get; set; }

        [JsonPropertyName(nameof(latest_raw_zip))]
        public string latest_raw_zip { get; set; }

        public UpdateMetadataResponseData(string? latest_version, string? latest_raw_zip)
        {
            this.latest_version = latest_version;
            this.latest_raw_zip = latest_raw_zip;
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateMetadataResponse))]
    internal partial class UpdateMetadataContext : JsonSerializerContext
    {
    }
}
