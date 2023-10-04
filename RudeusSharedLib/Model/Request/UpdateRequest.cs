using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Request
{
    internal class UpdateRequest : BaseRequest
    {
        public UpdateRequestData request_data { get; set; }
        public string access_token { get; set; }

        public UpdateRequest(string access_token, string username=null, string hostname=null)
        {
            type = "device_update";
            this.access_token = access_token;
            request_data = new(username, hostname);
        }
    }
    internal class UpdateRequestData
    {
        public string username { get; set; }
        public string hostname { get; set; }

        public UpdateRequestData(string username, string hostname)
        {
            this.username = username;
            this.hostname = hostname;
        }
    }
}
