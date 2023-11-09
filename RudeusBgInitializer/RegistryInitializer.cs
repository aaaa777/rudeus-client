using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class RegistryInitializer
{
    private readonly static string RudeusBgRegKey = Constants.RudeusBgRegKey;
    private readonly static string RudeusBgFormRegKey = Constants.RudeusBgFormRegKey;

    public static void Run()
    {
        // RudeusBgのLaunch設定
        
        //Settings.UpdateRegistryKey(RudeusBgRegKey);
        Settings bgSettings = new(RudeusBgRegKey);

#if (DEBUG)
        bgSettings.SetDevelopChannelP();
#else
        bgSettings.SetStableChannelP();
#endif
        bgSettings.LastVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLastName}";
        bgSettings.LastVersionExeNameP = $"{Constants.RudeusBgExeName}";
        bgSettings.LastDirNameP = Constants.RudeusBgLastName;

        bgSettings.LatestVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLatestName}";
        bgSettings.LatestVersionExeNameP = $"{Constants.RudeusBgExeName}";
        bgSettings.LatestDirNameP = Constants.RudeusBgLatestName;

        bgSettings.CurrentVersionP = "0.0.0.0";
        bgSettings.SetLatestVersionStatusDownloadedP();


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


        Console.WriteLine("[Installer] Registry: Settings loaded for launcher");
    }
}
