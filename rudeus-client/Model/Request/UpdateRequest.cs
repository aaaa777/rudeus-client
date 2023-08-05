using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Request
{
    internal class UpdateRequest : BaseRequest
    {
        public UpdateRequestData RequestData { get; set; }
        public string AccessToken { get; set; }

        public UpdateRequest(string access_token, string username)
        {
            Type = "update";
            AccessToken = access_token;
            RequestData = new(username);
        }
    }
    internal class UpdateRequestData
    {
        public string Username { get; set; }

        public UpdateRequestData(string username)
        {
            Username = username;
        }
    }
}
