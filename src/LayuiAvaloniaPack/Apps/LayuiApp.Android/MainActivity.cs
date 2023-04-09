
using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Avalonia.Android;
using static Android.Content.Res.Resources;

namespace LayuiApp.Android
{
    [Activity(Label = "LayuiApp.Android", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]

    public class MainActivity : AvaloniaMainActivity
    {

    }
}
