using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Response
{
    internal class LoginResponse : BaseResponse
    {
        public LoginResponseData response_data { get; set; }
    }

    internal class LoginResponseData
    {
        public string username { get; set; }
    }
}
