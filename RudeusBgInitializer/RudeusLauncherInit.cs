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
        Settings.LastVersionDirPath = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLastName}";
        Settings.LastVersionExeName = $"{Constants.RudeusBgExeName}";
        Settings.LastDirName = Constants.RudeusBgLastName;

        Settings.LatestVersionDirPath = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLatestName}";
        Settings.LatestVersionExeName = $"{Constants.RudeusBgExeName}";
        Settings.LatestDirName = Constants.RudeusBgLatestName;

        Settings.LastUpdateVersion = "0.0.0.0";
        Settings.SetLatestVersionStatusDownloaded();


        // RudeusBgFormのLaunch設定
        
        Settings.UpdateRegistryKey(RudeusBgFormRegKey);

        Settings.SetStableChannel();
        Settings.LastVersionDirPath = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLastName}";
        Settings.LastVersionExeName = $"{Constants.RudeusBgFormExeName}";
        Settings.LastDirName = Constants.RudeusBgLastName;

        Settings.LatestVersionDirPath = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLatestName}";
        Settings.LatestVersionExeName = $"{Constants.RudeusBgFormExeName}";
        Settings.LatestDirName = Constants.RudeusBgLatestName;

        Settings.LastUpdateVersion = "0.0.0.0";
        Settings.SetLatestVersionStatusDownloaded();


        Console.WriteLine("Settings loaded for launcher");
    }
}
