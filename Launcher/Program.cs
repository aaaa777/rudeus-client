// See https://aka.ms/new-console-template for more information



using Rudeus;
using Rudeus.Model;
using Rudeus.Procedure;
using Rudeus.Launcher.Procedure;
using SharedLib.Model.Settings;

namespace Rudeus.Launcher
{
    public class Program
    {
        public static IAppSettings AppSettings { get; set; }
        public static IRootSettings RootSettings { get; set; }
        public static string ArgsStr { get; set; }

        public static IProcedure Updater { get; set; }
        public static IExecuter Executer { get; set; }

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

            Updater = new Updater(aps: AppSettings, rts: RootSettings);
            Executer = new Executer(AppSettings, ArgsStr);
            ExitFunc = Exit;

            MainAsync().GetAwaiter().GetResult();
        }
        public static async Task MainAsync()
        {
            int exitCode = -1;

            do
            {
                // ToDo: 重複実行中にプロセスキルをする

                // アップデート確認・実行
                await Updater.Run();
                Console.WriteLine("Update process done");

                // アプリ起動
                await Executer.RunExe();
                exitCode = Executer.ExitCode;
                Console.WriteLine("ApplicationData stopped");
            }
            // 終了コードが強制アップデートを知らせるものだった場合、もう一度実行
            while (exitCode == Constants.ForceUpdateExitCode);

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

        private static bool Exit(int exitCode)
        {
            Environment.Exit(exitCode);
            return true;
        }
    }
}