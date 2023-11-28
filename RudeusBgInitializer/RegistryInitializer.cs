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
    private readonly static string InnoSetupUserDataRegKey = Constants.InnoSetupUserDataKey;

    public static void Run()
    {
        Settings confSettings = new Settings();
        Settings innoSettings = new Settings(InnoSetupUserDataRegKey);
        // ログイン前ユーザーIDを記録
        // TODO: ユーザーの認証状態を確認する

        // ラベルのIDをInnoから読みだして記録
        confSettings.LabelIdP = innoSettings.LabelIdP;

        // RudeusBgのLaunch設定
        
        //Settings.UpdateRegistryKey(RudeusBgRegKey);
        Settings bgSettings = new(RudeusBgRegKey);

#if (DEBUG)
        bgSettings.SetDevelopChannelP();
#else
        bgSettings.SetStableChannelP();
#endif

        // Debug時にProgram Filesを向かないように設定する
#if (DEBUG)
        bgSettings.LastVersionDirPathP = $"{Environment.CurrentDirectory}\\RudeusBg\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
        bgSettings.LastVersionExeNameP = $"{Constants.RudeusBgExeName}";
        bgSettings.LastDirNameP = Constants.RudeusBgLastName;

        bgSettings.LatestVersionDirPathP = $"{Environment.CurrentDirectory}\\RudeusBg\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
        bgSettings.LatestVersionExeNameP = $"{Constants.RudeusBgExeName}";
        bgSettings.LatestDirNameP = Constants.RudeusBgLatestName;
#else
        bgSettings.LastVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLastName}";
        bgSettings.LastVersionExeNameP = $"{Constants.RudeusBgExeName}";
        bgSettings.LastDirNameP = Constants.RudeusBgLastName;

        bgSettings.LatestVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLatestName}";
        bgSettings.LatestVersionExeNameP = $"{Constants.RudeusBgExeName}";
        bgSettings.LatestDirNameP = Constants.RudeusBgLatestName;
#endif
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

        // Debug時にProgram Filesを向かないように設定する
#if (DEBUG)
        bgSettings.LastVersionDirPathP = $"{Environment.CurrentDirectory}\\RudeusBgForm\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
        bgSettings.LastVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
        bgSettings.LastDirNameP = Constants.RudeusBgFormLastName;

        bgSettings.LatestVersionDirPathP = $"{Environment.CurrentDirectory}\\RudeusBgForm\\bin\\Debug\\net7.0-windows10.0.17763.0\\win-x64";
        bgSettings.LatestVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
        bgSettings.LatestDirNameP = Constants.RudeusBgFormLatestName;
#else
        bfSettings.LastVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLastName}";
        bfSettings.LastVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
        bfSettings.LastDirNameP = Constants.RudeusBgLastName;

        bfSettings.LatestVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLatestName}";
        bfSettings.LatestVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
        bfSettings.LatestDirNameP = Constants.RudeusBgLatestName;

        bfSettings.CurrentVersionP = "0.0.0.0";
        bfSettings.SetLatestVersionStatusDownloadedP();
#endif


        Console.WriteLine("[Installer] Registry: Settings loaded for launcher");
    }
}
