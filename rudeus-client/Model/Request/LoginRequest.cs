using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Request
{
    internal class LoginRequest : BaseRequest
    {
        public LoginRequestData RequestData { get; set; }
        public string AccessToken { get; set; }

        public LoginRequest(string accessToken, string username)
        {
            Type = "login";
            AccessToken = accessToken;
            RequestData = new(username);
        }
    }
    internal class LoginRequestData
    {
        public string Username { get; set; }

        public LoginRequestData(string username)
        {
            Username = username;
        }
    }
}
