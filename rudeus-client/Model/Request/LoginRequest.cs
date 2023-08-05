﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Request
{
    internal class LoginRequest : BaseRequest
    {
        public LoginRequestData request_data { get; set; }
        public string access_token { get; set; }

        public LoginRequest(string accessToken, string username)
        {
            type = "login";
            access_token = accessToken;
            request_data = new(username);
        }
    }
    internal class LoginRequestData
    {
        public string username { get; set; }

        public LoginRequestData(string username)
        {
            this.username = username;
        }
    }
}
