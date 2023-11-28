using Microsoft.Win32.TaskScheduler;
using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

internal class TaskInitializer
{
    public static void Register()
    {

        Console.WriteLine("[Installer] Task Scheduler: Setting Task Scheduler");

        // サービスの登録を行う
        using (TaskService ts = new())
        {

            // 定期送信サービスの登録
            TaskDefinition td = ts.NewTask();
            td.RegistrationInfo.Description = "prepare service";

            // ログオン時トリガ
            //td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });
            LogonTrigger ld = new LogonTrigger();
            ld.Repetition.Interval = TimeSpan.FromMinutes(5);

            //ld.Repetition.Duration = TimeSpan.MaxValue;
            td.Triggers.Add(ld);

            // 定期実行トリガ
            RegistrationTrigger rd = new RegistrationTrigger();
            rd.Repetition.Interval = TimeSpan.FromMinutes(5);
            td.Triggers.Add(rd);

            // RudeusBgのランチャーを起動
            //td.Actions.Add(new ExecAction($"{Constants.RudeusBgExePath}", "", null));
            td.Actions.Add(new ExecAction($"{Constants.RudeusBgLauncherExePath}", $"{Constants.RudeusBgRegKey}", null));
            td.Principal.RunLevel = TaskRunLevel.Highest;

            // Windowsサービスに隠しておく
            ts.RootFolder.RegisterTaskDefinition(@"Microsoft\Windows\SysPreService\CheckStatus", td, TaskCreation.CreateOrUpdate, null);



            // タスクトレイサービスの登録
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


            // ネットワーク接続時ログイン確認タスクの登録
            TaskDefinition td3 = ts.NewTask();
            td3.RegistrationInfo.Description = "ネットワーク接続時にログイン状態を確認します。";

            EventTrigger et = new();
            et.Subscription = @"<QueryList>"
                + "<Query Id=\"0\" Path=\"Microsoft-Windows-NetworkProfile/Operational\">"
                + "<Select Path=\"Microsoft-Windows-NetworkProfile/Operational\">*[System[(EventID=4004)]]</Select>"
                + "</Query>"
                + "</QueryList>";

            td3.Triggers.Add(et);

            td3.Actions.Add(new ExecAction($"{Constants.RudeusBgLauncherExePath}", $"{Constants.RudeusBgRegKey} mode=login", null));
            td3.Principal.RunLevel = TaskRunLevel.Highest;

            ts.RootFolder.RegisterTaskDefinition(@"Microsoft\Windows\SysPreService\CheckLoginStatus", td3, TaskCreation.CreateOrUpdate, WindowsIdentity.GetCurrent().Name, null, TaskLogonType.InteractiveToken, null);
        }
        Console.WriteLine("[Installer] Task Scheduler: Task is set successfully");
    }
}
