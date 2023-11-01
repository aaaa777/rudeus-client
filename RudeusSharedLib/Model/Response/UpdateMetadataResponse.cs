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

        public UpdateMetadataResponse(string latestVersion, string url) 
        {
            response_data = new UpdateMetadataResponseData(latestVersion, url);
        }

    }

    internal class UpdateMetadataResponseData
    {
        [JsonPropertyName(nameof(stable_version))]
        public string stable_version { get; set; }

        [JsonPropertyName(nameof(stable_zip_url))]
        public string stable_zip_url { get; set; }

        public UpdateMetadataResponseData(string? stableVersion, string? stableZipUrl)
        {
            this.stable_version = stableVersion;
            this.stable_zip_url = stableZipUrl;
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateMetadataResponse))]
    internal partial class UpdateMetadataResponseContext : JsonSerializerContext
    {
    }
}
