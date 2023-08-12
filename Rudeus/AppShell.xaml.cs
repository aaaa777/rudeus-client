namespace Rudeus;

public partial class AppShell : Shell
{

    public AppShell()
	{
		InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

        Shell.Current.FlyoutIsPresented = true;
    }
}
