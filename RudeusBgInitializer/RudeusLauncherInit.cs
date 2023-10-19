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

#if (DEBUG)
        Settings.SetDevelopChannel();
#else
        Settings.SetStableChannel();
#endif
        Settings.LastVersionDirPath = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLastName}";
        Settings.LastVersionExeName = $"{Constants.RudeusBgExeName}";
        Settings.LastDirName = Constants.RudeusBgLastName;

        Settings.LatestVersionDirPath = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLatestName}";
        Settings.LatestVersionExeName = $"{Constants.RudeusBgExeName}";
        Settings.LatestDirName = Constants.RudeusBgLatestName;

        Settings.CurrentVersion = "0.0.0.0";
        Settings.SetLatestVersionStatusDownloaded();


        // RudeusBgFormのLaunch設定
        
        //Settings.UpdateRegistryKey(RudeusBgFormRegKey);
        Settings bfSettings = new(RudeusBgFormRegKey);
#if (DEBUG)
        bfSettings.SetDevelopChannelP();
#else
        Settings.SetStableChannel();
#endif
        bfSettings.LastVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLastName}";
        bfSettings.LastVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
        bfSettings.LastDirNameP = Constants.RudeusBgLastName;

        bfSettings.LatestVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLatestName}";
        bfSettings.LatestVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
        bfSettings.LatestDirNameP = Constants.RudeusBgLatestName;

        bfSettings.CurrentVersionP = "0.0.0.0";
        bfSettings.SetLatestVersionStatusDownloadedP();


        Console.WriteLine("Settings loaded for launcher");
    }
}
