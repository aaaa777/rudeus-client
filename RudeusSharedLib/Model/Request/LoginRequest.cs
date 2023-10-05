using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Request
{
    internal class LoginRequest : BaseRequest
    {
        public LoginRequestData request_data { get; set; }
        public string access_token { get; set; }

        public LoginRequest(string accessToken, string username)
        {
            type = "user_login";
            access_token = accessToken;
            request_data = new(username);
        }
    }
    internal class LoginRequestData
    {
        public string user_id { get; set; }

        public LoginRequestData(string username)
        {
            this.user_id = username;
        }
    }
}
