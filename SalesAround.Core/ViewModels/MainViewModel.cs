using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using SalesAround.Core.DTO;
using SalesAround.Core.Messages;
using SalesAround.Core.Services;

namespace SalesAround.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private const double Bound = 0.007;
        private List<Feature> _allFeatures;
        private double _previuosLatitude;
        private double _previuosLongitude;
        private MvxSubscriptionToken _token;

        private static int number = 0;

        public MainViewModel(IMvxMessenger messenger)
        {
            _token = messenger.Subscribe<LocationMessage>(OnLocationChanged);
            number++;
        }

        public List<Feature> FeaturesAround
        {
            get
            {
                var searchBounds = 0.007;

                var searchLatitudeStart = _previuosLatitude - searchBounds;
                var searchLatitudeEnd = _previuosLatitude + searchBounds;
                var searchLongitudeStart = _previuosLongitude - searchBounds;
                var searchLongitudeEnd = _previuosLongitude + searchBounds;

                return _allFeatures.Where(feature =>
                        feature.Geometry.Coordinates[0] > searchLatitudeStart &&
                        feature.Geometry.Coordinates[0] < searchLatitudeEnd &&
                        feature.Geometry.Coordinates[1] > searchLongitudeStart &&
                        feature.Geometry.Coordinates[1] < searchLongitudeEnd)
                    .ToList();
            }
        }

        private void OnLocationChanged(LocationMessage message)
        {
            _previuosLatitude = message.Lat;
            _previuosLongitude = message.Lng;

            RaisePropertyChanged(nameof(FeaturesAround));
        }

        public override async Task Initialize()
        {
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var allFeatures = new List<Feature>();
            allFeatures.AddRange(await DownloadData("0/200/0"));
            allFeatures.AddRange(await DownloadData("0/1000/200"));
            allFeatures.AddRange(await DownloadData("0/1000/1200"));
            allFeatures.AddRange(await DownloadData("0/1000/2200"));
            allFeatures.AddRange(await DownloadData("0/1000/3200"));

            _allFeatures = allFeatures;
        }

        private async Task<List<Feature>> DownloadData(string param)
        {
            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"https://www.slivki.by/sale/location-info/{param}");

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new List<Feature>();
                }

                return JsonConvert.DeserializeObject<SlivkiSalesDTO>(await response.Content.ReadAsStringAsync())
                    .Features.ToList();
            }
            catch (Exception)
            {
                // ignored
            }

            return new List<Feature>();
        }
    }
}