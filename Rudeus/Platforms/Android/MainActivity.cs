using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Rudeus;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density
)]
[IntentFilter(
    actions: new[] { Android.Content.Intent.ActionView }
  , Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable }
  , DataHost = "callback"
  , DataScheme = "rudeus.client"
)]
public class MainActivity : MauiAppCompatActivity
{
}
