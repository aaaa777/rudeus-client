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

        string filePath = Settings.LatestVersionExePath;
        string altFilePath = Settings.LastVersionExePath;

        // 起動可能か未起動の状態のみでlatestを実行
        int exitCode = -1;
        if (!Settings.IsLatestVersionStatusUnlaunchable())
        {
            // latestの実行
            exitCode = StartProcess(filePath);
        }


        // latestが異常終了した時、lastにフォールバック
        if (exitCode != 0)
        {
            // ToDo: 終了が遅かった時はフォールバックしない？

            // レジストリにLastVersionStatusを登録
            Settings.SetLatestVersionStatusUnlaunchable();

            // lastを実行、フォールバックは無し
            StartProcess(altFilePath);
        }

    }

    public static int StartProcess(string filePath)
    {
        //int psExitCode = -1;
        try
        {
            Process ps = Process.Start(filePath);
            //ps.WaitForExit();

            return ps.ExitCode;
        }
        catch
        {
            // launch failed
            return -1;
        }
    }
}
