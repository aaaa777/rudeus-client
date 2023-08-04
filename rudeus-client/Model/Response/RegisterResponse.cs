using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Response
{
    internal class RegisterResponse : BaseResponse
    {
        public RegisterResponseData ResponseData { get; set; }
    }

    internal class RegisterResponseData
    {
        public string AccessToken { get; set; }
    }
}
