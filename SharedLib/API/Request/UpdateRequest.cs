using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.API.Request
{
    public class UpdateRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "device_update";

        [JsonPropertyName(nameof(request_data))]
        public UpdateRequestData request_data { get; set; }

        public UpdateRequest() 
        {
            request_data = new();
        }
        public UpdateRequest(string? hostname=null)
        {
            request_data = new(hostname);
        }
    }
    internal class UpdateRequestData
    {
        [JsonPropertyName(nameof(hostname))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? hostname { get; set; }

        [JsonPropertyName(nameof(spec))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? spec { get; set; }

        public UpdateRequestData() { }

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
