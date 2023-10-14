// See https://aka.ms/new-console-template for more information



using Rudeus.Model;

public class Program
{
    public static void Main(string[] args)
    {
#if(DEBUG)
        args = new string[] { $"latest\\Testapp.exe", $"last\\Testapp.exe", "TestApp" };
#endif
        if(args.Length < 2)
        {
            return;
        }

        string latestAppPath = args[0];
        string lastAppPath = args[1];
        string registryKey = args[2];

        // アップデート確認・実行
        Updater.Run(registryKey);

        // アプリ起動
        Launcher.Run(registryKey);
    }
}