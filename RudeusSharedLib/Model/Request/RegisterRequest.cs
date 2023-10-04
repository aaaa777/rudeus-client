using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Request
{
    internal class RegisterRequest : BaseRequest
    {
        public RegisterRequestData request_data { get; set; }

        public RegisterRequest(string deviceId, string hostname)
        {
            type = "device_initialize";
            request_data = new(deviceId, hostname);
        }
    }
    internal class RegisterRequestData
    {
        public string device_id { get; set; }
        public string hostname { get; set; }


        public RegisterRequestData(string deviceId, string hostname)
        {
            device_id = deviceId;
            this.hostname = hostname;
        }
    }
}
