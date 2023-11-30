using Rudeus.API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rudeus.API.Request
{
    internal class RegisterRequest : BaseRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "device_initialize";

        [JsonPropertyName(nameof(request_data))]
        public RegisterRequestData request_data { get; set; }

        public RegisterRequest(string deviceId, string hostname, string manageId)
        {
            request_data = new(deviceId, hostname, manageId);
        }
    }
    internal class RegisterRequestData
    {

        [JsonPropertyName(nameof(device_id))]
        public string? device_id { get; set; }

        [JsonPropertyName(nameof(hostname))]
        public string? hostname { get; set; }

        [JsonPropertyName(nameof(manage_id))]
        public string? manage_id { get; set; }

        [JsonPropertyName(nameof(product_name))]
        public string? product_name { get; set; }


        public RegisterRequestData(string deviceId, string hostname, string manageId)
        {
            device_id = deviceId;
            this.hostname = hostname;
            this.manage_id = manageId;
            this.product_name = "Rudeus Test Client";
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(RegisterRequest))]
    internal partial class RegisterRequestContext : JsonSerializerContext
    {
    }

}
