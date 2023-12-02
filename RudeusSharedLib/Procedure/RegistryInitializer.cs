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

        public ISettings ConfSettings { get; set; }
        public  ISettings InnoSettings { get; set; }
        public ISettings BgSettings { get; set; }
        public ISettings BfSettings { get; set; }

        public RegistryInitializer(ISettings? cfs = null, ISettings? ins = null, ISettings? bgs = null, ISettings? bfs = null)
        {
            ConfSettings = cfs ?? new Settings();
            InnoSettings = ins ?? new Settings(InnoSetupUserDataRegKey);
            BgSettings = bgs ?? new Settings(RudeusBgRegKey);
            BfSettings = bfs ?? new Settings(RudeusBgFormRegKey);
        }

        public async Task Run()
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
            BgSettings.SetDevelopChannelP();
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
            //Settings BfSettings = new(RudeusBgFormRegKey);
#if (DEVELOPMENT)
            BfSettings.SetDevelopChannelP();
#else
            BfSettings.SetStableChannelP();
#endif


            BfSettings.LastVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLastName}";
            BfSettings.LastVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
            BfSettings.LastDirNameP = Constants.RudeusBgLastName;

            BfSettings.LatestVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLatestName}";
            BfSettings.LatestVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
            BfSettings.LatestDirNameP = Constants.RudeusBgLatestName;

            BfSettings.CurrentVersionP = "0.0.0.0";
            BfSettings.SetLatestVersionStatusDownloadedP();



            Console.WriteLine("[Installer] Registry: Settings loaded for launcher");
        }
    }

}