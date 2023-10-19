// See https://aka.ms/new-console-template for more information



using Rudeus.Model;

public class Program
{
    public static void Main(string[] args)
    {
#if(DEBUG)
        args = new string[] { Constants.RudeusBgFormRegKey };
        //args = new string[] { Constants.RudeusBgRegKey };
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
        while (exitCode == Constants.ForceUpdateExitCode);
    }
}