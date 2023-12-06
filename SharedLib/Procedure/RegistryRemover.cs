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
    public class RegistryRemover : IProcedure
    {
        private readonly static string RudeusBgRegKey = Constants.RudeusBgRegKey;
        private readonly static string RudeusBgFormRegKey = Constants.RudeusBgFormRegKey;
        private readonly static string InnoSetupUserDataRegKey = Constants.InnoSetupUserDataKey;

        public IRootSettings ConfSettings { get; set; }
        public  IAppSettings InnoSettings { get; set; }
        public IAppSettings BgSettings { get; set; }
        public IAppSettings BfSettings { get; set; }

        public RegistryRemover(IRootSettings? cfs = null, IAppSettings? ins = null, IAppSettings? bgs = null, IAppSettings? bfs = null)
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
            ConfSettings.DeleteAll();
            InnoSettings.DeleteAll();
            BgSettings.DeleteAll();
            BfSettings.DeleteAll();

            Console.WriteLine("Registry: All settings are deleted");
        }
    }

}