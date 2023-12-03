
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Runtime;
using System.Threading;
using System;
using Microsoft.Win32.TaskScheduler;
using Rudeus.API;
using Rudeus.Procedure;

class Program
{
    // DI for static class
    public static IProcedure _registryInitializer = new RegistryInitializer();
    public static IProcedure _taskInitializer = new TaskInitializer();
    public static IProcedure _certificateInstaller = new CertificateInstaller();
    public static IProcedure _serverRegister = new ServerRegister();


    public static void Main(string[] args)
    {
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
        Console.WriteLine("\n");
        Console.WriteLine("[Installer] Done, Press Enter");
        Console.ReadLine();
#endif

    }
}