using MvvmCross.Plugin.Messenger;

namespace SalesAround.Core.Messages
{
    public class LocationMessage : MvxMessage
    {
        public LocationMessage(object sender, double lat, double lng)
            : base(sender)
        {
            Lng = lng;
            Lat = lat;
        }

        public double Lat { get; }

        public double Lng { get; }
    }
}