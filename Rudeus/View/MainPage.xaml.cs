using System.Net.Http;
using Rudeus.View;
using Rudeus.Model;
using Rudeus.ViewModel;
using Device = Rudeus.Model.Device;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Rudeus.View;

public partial class MainPage : ContentPage
{

    public HttpClient sharedClient = new HttpClient();

    private MainPageViewModel MainPageViewModel { get; set; }


    public MainPage()
	{
		InitializeComponent();
        MainPageViewModel = new MainPageViewModel();

        // Viewモデルをデータバインド
        BindingContext = MainPageViewModel;
    }

	private void OnLoginBtnClicked(object sender, EventArgs e)
	{  
        // ViewModelのログイン処理を発火
        MainPageViewModel.LoginDeviceAsync();
        //DisplayAlert("認証中…", "ブラウザでログイン操作をしてください", "キャンセル");
    }

    private void OnRegisterBtnClicked(object sender, EventArgs e)
    {
#if WINDOWS
        Shell.Current.FlyoutIsPresented = true;
#endif
        // ViewModelの登録処理を発火
        MainPageViewModel.RegisterDevice();
    }

    private void OnUpdateBtnClicked(object sender, EventArgs e)
    {
        // ViewModelのアップデート処理を発火
        MainPageViewModel.UpdateDevice();
	}

    private void OnResetBtnClicked(object sender, EventArgs e)
    {
        // ViewModelのDebugBoxリセット処理を発火
        MainPageViewModel.UpdateDebugBoxText("");
    }

}

