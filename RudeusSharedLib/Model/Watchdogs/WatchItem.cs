using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    internal class WatchItem
    {
        public string Name;
        public Func<string> GetLastStatus { get; set; }
        public Func<string> GetLatestStatus { get; set; }
        public WatchItem(string name, Func<string> getLastStatus, Func<string> getLatestStatus)
        {
            Name = name;
            GetLastStatus = getLastStatus;
            GetLatestStatus = getLatestStatus;
        }
        public bool IsChanged()
        {
            return GetLastStatus() == GetLatestStatus();
        }

        public string Status()
        {
            return GetLatestStatus();
        }
    }
}
