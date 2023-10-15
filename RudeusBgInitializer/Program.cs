
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
            RudeusLauncherInit.Run();

            // タスクスケジューラ登録処理
            RudeusTask.Register();

            // ルート証明書登録処理
            RudeusCert.InstallCertificate();

            // デバイス情報送信、サーバ登録処理
            RudeusRegister.Run();
        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex.ToString());
        }
        Console.WriteLine("\n");
        Console.WriteLine("Done, Press Enter");
        Console.ReadLine();
    }
}