using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Response
{
    internal class RegisterResponse : BaseResponse
    {
        public RegisterResponseData response_data { get; set; }
    }

    internal class RegisterResponseData
    {
        public string access_token { get; set; }
    }
}
