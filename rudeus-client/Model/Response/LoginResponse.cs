using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Response
{
    internal class LoginResponse : BaseResponse
    {
        public LoginResponseData ResponseData { get; set; }
    }

    internal class LoginResponseData
    {
        public string Username { get; set; }
    }
}
