using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rudeus_client.Model.Response
{
    internal class UpdateResponse : BaseResponse
    {
        public UpdateResponseData response_data { get; set; }
    }

    internal class UpdateResponseData
    {
        public string username { get; set; }
    }
}
