using Rudeus.API.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Rudeus.Model;

namespace Rudeus.Watchdogs
{
    class Watcher
    {
        private WatchItem[] WatchItems;
        
        public Watcher() 
        {
            WatchItems = new WatchItem[]
            {
                //new WatchItem("device_id", GetDeviceId, Utils.GetDeviceId),
                new WatchItem("hostname", GetHostname, Utils.GetHostname),
                //new WatchItem("hostname", GetSpec, Utils.GetSpec),
            };
        }

        public UpdateRequest BuildUpdateRequestWithChanges()
        {
            UpdateRequestBuilder builder = new();
            foreach (var item in WatchItems)
            {
                if(item.IsChanged())
                {
                    builder.Add(item.Name, item.Status());
                }
            }
            return builder.BuildRequest();
        }

        private static string GetDeviceId()
        {
            return Settings.DeviceId;
        }

        private static string GetHostname()
        {
            return Settings.Hostname;
        }

        private static string GetSpec()
        {
            throw new NotImplementedException();
            //return Settings.Spec;
        }
    }
}
