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

        public UpdateRequest(string hostname=null)
        {
            type = "device_update";
            request_data = new(hostname);
        }
    }
    internal class UpdateRequestData
    {
        public string username { get; set; }
        public string hostname { get; set; }

        public UpdateRequestData(string hostname)
        {
            this.hostname = hostname;
        }
    }
}
