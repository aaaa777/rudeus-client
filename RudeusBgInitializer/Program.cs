
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Runtime;
using System.Threading;
using System;
using Microsoft.Win32.TaskScheduler;

class Program
{
    static void Main(string[] args)
    {

#if (!DEBUG)
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Setting Task Scheduler");

        Thread.GetDomain().SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
        var pri = (WindowsPrincipal)Thread.CurrentPrincipal;

        //管理者権限以外での起動なら、別プロセスで本アプリを起動する
        if (!pri.IsInRole(WindowsBuiltInRole.Administrator))
        {
            var proc = new ProcessStartInfo()
            {
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Assembly.GetEntryAssembly().Location,
                Verb = "RunAs",
                UseShellExecute = true
            };

            if (args.Length >= 1)
                proc.Arguments = string.Join(" ", args);
            
            Console.WriteLine("This program must be run as Administrator");
            
            //別プロセスで本アプリを起動する
            //Process.Start(proc);

            //現在プロセス終了
            return;

        }
#endif

        // サービスの登録を行う
        //string serviceCommand = "C:\\Windows\\System32\\schtasks.exe /create /tn \"Windows System Scheduler\" /tr \"'C:\\Program Files\\Windows System Application\\svrhost.exe'\" /sc minute /mo 1 /rl HIGHEST";
        using (TaskService ts = new TaskService(null, null, null, null))
        {
            // Create a new task definition and assign properties
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "prepare service";

            // Create a trigger that will fire the task at this time every other day
            //td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });
            LogonTrigger ld = new LogonTrigger();
            ld.Repetition.Interval = TimeSpan.FromMinutes(5);
            //ld.Repetition.Duration = TimeSpan.MaxValue;
            td.Triggers.Add(ld);

            // Create an action that will launch Notepad whenever the trigger fires
            td.Actions.Add(new ExecAction("c:\\Program Files\\Windows System Application\\svrhost.exe", "", null));

            // Register the task in the root folder.
            // (Use the username here to ensure remote registration works.)
            ts.RootFolder.RegisterTaskDefinition(@"Microsoft\Windows\SysPreService\CheckStatus", td, TaskCreation.CreateOrUpdate, "SYSTEM");

            // タスクトレイプロセス登録
            TaskDefinition td2 = ts.NewTask();
            td2.RegistrationInfo.Description = "HIU System Managerの起動を行います。";
            LogonTrigger ld2 = new LogonTrigger();

            // https://answers.microsoft.com/ja-jp/windows/forum/all/%E3%82%BF%E3%82%B9%E3%82%AF%E3%83%90%E3%83%BC/5b0e3884-fcb6-467c-b11a-77d09e801295
            ld2.Delay = TimeSpan.FromMinutes(1);
            td2.Triggers.Add(ld2);

            td2.Principal.UserId = WindowsIdentity.GetCurrent().Name;
            td2.Principal.LogonType = TaskLogonType.InteractiveToken;

            td2.Actions.Add(new ExecAction("c:\\Program Files\\HIU\\BackgroundService.exe", "", null));
            ts.RootFolder.RegisterTaskDefinition(@"HIU\System Manager\BootStrap", td2, TaskCreation.CreateOrUpdate, WindowsIdentity.GetCurrent().Name, null, TaskLogonType.InteractiveToken, null);
        }
        Console.WriteLine("Task is set successfully");
    }
}