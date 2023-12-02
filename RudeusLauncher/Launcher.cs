using Rudeus.Model;
using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rudeus.Procedure;


internal class Launcher : IReturnsExitCode
{
    //public static int Run(string registryKey, string args="")
    public ISettings _appSettings { get; set; }
    public string args { get; set; }

    public int ExitCode { get; set; }

    public Launcher(ISettings aps, string args="")
    {
        _appSettings = aps;
        this.args = args;
        ExitCode = -1;
    }

    public async Task Run()
    {
        string latestExePath = _appSettings.LatestVersionExePathP;
        string lastExePath = _appSettings.LastVersionExePathP;
        bool skipLatest = _appSettings.IsLatestVersionStatusUnlaunchableP();
#if(DEBUG)
        Console.WriteLine("[Launcher] Debug build is running");
#endif
        // レジストリを切り替え
        //Settings.UpdateRegistryKey(registryKey);
        //Settings settings = new(registryKey);

        //string latestExePath = Settings.LatestVersionExePath;
        //string lastExePath = Settings.LastVersionExePath;

        // 起動可能かダウンロード後未起動の状態のみでlatestを実行
        //ExitCode = -1;
        if (!skipLatest)
        {
            // latestの実行
            Console.WriteLine("Trying launching latest version");
            ExitCode = StartProcess(latestExePath, args);
        }
        else
        {
            Console.WriteLine("Last version is selected cuz latest version is marked `unlaunchable`");
            Console.WriteLine("After next update comes, latest version will be tryed to launch");
        }


        // latestが異常終了した時、lastにフォールバック
        if (ExitCode != 0)
        {
            if (!skipLatest)
            {
                Console.WriteLine("Latest version returned wrong exit code");
            }
            Console.WriteLine("Trying launching last version");

            // ToDo: 終了が遅かった時はフォールバックしない？

            // レジストリにLatestが異常終了することを記録
            Settings.SetLatestVersionStatusUnlaunchable();

            // lastを実行、フォールバックは無し
            ExitCode = StartProcess(lastExePath, args);
        }
        // latestが正常終了した時、そのまま終了
        else
        {
            Console.WriteLine("Latest version returned exit code 0");
            Console.WriteLine("Latest version status is ok");

            // Latestの実行に成功したことを記録
            Settings.SetLatestVersionStatusOk();
        }

        return;
    }

    public static int StartProcess(string filePath, string args="")
    {
        //int psExitCode = -1;
        try
        {
            Process ps = Process.Start(filePath, args);
            
            bool isExited = ps.WaitForExit(0);

            while(!ps.WaitForExit(10_000))
            {
                // waiting for exit
            }

            return ps.ExitCode;
        }
        catch
        {
            // launch failed
            return -1;
        }
    }
}
