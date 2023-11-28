// See https://aka.ms/new-console-template for more information



using Rudeus.Model;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 1 )
        {
            return;
        }

        if (args[0] != Constants.RudeusBgRegKey && args[0] != Constants.RudeusBgFormRegKey)
        {
            return;
        }

        string registryKey = args[0];
        int exitCode = -1;

        Settings settings = new(registryKey);

        string argsStr = "";

        if (args.Length > 1)
        {
            argsStr = string.Join(" ", args[1..]);
        }

        do
        {
            // ToDo: 重複実行中にプロセスキルをする

            // アップデート確認・実行
            Updater.Run(registryKey);
            Console.WriteLine("Update process done");

            // アプリ起動
            exitCode = Launcher.Run(
                settings.LatestVersionExePathP,
                settings.LastVersionExePathP,
                settings.IsLatestVersionStatusUnlaunchableP(),
                argsStr
            );
            Console.WriteLine("Application stopped");
        }
        // 終了コードが強制アップデートを知らせるものだった場合、もう一度実行
        while (exitCode == Constants.ForceUpdateExitCode);

#if (DEBUG)
        Console.WriteLine("Program end.");
        //Console.ReadLine();
#endif
    }
}