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
    public class Worker : BackgroundService, IWorker
    {
        private readonly ILogger<Worker> _logger;
        private string[] _args;

        // DI for static class
        public IProcedure _accessTokenValidator = new AccessTokenValidator();
        public IProcedure _scheduledRelularExecuter = new ScheduledRegularExecuter();
        public IProcedure _userLoginExecuter = new UserLoginExecuter();

        public static Settings settings { get; set; } = new Settings();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _args = Program.commandArgs;
        }
    

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker running at: {Settings.DeviceId}");

            var argsDict = Utils.ParseArgs(_args);

            string mode = argsDict.GetValueOrDefault("mode", "default");

            if (!RemoteAPI.IsRemoteReachable())
            {
                _logger.LogInformation("server is not reachable");
                Environment.Exit(0);
            }


            // ������
            _accessTokenValidator.Run();

            // �ʏ�N����
            if (mode == "default")
            {
                // UpdateDevice�̎��s
                _scheduledRelularExecuter.Run();

                //InstalledAppsSender.Run();
            }

            if (mode == "login")
            {
                _userLoginExecuter.Run();
            }

            // �e�X�g�����m�F�p
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