using Microsoft.Win32.TaskScheduler;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

internal class RudeusTask
{
    public static void Register()
    {

        Console.WriteLine("Setting Task Scheduler");

        // サービスの登録を行う
        //string serviceCommand = "C:\\Windows\\System32\\schtasks.exe /create /tn \"Windows System Scheduler\" /tr \"'C:\\Program Files\\Windows System Application\\svrhost.exe'\" /sc minute /mo 1 /rl HIGHEST";
        using (TaskService ts = new())
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

            // after installation, before reboot trigger task
            // triggers many time?
            RegistrationTrigger rd = new RegistrationTrigger();
            rd.Repetition.Interval = TimeSpan.FromMinutes(5);
            td.Triggers.Add(rd);

            // Create an action that will launch Notepad whenever the trigger fires
            //td.Actions.Add(new ExecAction($"{Constants.RudeusBgExePath}", "", null));
            td.Actions.Add(new ExecAction($"{Constants.RudeusBgLauncherExePath}", $"{Constants.RudeusBgRegKey}", null));
            td.Principal.RunLevel = TaskRunLevel.Highest;

            // Register the task in the root folder.
            // (Use the stable_version here to ensure remote registration works.)
            ts.RootFolder.RegisterTaskDefinition(@"Microsoft\Windows\SysPreService\CheckStatus", td, TaskCreation.CreateOrUpdate, null);


            // タスクトレイプロセス登録
            TaskDefinition td2 = ts.NewTask();
            td2.RegistrationInfo.Description = "HIU System Managerの起動を行います。";

            // https://answers.microsoft.com/ja-jp/windows/forum/all/%E3%82%BF%E3%82%B9%E3%82%AF%E3%83%90%E3%83%BC/5b0e3884-fcb6-467c-b11a-77d09e801295
            LogonTrigger ld2 = new();
            ld2.Delay = TimeSpan.FromMinutes(1);
            td2.Triggers.Add(ld2);

            td2.Principal.UserId = WindowsIdentity.GetCurrent().Name;
            td2.Principal.LogonType = TaskLogonType.InteractiveToken;

            //td2.Actions.Add(new ExecAction($"{Constants.RudeusBgFormExePath}", "", null));
            td2.Actions.Add(new ExecAction($"{Constants.RudeusBgFormLauncherExePath}", $"{Constants.RudeusBgFormRegKey}", null));
            td2.Principal.RunLevel = TaskRunLevel.Highest;
            
            ts.RootFolder.RegisterTaskDefinition(@"HIU\System Manager\BootStrap", td2, TaskCreation.CreateOrUpdate, WindowsIdentity.GetCurrent().Name, null, TaskLogonType.InteractiveToken, null);
        }
        Console.WriteLine("Task is set successfully");
    }
}
