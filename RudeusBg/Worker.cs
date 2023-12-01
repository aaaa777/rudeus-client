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

namespace RudeusBg
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string[] _args;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _args = Program.commandArgs;
            
        }
    

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker running at: {Settings.DeviceId}");

            var argsDict = _args.Select(arg => arg.Split('=')).Where(s => s.Length == 2).ToDictionary(v => v[0], v => v[1]);

            string mode = argsDict.GetValueOrDefault("mode", "default");

            if (!RemoteAPI.IsRemoteReachable())
            {
                _logger.LogInformation("server is not reachable");
                Environment.Exit(0);
            }


            // 初期化
            AccessTokenValidator.Run();

            // 通常起動時
            if (mode == "default")
            {
                // UpdateDeviceの実行
                ScheduledRegularExecuter.Run();

                //InstalledAppsSender.Run();
            }

            if (mode == "login")
            {
                StartUserLoginFlow.Run();
            }

            // テスト実装確認用
            if (mode == "test")
            {
                var apps = await InstalledApplications.LoadAsync();
                try
                {
                    RemoteAPI.SendInstalledApps(Settings.AccessToken, apps);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }



#if (!RELEASE)
            await Task.Delay(5000, stoppingToken);
#endif
            Environment.Exit(0);
        }
    }
}