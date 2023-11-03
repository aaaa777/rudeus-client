using Rudeus.Model.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model.Watchdogs
{
    class Watcher
    {
        private WatchItem[] WatchItems =
        {
            //new WatchItem("device_id", GetDeviceId, Utils.GetDeviceId),
            new WatchItem("hostname", GetHostname, Utils.GetHostname),
        };
        public Watcher() 
        {
            
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
    }
}
