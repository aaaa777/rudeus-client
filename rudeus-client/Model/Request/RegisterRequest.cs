using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Request
{
    internal class RegisterRequest : BaseRequest
    {
        public RegisterRequestData RequestData { get; set; }

        public RegisterRequest(string deviceId, string deviceName)
        {
            Type = "register";
            RequestData = new(deviceId, deviceName);
        }
    }
    internal class RegisterRequestData
    {
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }


        public RegisterRequestData(string deviceId, string deviceName)
        {
            DeviceId = deviceId;
            DeviceName = deviceName;
        }
    }
}
