using Rudeus;
using Rudeus.API;
using Rudeus.API.Response;
using Rudeus.Model;
using Rudeus.Model.Operations;
using Rudeus.Procedure;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Windows.System.Inventory;
using SharedLib.Model.Settings;
using Rudeus.API.Request;

namespace Rudeus.Command
{
    public class Worker : BackgroundService, IWorker
    {
        private readonly ILogger<Worker> _logger;
        private string[] _args;

        // DI for static class
        public IProcedure AccessTokenValidator { get; set; }
        public IProcedure ScheduledRelularExecuter { get; set; }
        public IProcedure UserLoginExecuter { get; set; }

        public IRootSettings RootSettings { get; set; }

        public Worker(ILogger<Worker>? logger = null, string[]? args = null, IRootSettings? settings = null, IProcedure? accessTokenValidator = null, IProcedure? scheduledRegularExecuter = null, IProcedure? userLoginExecuter = null)
        {
            //_logger = logger;
            _args = args ?? Program.commandArgs ?? Array.Empty<string>();
            this.RootSettings = settings ?? new RootSettings();
            AccessTokenValidator = accessTokenValidator ?? new AccessTokenValidator(settings);
            ScheduledRelularExecuter = scheduledRegularExecuter ?? new RegularExecuter();
            UserLoginExecuter = userLoginExecuter ?? new UserLoginExecuter();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await RunAsync();
        }
        public async Task RunAsync()
        { 
            //Console.WriteLine($"Worker running accessTokenValidator: {this.RootSettings.DeviceIdP}");

            var argsDict = Utils.ParseArgs(_args);

            string mode = argsDict.GetValueOrDefault("mode", "default");

            if (!RemoteAPI.IsRemoteReachable())
            {
                Console.WriteLine("server is not reachable");
                Environment.Exit(0);
            }


            // 初期化
            await AccessTokenValidator.Run();

            // 通常起動時
            if (mode == "default")
            {
                // UpdateDeviceの実行
                await ScheduledRelularExecuter.Run();

                //InstalledAppsSender.Run();
            }

            if (mode == "login")
            {
                await UserLoginExecuter.Run();
            }

            // テスト実装確認用
            if (mode == "test")
            {
                var apps = await InstalledApplications.LoadAsync();
                try
                {
                    //RemoteAPI.SendInstalledApps(RootSettings.AccessTokenP, apps);
                    //ILocalMachine lm = LocalMachine.GetInstance();
                    //lm.GetNetworkInterfaces();

                    var req = new UpdateMacRequest();
                    var data = new UpdateMacInterface();
                    data.name = "test if";
                    data.mac_address = "12-34-56-78-90-12";
                    req.request_data.interfaces.Add(data);
                    RemoteAPI.UpdateMacAddress(RootSettings.AccessTokenP, req);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }



#if (!RELEASE)
            await Task.Delay(5000);
#endif
            Environment.Exit(0);
        }
    }
}