using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.API.Request
{
    public class UpdateLabelIdRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "device_update";

        [JsonPropertyName(nameof(request_data))]
        public UpdateLabelIdRequestData request_data { get; set; }

        public UpdateLabelIdRequest(string labelId) 
        {
            request_data = new(labelId);
        }

    }
    public class UpdateLabelIdRequestData
    {

        [JsonPropertyName(nameof(label_id))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? label_id { get; set; }

        public UpdateLabelIdRequestData() { }

        public UpdateLabelIdRequestData(string? labelId)
        {
            this.label_id = labelId;
        }
    }
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(UpdateLabelIdRequest))]
    internal partial class UpdateLabelIdRequestContext : JsonSerializerContext
    {
    }
}
