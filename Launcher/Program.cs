// See https://aka.ms/new-console-template for more information



using Rudeus;
using Rudeus.Model;
using Rudeus.Procedure;
using Rudeus.Launcher.Procedure;
using SharedLib.Model.Settings;

namespace Rudeus.Launcher
{
    /// <summary>
    /// ランチャーのエントリーポイント
    /// 管理サーバからアップデート出来ないクラスです
    /// </summary>
    public class Program
    {
        public static IAppSettings AppSettings { get; set; }
        public static IRootSettings RootSettings { get; set; }
        public static string ArgsStr { get; set; }

        public static IProcedure Updater { get; set; }
        public static IExecuter Executer { get; set; }
        public static IProcedure AccessTokenValidator { get; set; }

        public static Func<int, bool> ExitFunc { get; set; }

        // 実質的なコンストラクタ
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                return;
            }

            if (args[0] != Constants.RudeusBgRegKey && args[0] != Constants.RudeusBgFormRegKey)
            {
                return;
            }

            string registryKey = args[0];
            AppSettings = new AppSettings(registryKey);
            RootSettings = new RootSettings();

            ArgsStr = JoinArgs(args);

            AccessTokenValidator = new AccessTokenValidator(RootSettings);
            Updater = new Updater(aps: AppSettings, rts: RootSettings);
            Executer = new Executer(AppSettings, ArgsStr);
            ExitFunc = Exit;

            MainAsync().GetAwaiter().GetResult();
        }
        public static async Task MainAsync()
        {
            Console.WriteLine($"Rudeus launcher version: {Version.ToString()}");

            int exitCode = -1;
            int runCount = 0;
            var argDict = Utils.ParseArgs(ArgsStr.Split(' '));

            bool spamMode = false;
            if(argDict.ContainsKey("spam"))
            {
                Console.WriteLine("[Warning] selected spam mode but its deprecated");
                //spamMode = true;
            }

            do
            {
                // 2回目以降の実行時
                if(runCount > 0)
                {
                    // プロセス再実行する場合に少し待つ
                    await Task.Delay(2000);

                    // Executerの再初期化
                    //Executer = new Executer(AppSettings, ArgsStr);
                }
                // アクセストークンの有効性を確認
                await AccessTokenValidator.Run();

                // ToDo: 重複実行中にプロセスキルをする

                // アップデート確認・実行
                await Updater.Run();
                Console.WriteLine("Update process done");

                // アプリ起動
                await Executer.RunExe();
                exitCode = Executer.ExitCode;
                Console.WriteLine("ApplicationData stopped");

                runCount++;
            }
            // 終了コードによってもう一度実行するか決める
            while (IsRetry(exitCode, spamMode));

#if (!RELEASE)
        Console.WriteLine("Program end.");
        //Console.ReadLine();
#endif
            ExitFunc(exitCode);
        }

        public static string JoinArgs(string[] args)
        {
            if (args.Length > 0)
            {
               return string.Join(" ", args[1..]);
            }
            return "";
        }

        private static bool IsRetry(int exitCode, bool spamMode)
        {
            if(exitCode == Constants.ForceUpdateExitCode)
            {
                return true;
            }
            if(exitCode == Constants.RestartRequiredCode)
            {
                return true;
            }
            if(spamMode)
            {
                return Constants.SpamModeExitCode != exitCode;
            }
            return false;
        }

        private static bool Exit(int exitCode)
        {
            Environment.Exit(exitCode);
            return true;
        }
    }
}