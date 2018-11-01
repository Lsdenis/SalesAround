using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Plugin.WebBrowser;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using SalesAround.CoreProject.DTO;

namespace SalesAround.CoreProject.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private const double Bound = 0.007;
        private readonly IMvxWebBrowserTask _browserTask;
        private List<Feature> _allFeatures;
        private IMvxCommand<Feature> _featureSelected;
        private double _previuosLatitude;
        private double _previuosLongitude;

        public MainViewModel(IMvxWebBrowserTask browserTask)
        {
            _browserTask = browserTask;
        }

        public List<Feature> FeaturesAround
        {
            get
            {
                if (_allFeatures == null || _allFeatures.Count == 0)
                {
                    return null;
                }

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

        public IMvxCommand<Feature> FeatureSelected =>
            _featureSelected ?? (_featureSelected = new MvxCommand<Feature>(FeatureSelectedExecute));

        private void FeatureSelectedExecute(Feature feature)
        {
            _browserTask.ShowWebPage($"https://www.slivki.by{feature.Properties.Url}");
        }

        public void OnLocationChanged(double latitude, double longitude)
        {
            _previuosLatitude = latitude;
            _previuosLongitude = longitude;

            RaisePropertyChanged(nameof(FeaturesAround));
        }

        public override async Task Initialize()
        {
            await LoadDataAsync();
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

            RaisePropertyChanged(nameof(FeaturesAround));
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