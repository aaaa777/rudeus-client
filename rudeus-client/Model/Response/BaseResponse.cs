using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Response
{
    internal class BaseResponse
    {
        public BaseResponse()
        {
            Status = "ok";
        }

        public string Status { get; set; }

        public BaseResponse ResponseData { get; set; }
    }

    internal class BaseResponseData
    {
    }
}
