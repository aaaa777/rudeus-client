using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Model
{
    internal class BgUpdater
    {
        public static void Run()
        {
            AutoUpdater.Start();
        }
    }
}
