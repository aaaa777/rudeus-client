// See https://aka.ms/new-console-template for more information



using Rudeus.Model;

public class Program
{
    public static void Main(string[] args)
    {
#if(DEBUG)
        args = new string[] { $"TestApp" };
#endif
        if(args.Length < 1)
        {
            return;
        }

        string registryKey = args[1];

        // アップデート確認・実行
        Updater.Run(registryKey);

        // アプリ起動
        Launcher.Run(registryKey);
    }
}