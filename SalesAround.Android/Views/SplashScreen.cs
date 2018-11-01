using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;

namespace SalesAround.Android.Views
{
    [Activity(Label = "Sales Around", MainLauncher = true, NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
    }
}