using System;
using System.Collections.Generic;
using System.Text;
using Rudeus.API.Request;

namespace Rudeus.Watchdogs
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
            
            if (key == "spec")
            {
                request.request_data.spec = value;
            }
        }
        public UpdateRequest BuildRequest() 
        {

            return request;
        }
    }
}
