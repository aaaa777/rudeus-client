using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.Model.Request
{
    internal class UpdateRequest : BaseRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "device_update";

        [JsonPropertyName(nameof(request_data))]
        public UpdateRequestData request_data { get; set; }

        public UpdateRequest(string? hostname=null)
        {
            request_data = new(hostname);
        }
    }
    internal class UpdateRequestData
    {
        [JsonPropertyName(nameof(hostname))]
        public string? hostname { get; set; }

        public UpdateRequestData(string? hostname)
        {
            this.hostname = hostname;
        }
    }
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateRequest))]
    internal partial class UpdateRequestContext : JsonSerializerContext
    {
    }
}
