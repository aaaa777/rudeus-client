
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Runtime;
using System.Threading;
using System;
using Microsoft.Win32.TaskScheduler;
using Rudeus.Model;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Launcherのレジストリ初期値設定
            RegistryInitializer.Run();

            // タスクスケジューラ登録処理
            TaskInitializer.Register();

            // ルート証明書登録処理
            CertificateInstaller.InstallDefaults();

            // デバイス情報送信、サーバ登録処理
            ServerRegister.Run();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
        }

#if (DEBUG)
        Console.WriteLine("\n");
        Console.WriteLine("[Installer] Done, Press Enter");
        Console.ReadLine();
#endif

    }
}