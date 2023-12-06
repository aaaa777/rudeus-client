using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// Sample code
#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.API.Request
{
    public class UpdateMacRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; }

        [JsonPropertyName(nameof(request_data))]
        public UpdateMacRequestData request_data { get; set; }

        public UpdateMacRequest()
        {
            type = "device_mac_update";
            request_data = new();
        }
    }
    public class UpdateMacRequestData
    {
        [JsonPropertyName(nameof(mac_address))]
        public string mac_address { get; set; }

        [JsonPropertyName(nameof(interface_name))]
        public string interface_name { get; set; }
    }
    
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateMacRequest))]
    internal partial class UpdateMacRequestContext : JsonSerializerContext
    {
    }
}
