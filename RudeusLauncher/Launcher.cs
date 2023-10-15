using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Launcher
{
    public static void Run(string registryKey)
    {
        // レジストリを切り替え
        Settings.UpdateRegistryKey(registryKey);

        string latestExePath = Settings.LatestVersionExePath;
        string lastExePath = Settings.LastVersionExePath;

        // 起動可能かダウンロード後未起動の状態のみでlatestを実行
        int exitCode = -1;
        if (!Settings.IsLatestVersionStatusUnlaunchable())
        {
            // latestの実行
            exitCode = StartProcess(latestExePath);
        }


        // latestが異常終了した時、lastにフォールバック
        if (exitCode != 0)
        {
            // ToDo: 終了が遅かった時はフォールバックしない？

            // レジストリにLatestが異常終了することを記録
            Settings.SetLatestVersionStatusUnlaunchable();

            // lastを実行、フォールバックは無し
            StartProcess(lastExePath);
        }
        else
        {
            // Latestの実行に成功したことを記録
            Settings.SetLatestVersionStatusOk();
        }

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
