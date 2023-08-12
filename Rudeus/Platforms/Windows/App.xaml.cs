using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Rudeus.Model;
using System.Collections.Specialized;
using System.Reflection;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Rudeus.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    private static readonly Mutex mutex = new(true, Assembly.GetEntryAssembly().GetName().Name);
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
        string[] Args = Environment.GetCommandLineArgs();

        if (!mutex.WaitOne(TimeSpan.Zero, true))
        {
            if (Args.Length > 1)
            {
                // Todo: 引数がカスタムURIスキーム形式であるかどうかを判定する必要がある
                Uri exeUri = new(Args[1]);
                NameValueCollection query = System.Web.HttpUtility.ParseQueryString(exeUri.Query);

                CallbackAPI.SendSamlCallback(query.Get("user_id"), query.Get("token"));
                // 必要？
                Task.Delay(10000);
            }
            // Current.Quit();
            Environment.Exit(0);
        }

        // 変な引数構成の場合起動しない
        if (Args.Length != 1)
        {
            // Current.Quit();
            Environment.Exit(0);
        }

        this.InitializeComponent();

        // ウィンドウサイズの指定
		int WindowWidth = 1024;
		int WindowHeight = 768;
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));

        });
    }

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

