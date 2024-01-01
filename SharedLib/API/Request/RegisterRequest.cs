using Rudeus.API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rudeus.API.Request
{
    public class RegisterRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "device_initialize";

        [JsonPropertyName(nameof(request_data))]
        public RegisterRequestData request_data { get; set; }

        public RegisterRequest(string deviceId, string hostname, string manageId)
        {
            request_data = new RegisterRequestData(deviceId, hostname, manageId);
        }

        public RegisterRequest() { request_data = new RegisterRequestData(); }
    }
    public class RegisterRequestData
    {

        [JsonPropertyName(nameof(product_id))]
        public string? product_id { get; set; }

        [JsonPropertyName(nameof(product_name))]
        public string? product_name { get; set; }


        public RegisterRequestData(string deviceId, string hostname, string manageId)
        {
            product_id = deviceId;
            product_name = "Rudeus Test Client";
        }

        public RegisterRequestData() { }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(RegisterRequest))]
    internal partial class RegisterRequestContext : JsonSerializerContext
    {
    }

}
