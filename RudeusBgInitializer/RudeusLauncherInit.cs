using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class RudeusLauncherInit
{
    private readonly static string RudeusBgRegKey = Constants.RudeusBgRegKey;
    private readonly static string RudeusBgFormRegKey = Constants.RudeusBgFormRegKey;

    public static void Run()
    {
        // RudeusBgのLaunch設定
        Settings.UpdateRegistryKey(RudeusBgRegKey);

        Settings.SetStableChannel();
        Settings.LastVersionDirPath = Constants.RudeusBgDir;
        Settings.LastVersionExeName = Constants.RudeusBgExeName;
        Settings.LastUpdateVersion = "0.0.0.0";
        Settings.SetLatestVersionStatusOk();


        // RudeusBgFormのLaunch設定
        Settings.UpdateRegistryKey(RudeusBgFormRegKey);

        Settings.SetStableChannel();
        Settings.LastVersionDirPath = Constants.RudeusBgFormDir;
        Settings.LastVersionExeName = Constants.RudeusBgFormExeName;
        Settings.LastUpdateVersion = "0.0.0.0";
        Settings.SetLatestVersionStatusOk();

        Console.WriteLine("Settings loaded for launcher");
    }
}
