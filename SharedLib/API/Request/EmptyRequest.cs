using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.API.Request
{
    public class EmptyRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; } = "empty_request";

        [JsonPropertyName(nameof(request_data))]
        public EmptyRequestData request_data { get; set; }

        public EmptyRequest()
        {
            request_data = new();
        }
    }
    internal class EmptyRequestData
    {
        public EmptyRequestData() { }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(EmptyRequest))]
    internal partial class EmptyRequestContext : JsonSerializerContext
    {
    }
}
