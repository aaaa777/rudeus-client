using Rudeus;
using Rudeus.API;
using Rudeus.Model;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Procedure
{
    /// <summary>
    /// ローカルマシンに必要なレジストリを初期化する手続き
    /// </summary>
    public class RegistryInitializer : IProcedure
    {
        private readonly static string RudeusBgRegKey = Constants.RudeusBgRegKey;
        private readonly static string RudeusBgFormRegKey = Constants.RudeusBgFormRegKey;
        private readonly static string InnoSetupUserDataRegKey = Constants.InnoSetupUserDataKey;

        public IRootSettings ConfSettings { get; set; }
        public  IAppSettings InnoSettings { get; set; }
        public IAppSettings BgSettings { get; set; }
        public IAppSettings BfSettings { get; set; }

        public RegistryInitializer(IRootSettings? cfs = null, IAppSettings? ins = null, IAppSettings? bgs = null, IAppSettings? bfs = null)
        {
            ConfSettings = cfs ?? new RootSettings();
            InnoSettings = ins ?? new AppSettings(InnoSetupUserDataRegKey);
            BgSettings = bgs ?? new AppSettings(RudeusBgRegKey);
            BfSettings = bfs ?? new AppSettings(RudeusBgFormRegKey);
        }

        // TODO: 既にレジストリが存在する場合は上書き
        /// <inheritdoc/>
        public async Task Run()
        {
            //RootSettings confSettings = new RootSettings();
            //RootSettings innoSettings = new RootSettings(InnoSetupUserDataRegKey);
            // ログイン前ユーザーIDを記録
            // TODO: ユーザーの認証状態を確認する

            // ラベルのIDをInnoから読みだして記録
            //ConfSettings.LabelIdP = InnoSettings.LabelIdP;

            // RudeusBgのLaunch設定
        
            //RootSettings.UpdateRegistryKey(RudeusBgRegKey);
            //RootSettings bgSettings = new(RudeusBgRegKey);

#if (DEVELOPMENT)
            ConfSettings.SetDevelopChannelP();
#else
            ConfSettings.SetStableChannelP();
#endif


            BgSettings.LastVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLastName}";
            BgSettings.LastVersionExeNameP = $"{Constants.RudeusBgExeName}";
            BgSettings.LastDirNameP = Constants.RudeusBgLastName;

            BgSettings.LatestVersionDirPathP = $"{Constants.RudeusBgDir}\\{Constants.RudeusBgLatestName}";
            BgSettings.LatestVersionExeNameP = $"{Constants.RudeusBgExeName}";
            BgSettings.LatestDirNameP = Constants.RudeusBgLatestName;

            BgSettings.CurrentVersionP = "0.0.0.0";
            BgSettings.SetLatestVersionStatusUnlaunchableP();


            // RudeusBgFormのLaunch設定
        
            //RootSettings.UpdateRegistryKey(RudeusBgFormRegKey);
            //RootSettings BfSettings = new(RudeusBgFormRegKey);
#if (DEVELOPMENT)
            ConfSettings.SetDevelopChannelP();
#else
            ConfSettings.SetStableChannelP();
#endif


            BfSettings.LastVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLastName}";
            BfSettings.LastVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
            BfSettings.LastDirNameP = Constants.RudeusBgLastName;

            BfSettings.LatestVersionDirPathP = $"{Constants.RudeusBgFormDir}\\{Constants.RudeusBgLatestName}";
            BfSettings.LatestVersionExeNameP = $"{Constants.RudeusBgFormExeName}";
            BfSettings.LatestDirNameP = Constants.RudeusBgLatestName;

            BfSettings.CurrentVersionP = "0.0.0.0";
            BfSettings.SetLatestVersionStatusUnlaunchableP();

            Console.WriteLine("[Installer] Registry: RootSettings loaded for launcher");
        }
    }

}