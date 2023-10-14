﻿using Rudeus.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable IDE1006 // 命名スタイル
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
namespace Rudeus.Model.Response
{
    internal class RegisterResponse : BaseResponse
    {
        [JsonPropertyName(nameof(response_data))]
        public RegisterResponseData response_data { get; set; }
    }

    internal class RegisterResponseData
    {
        [JsonPropertyName(nameof(access_token))]
        public string? access_token { get; set; }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(RegisterResponse))]
    internal partial class RegisterResponseContext : JsonSerializerContext
    {
    }
}
