// See https://aka.ms/new-console-template for more information



using Rudeus;
using Rudeus.Model;
using Rudeus.Procedure;
using Rudeus.Launcher.Procedure;

namespace Rudeus.Launcher
{
    public class Program
    {
        public static ISettings _appSettings { get; set; }
        public static string _argsStr { get; set; }

        public static IProcedure _updater { get; set; }
        public static ILauncher _launcher { get; set; }

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
            _appSettings = new Settings(registryKey);

            _argsStr = "";
            if (args.Length > 1)
            {
                _argsStr = string.Join(" ", args[1..]);
            }

            _updater = new Updater();
            _launcher = new Launcher(_appSettings, _argsStr);

            MainAsync().GetAwaiter().GetResult();
        }
        public static async Task MainAsync()
        {
            int exitCode = -1;



            do
            {
                // ToDo: 重複実行中にプロセスキルをする

                // アップデート確認・実行
                await _updater.Run();
                Console.WriteLine("Update process done");

                // アプリ起動
                await _launcher.Run();
                exitCode = _launcher.ExitCode;
                Console.WriteLine("ApplicationData stopped");
            }
            // 終了コードが強制アップデートを知らせるものだった場合、もう一度実行
            while (exitCode == Constants.ForceUpdateExitCode);

#if (!RELEASE)
        Console.WriteLine("Program end.");
        //Console.ReadLine();
#endif
            Environment.Exit(exitCode);
        }
    }
}