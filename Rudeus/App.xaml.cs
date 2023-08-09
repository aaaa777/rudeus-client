using System.Reflection;

namespace Rudeus;

public partial class App : Application
{
    private static readonly Mutex mutex = new(true, Assembly.GetEntryAssembly().GetName().Name);

    public App()
    {
        if (!mutex.WaitOne(TimeSpan.Zero, true))
        {
            string[] Args = Environment.GetCommandLineArgs();
            if(Args.Length > 1)
            {
                HttpClient Client = new()
                {
                    BaseAddress = new Uri($"http://localhost:11178"),
                };
                HttpResponseMessage res = Client.GetAsync($"/?user=s2112097").Result;
            }
            Current.Quit();
            Environment.Exit(0);
        }

		InitializeComponent();

		MainPage = new AppShell();
        // MainPage = new NavigationPage(new MainPage());
	}
}
