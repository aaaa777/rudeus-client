using Rudeus.Procedure;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rudeus.Launcher.Procedure
{
    /// <summary>
    /// アプリの起動を行う手続き
    /// </summary>
    public class Executer : IExecuter
    {
        //public static int Run(string registryKey, string Args="")
        public IAppSettings AppSettings { get; set; }
        public string Args { get; set; }

        public int ExitCode { get; set; }

        public Func<string, string, int> StartProcess { get; set; }


        public Executer(IAppSettings aps, string args = "", Func<string, string, int>? startProcess = null)
        {
            AppSettings = aps;
            this.Args = args;
            ExitCode = -1;
            StartProcess = startProcess ?? _startProcess;
        }

        /// <inheritdoc/>
        public async Task RunExe()
        {
            string latestExePath = AppSettings.LatestVersionExePathP;
            string lastExePath = AppSettings.LastVersionExePathP;
            bool skipLatest = AppSettings.IsLatestVersionStatusUnlaunchableP();
#if (DEBUG)
        Console.WriteLine("[Executer] Debug build is running");
#endif
            // レジストリを切り替え
            //RootSettings.UpdateRegistryKey(registryKey);
            //RootSettings settings = new(registryKey);

            //string latestExePath = RootSettings.LatestVersionExePath;
            //string lastExePath = RootSettings.LastVersionExePath;

            // 起動可能かダウンロード後未起動の状態のみでlatestを実行
            //ExitCode = -1;
            if (!skipLatest)
            {
                // latestの実行
                Console.WriteLine("Trying launching latest stable");
                ExitCode = StartProcess(latestExePath, Args);
            }
            else
            {
                Console.WriteLine("Last stable is selected cuz latest stable is marked `unlaunchable`");
                Console.WriteLine("After next update comes, latest stable will be tryed to launch");
            }


            // latestが異常終了した時、lastにフォールバック
            if (ExitCode != 0)
            {
                if (!skipLatest)
                {
                    Console.WriteLine("Latest stable returned wrong exit code");
                }
                Console.WriteLine("Trying launching last stable");

                // ToDo: 終了が遅かった時はフォールバックしない？

                // レジストリにLatestが異常終了することを記録
                AppSettings.SetLatestVersionStatusUnlaunchableP();

                // lastを実行、フォールバックは無し
                ExitCode = StartProcess(lastExePath, Args);
            }
            // latestが正常終了した時、そのまま終了
            else
            {
                Console.WriteLine("Latest stable returned exit code 0");
                Console.WriteLine("Latest stable status is ok");

                // Latestが実行されている最中に別プロセスでアップデートされok以外になった場合、そちらを優先する
                if(AppSettings.IsLatestVersionStatusOkP())
                {
                    // レジストリにLatestが正常終了することを記録
                    AppSettings.SetLatestVersionStatusOkP();
                }
            }

            return;
        }

        private int _startProcess(string filePath, string args = "")
        {
            //int psExitCode = -1;
            try
            {
                Process ps = Process.Start(filePath, args);

                //bool isExited = ps.WaitForExit(0);

                while (!ps.WaitForExit(10_000))
                {
                    // waiting for exit loop
                }

                return ps.ExitCode;
            }
            catch
            {
                // launch failed
                return -1;
            }
        }

        public Task Run()
        {
            throw new NotImplementedException();
        }
    }
}