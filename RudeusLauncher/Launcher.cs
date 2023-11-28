using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Launcher
{
    public static int Run(string registryKey)
    {
#if(DEBUG)
        Console.WriteLine("[Launcher] Debug build is running");
#else
        Console.WriteLine("[Launcher] Release build is running");
#endif
        // レジストリを切り替え
        Settings.UpdateRegistryKey(registryKey);
        Settings settings = new(registryKey);

        string latestExePath = Settings.LatestVersionExePath;
        string lastExePath = Settings.LastVersionExePath;

        // 起動可能かダウンロード後未起動の状態のみでlatestを実行
        int exitCode = -1;
        if (!Settings.IsLatestVersionStatusUnlaunchable())
        {
            // latestの実行
            Console.WriteLine("Trying launching latest version");
            exitCode = StartProcess(latestExePath);
        }
        else
        {
            Console.WriteLine("Last version is selected cuz latest version is marked `unlaunchable`");
            Console.WriteLine("After next update comes, latest version will be tryed to launch");
        }


        // latestが異常終了した時、lastにフォールバック
        if (exitCode != 0)
        {
            Console.WriteLine("Latest version returned wrong exit code or skipperd");
            Console.WriteLine("Trying launching last version");

            // ToDo: 終了が遅かった時はフォールバックしない？

            // レジストリにLatestが異常終了することを記録
            Settings.SetLatestVersionStatusUnlaunchable();

            // lastを実行、フォールバックは無し
            exitCode = StartProcess(lastExePath);
        }
        // latestが正常終了した時、そのまま終了
        else
        {
            Console.WriteLine("Latest version returned exit code 0");
            Console.WriteLine("Latest version status is ok");

            // Latestの実行に成功したことを記録
            Settings.SetLatestVersionStatusOk();
        }

        return exitCode;
    }

    public static int StartProcess(string filePath)
    {
        //int psExitCode = -1;
        try
        {
            Process ps = Process.Start(filePath);
            ps.WaitForExit();

            return ps.ExitCode;
        }
        catch
        {
            // launch failed
            return -1;
        }
    }
}
