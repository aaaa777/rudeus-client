using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.Model.Response
{
    internal class LoginResponse : BaseResponse
    {
        [JsonPropertyName(nameof(response_data))]
        public LoginResponseData? response_data { get; set; }
    }

    internal class LoginResponseData
    {
        [JsonPropertyName(nameof(username))]
        public string? username { get; set; }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(LoginResponse))]
    internal partial class LoginResponseContext : JsonSerializerContext
    {
    }
}
