using Rudeus;
using Rudeus.API;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Procedure
{ 
    internal class RegistryInitializer : IProcedure
    {
        private readonly static string RudeusBgRegKey = Constants.RudeusBgRegKey;
        private readonly static string RudeusBgFormRegKey = Constants.RudeusBgFormRegKey;
        private readonly static string InnoSetupUserDataRegKey = Constants.InnoSetupUserDataKey;

        public ISettings ConfSettings { get; set; } = new Settings();
        public  ISettings InnoSettings { get; set; } = new Settings(InnoSetupUserDataRegKey);
        public ISettings BgSettings { get; set; } = new Settings(RudeusBgRegKey);
        public ISettings BfSettings { get; set; } = new Settings(RudeusBgFormRegKey);


        public void Run()
        {
            //Settings confSettings = new Settings();
            //Settings innoSettings = new Settings(InnoSetupUserDataRegKey);
            // ログイン前ユーザーIDを記録
            // TODO: ユーザーの認証状態を確認する

            // ラベルのIDをInnoから読みだして記録
            ConfSettings.LabelIdP = InnoSettings.LabelIdP;

            // RudeusBgのLaunch設定
        
            //Settings.UpdateRegistryKey(RudeusBgRegKey);
            //Settings bgSettings = new(RudeusBgRegKey);

#if (DEVELOPMENT)
            bgSettings.SetDevelopChannelP();
#else
            BgSettings.SetStableChannelP();
#endif


            BgSettings.LastVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLastName}";
            BgSettings.LastVersionExeNameP = $"{Constants.RudeusBgExeName}";
            BgSettings.LastDirNameP = Constants.RudeusBgLastName;

            BgSettings.LatestVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLatestName}";
            BgSettings.LatestVersionExeNameP = $"{Constants.RudeusBgExeName}";
            BgSettings.LatestDirNameP = Constants.RudeusBgLatestName;

            BgSettings.CurrentVersionP = "0.0.0.0";
            BgSettings.SetLatestVersionStatusDownloadedP();


            // RudeusBgFormのLaunch設定
        
            //Settings.UpdateRegistryKey(RudeusBgFormRegKey);
            Settings bfSettings = new(RudeusBgFormRegKey);
#if (DEVELOPMENT)
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

}