using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model.Response
{
    internal class UpdateResponse : BaseResponse
    {
        public UpdateResponseData response_data { get; set; }
        public PushResponseData[] push_data { get; set; }
    }

    internal class UpdateResponseData
    {
        public string username { get; set; }
    }

    internal class PushResponseData
    {
        public string opcode { get; set; }
        public string data { get; set; }
    }
}
