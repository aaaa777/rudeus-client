using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Request
{
    internal class RegisterRequest : BaseRequest
    {
        public RegisterRequestData request_data { get; set; }

        public RegisterRequest(string deviceId, string deviceName)
        {
            type = "register";
            request_data = new(deviceId, deviceName);
        }
    }
    internal class RegisterRequestData
    {
        public string device_id { get; set; }
        public string device_name { get; set; }


        public RegisterRequestData(string deviceId, string deviceName)
        {
            device_id = deviceId;
            device_name = deviceName;
        }
    }
}
