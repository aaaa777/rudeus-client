using Rudeus.Model;
using System.Collections.Specialized;
using System.Reflection;

namespace Rudeus;

public partial class App : Application
{

    public App()
    {
        
        

        InitializeComponent();

		MainPage = new AppShell();
	}
}
