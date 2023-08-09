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
                Uri customUri = new(Args[1]);
                HttpClient Client = new()
                {
                    BaseAddress = new Uri($"http://localhost:11178"),
                };
                NameValueCollection queryDict = System.Web.HttpUtility.ParseQueryString(customUri.Query);
                HttpResponseMessage res = Client.GetAsync($"/?user={queryDict.Get("user")}").Result;
            }
            Current.Quit();
            Environment.Exit(0);
        }

        if (Args.Length != 1)
        {
            Current.Quit();
            Environment.Exit(0);
        }

        InitializeComponent();

		MainPage = new AppShell();
        // MainPage = new NavigationPage(new MainPage());
	}
}
