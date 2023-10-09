using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    internal class Updater
    {
        static public string UpdateCheckUrl = "";
        
        static public void Init()
        {
            //AutoUpdater;
        }

        public static void Run()
        {
            AutoUpdater.Start(UpdateCheckUrl);
        }
    }
}
