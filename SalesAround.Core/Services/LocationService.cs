using MvvmCross.Plugin.Location;
using MvvmCross.Plugin.Messenger;
using SalesAround.Core.Messages;

namespace SalesAround.Core.Services
{
    public class LocationService : ILocationService
    {
        private readonly IMvxMessenger _messenger;
        private readonly IMvxLocationWatcher _watcher;

        public LocationService(IMvxLocationWatcher watcher, IMvxMessenger messenger)
        {
            _watcher = watcher;
            _messenger = messenger;
//            _watcher.Start(new MvxLocationOptions(), OnLocation, OnError);
        }

        private void OnLocation(MvxGeoLocation location)
        {
            var message = new LocationMessage(this,
                location.Coordinates.Latitude,
                location.Coordinates.Longitude);

            _messenger.Publish(message);
        }

        private void OnError(MvxLocationError error)
        {
        }
    }
}