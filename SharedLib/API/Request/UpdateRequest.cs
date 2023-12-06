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

        public UpdateRequest(UpdateRequestData data)
        {
            request_data = data;
        }
    }
    public class UpdateRequestData
    {
        [JsonPropertyName(nameof(hostname))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? hostname { get; set; }

        [JsonPropertyName(nameof(spec))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? spec { get; set; }

        [JsonPropertyName(nameof(cpu_name))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? cpu_name { get; set; }

        [JsonPropertyName(nameof(memory))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? memory { get; set; }

        [JsonPropertyName(nameof(c_drive))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? c_drive { get; set; }

        [JsonPropertyName(nameof(os))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? os { get; set; }

        [JsonPropertyName(nameof(os_version))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? os_version { get; set; }

        [JsonPropertyName(nameof(withsecure))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? withsecure { get; set; }

        [JsonPropertyName(nameof(label_id))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? label_id { get; set; }

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
