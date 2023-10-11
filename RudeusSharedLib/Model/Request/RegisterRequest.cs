using Rudeus.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rudeus.Model.Request
{
    internal class RegisterRequest : BaseRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "device_initialize";

        [JsonPropertyName(nameof(request_data))]
        public RegisterRequestData request_data { get; set; }

        public RegisterRequest(string deviceId, string hostname)
        {
            request_data = new(deviceId, hostname);
        }
    }
    internal class RegisterRequestData
    {

        [JsonPropertyName(nameof(device_id))]
        public string? device_id { get; set; }

        [JsonPropertyName(nameof(hostname))]
        public string? hostname { get; set; }

        public RegisterRequestData(string deviceId, string hostname)
        {
            device_id = deviceId;
            this.hostname = hostname;
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(RegisterRequest))]
    internal partial class RegisterRequestContext : JsonSerializerContext
    {
    }

}
