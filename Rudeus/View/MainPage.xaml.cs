using System.Net.Http;
using Rudeus.View;
using Rudeus.Model;
using Rudeus.ViewModel;
using Device = Rudeus.Model.Device;
using System.ComponentModel;
using System.Runtime.CompilerServices;


//namespace Rudeus.ViewModel;
namespace Rudeus;

public partial class MainPage : ContentPage
{

    public HttpClient sharedClient = new HttpClient();

    private Device MyDevice { get; set; } = Device.Load();
    private MainPageViewModel MainPageViewModel { get; set; }


    // public string Text { get => $"DeviceName: {MyDevice?.DeviceName}"; }
    // public string Text2 { get => $"Username: {MyDevice?.Username}"; }
    // public string Text3 { get => $"AccessToken: {MyDevice?.AccessToken}"; }

    public MainPage()
	{
		InitializeComponent();
        // BindingContext = this;
        MainPageViewModel = new MainPageViewModel();
        BindingContext = MainPageViewModel;
        // Counter.Clicked += OnCounterClicked;
        // Init.Clicked += OnInitClicked;

    }

	private void OnLoginBtnClicked(object sender, EventArgs e)
	{
        //MainPageViewModel.OpenBrowser();
        // テスト用
        MainPageViewModel.LoginDeviceAsync();
        //Model.RemoteAPI.SAMLLoginAsync();
        //DisplayAlert("認証中…", "ブラウザでログイン操作をしてください", "キャンセル");
        
        
    }

    private void OnRegisterBtnClicked(object sender, EventArgs e)
    {
        // MyDevice.Register();
        MainPageViewModel.RegisterDevice();
    }

    private void OnUpdateBtnClicked(object sender, EventArgs e)
    {
        MainPageViewModel.UpdateDevice();
	}

}

