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
    public class UpdateExecutedPushDataRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "executed_push_data_update";

        [JsonPropertyName(nameof(request_data))]
        public UpdateExecutedPushDataRequestData request_data { get; set; }

        public UpdateExecutedPushDataRequest()
        {
            request_data = new UpdateExecutedPushDataRequestData { };
        }
    }
    public class UpdateExecutedPushDataRequestData
    {
        [JsonPropertyName(nameof(queue_id))]
        public List<int> queue_id { get; set; }

        public UpdateExecutedPushDataRequestData()
        {
            queue_id = new List<int>();
        }
    }
    
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateExecutedPushDataRequest))]
    internal partial class UpdateExecutedPushDataRequestContext : JsonSerializerContext
    {
    }
}
