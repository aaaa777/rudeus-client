using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Watchdogs
{
    internal class WatchItem
    {
        public string Name;
        public Func<string> GetLastStatusFunc { get; set; }
        public Func<string> GetLatestStatusFunc { get; set; }
        public WatchItem(string name, Func<string> getLastStatusFunc, Func<string> getLatestStatusFunc)
        {
            Name = name;
            GetLastStatusFunc = getLastStatusFunc;
            GetLatestStatusFunc = getLatestStatusFunc;
        }
        public bool IsChanged()
        {
            return GetLastStatusFunc() == GetLatestStatusFunc();
        }

        public string Status()
        {
            return GetLatestStatusFunc();
        }
    }
}
