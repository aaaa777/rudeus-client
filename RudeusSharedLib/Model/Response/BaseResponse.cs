using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Response
{
    internal class BaseResponse
    {
        public string status { get; set; }
        public BaseResponse response_data { get; set; }

        public BaseResponse()
        {
            status = "ok";
        }

    }

    internal class BaseResponseData
    {
    }
}
