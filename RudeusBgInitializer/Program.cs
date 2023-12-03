
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Runtime;
using System.Threading;
using System;
using Microsoft.Win32.TaskScheduler;
using Rudeus.API;
using Rudeus.Procedure;
namespace RudeusBgInitializer
{

    public class Program
    {
        // DI for static class
        public static IProcedure _registryInitializer { get; set; }
        public static IProcedure _taskInitializer { get; set; }
        public static IProcedure _certificateInstaller { get; set; }
        public static IProcedure _serverRegister { get; set; }

        public static bool? CheckLogMessage { get; set; }

        public static void Main(string[] args)
        {
            _registryInitializer ??= new RegistryInitializer();
            _taskInitializer ??= new TaskInitializer();
            _certificateInstaller ??= new CertificateInstaller();
            _serverRegister ??= new ServerRegister();
            CheckLogMessage ??= true;
            MainAsync(args).GetAwaiter().GetResult();

        }

        public static async System.Threading.Tasks.Task MainAsync(string[] args)
        {
            // Launcherのレジストリ初期値設定
            await _registryInitializer.Run();

            // タスクスケジューラ登録処理
            await _taskInitializer.Run();

            // ルート証明書登録処理
            await _certificateInstaller.Run();

            try
            {
                // デバイス情報送信、サーバ登録処理
                await _serverRegister.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

#if (!RELEASE)
            if (CheckLogMessage == true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("[Installer] Done, Press Enter");
                Console.ReadLine();
            }
#endif

        }
    }
}