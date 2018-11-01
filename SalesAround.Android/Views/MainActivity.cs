using System.Linq;
using Android;
using Android.App;
using Android.Content.PM;
using Android.Gms.Location;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using MvvmCross.Droid.Support.V7.AppCompat;
using SalesAround.CoreProject.ViewModels;

namespace SalesAround.Android.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false,
        ScreenOrientation = ScreenOrientation.Portrait, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this,
                    new[]
                    {
                        Manifest.Permission.AccessCoarseLocation, 
                        Manifest.Permission.AccessFineLocation
                    }, 1);
            }
            else
            {
                SubscribeToLocationChanges();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 1 && grantResults.All(permission => permission == Permission.Granted))
            {
                SubscribeToLocationChanges();
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SubscribeToLocationChanges()
        {
            var fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(this);
            var locationRequest = new LocationRequest()
                .SetPriority(LocationRequest.PriorityHighAccuracy)
                .SetInterval(60 * 1000 * 5)
                .SetFastestInterval(60 * 1000 * 2);

            var fusedLocationProviderCallback = new FusedLocationProviderCallback(ViewModel);
            fusedLocationProviderClient.RequestLocationUpdates(locationRequest, fusedLocationProviderCallback, null);
        }
    }

    public class FusedLocationProviderCallback : LocationCallback
    {
        private readonly MainViewModel _mainViewModel;

        public FusedLocationProviderCallback(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void OnLocationAvailability(LocationAvailability locationAvailability)
        {
        }

        public override void OnLocationResult(LocationResult result)
        {
            if (!result.Locations.Any())
            {
                return;
            }

            var location = result.Locations.First();
            _mainViewModel.OnLocationChanged(location.Latitude, location.Longitude);
        }
    }
}