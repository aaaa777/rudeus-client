using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class RudeusLauncherInit
{
    private readonly static string RudeusBgRegKey = "Bg";
    private readonly static string RudeusBgFormRegKey = "Task";

    public static void Run()
    {
        // RudeusBgのLaunch設定
        Settings.UpdateRegistryKey(RudeusBgRegKey);

        Settings.SetStableChannel();
        Settings.LastVersionExePath = $"{Utils.RudeusBgDir}{Utils.RudeusBgExe}";
        Settings.LastUpdateVersion = "0.0.0.0";
        Settings.SetLatestVersionStatusOk();


        // RudeusBgFormのLaunch設定
        Settings.UpdateRegistryKey(RudeusBgFormRegKey);

        Settings.SetStableChannel();
        Settings.LastVersionExePath = $"{Utils.RudeusBgFormDir}{Utils.RudeusBgFormExe}";
        Settings.LastUpdateVersion = "0.0.0.0";
        Settings.SetLatestVersionStatusOk();

        Console.WriteLine("Settings loaded for launcher");
    }
}
