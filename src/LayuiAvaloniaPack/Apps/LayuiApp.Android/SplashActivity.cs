using Android.App;
using Android.Content;
using Android.OS;
using Avalonia.Android;
using Avalonia; 
using Application = Android.App.Application;
using Layui.Main;

namespace LayuiApp.Android
{
    [Activity(MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AvaloniaSplashActivity<App>
    {
        protected override void OnResume()
        {
            base.OnResume();

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
