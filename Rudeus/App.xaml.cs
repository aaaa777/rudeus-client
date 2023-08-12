using Rudeus.Model;
using System.Collections.Specialized;
using System.Reflection;

namespace Rudeus;

public partial class App : Application
{
    private static readonly Mutex mutex = new(true, Assembly.GetEntryAssembly().GetName().Name);

    public App()
    {
        
        string[] Args = Environment.GetCommandLineArgs();

        if (!mutex.WaitOne(TimeSpan.Zero, true))
        {
            if(Args.Length > 1)
            {
                // Todo: 引数がカスタムURIスキーム形式であるかどうかを判定する必要がある
                Uri exeUri = new(Args[1]);
                NameValueCollection query = System.Web.HttpUtility.ParseQueryString(exeUri.Query);

                CallbackAPI.SendSamlCallback(query.Get("user_id"), query.Get("token"));
                // 必要？
                Task.Delay(10000);
            }
            Current.Quit();
            Environment.Exit(0);
        }

        // 変な引数構成の場合起動しない
        if (Args.Length != 1)
        {
            Current.Quit();
            Environment.Exit(0);
        }

        InitializeComponent();

		MainPage = new AppShell();
	}
}
