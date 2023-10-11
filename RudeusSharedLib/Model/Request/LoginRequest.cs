using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.Model.Request
{
    internal class LoginRequest : BaseRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "user_login";

        [JsonPropertyName(nameof(request_data))]
        public LoginRequestData request_data { get; set; }

        public LoginRequest(string username)
        {
            request_data = new(username);
        }
    }
    internal class LoginRequestData
    {
        [JsonPropertyName(nameof(user_id))]
        public string user_id { get; set; }

        public LoginRequestData(string userId)
        {
            user_id = userId;
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(LoginRequest))]
    internal partial class LoginRequestContext : JsonSerializerContext
    {
    }
}
