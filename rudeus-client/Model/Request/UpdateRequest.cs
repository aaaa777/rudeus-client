using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Request
{
    internal class UpdateRequest : BaseRequest
    {
        public UpdateRequestData request_data { get; set; }
        public string access_token { get; set; }

        public UpdateRequest(string access_token, string username)
        {
            type = "update";
            this.access_token = access_token;
            request_data = new(username);
        }
    }
    internal class UpdateRequestData
    {
        public string username { get; set; }

        public UpdateRequestData(string username)
        {
            this.username = username;
        }
    }
}
