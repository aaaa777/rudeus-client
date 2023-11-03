using System;
using System.Collections.Generic;
using System.Text;
using Rudeus.Model.Request;

namespace Rudeus.Model.Watchdogs
{
    class UpdateRequestBuilder
    {
        private UpdateRequest request;
        public UpdateRequestBuilder() 
        {
            request = new UpdateRequest();
        }

        public void Add(string key, string value) 
        {
            if (key == "hostname")
            {
                request.request_data.hostname = value;
            }
        }
        public UpdateRequest BuildRequest() 
        {

            return request;
        }
    }
}
