// See https://aka.ms/new-console-template for more information



using Rudeus.Model;

public class Program
{
    public static void Main(string[] args)
    {
#if(DEBUG)
        if(args.Length == 0)
        {
            args = new string[] { Constants.RudeusBgFormRegKey };
        }
#endif
        if (args.Length < 1)
        {
            return;
        }

        string registryKey = args[0];
        int exitCode = -1;

        do
        {
            // ToDo: 重複実行中にプロセスキルをする

            // アップデート確認・実行
            Updater.Run(registryKey);
            Console.WriteLine("Update process done");

            // アプリ起動
            exitCode = Launcher.Run(registryKey);
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