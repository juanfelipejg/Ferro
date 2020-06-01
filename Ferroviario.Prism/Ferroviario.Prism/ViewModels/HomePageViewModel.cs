using Ferroviario.Common.Services;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Ferroviario.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGeolocatorService _geolocatorService;
        private string _source;
        private string _buttonLabel;
        private DelegateCommand _getAddressCommand;
        private DelegateCommand _sendAddressCommand;

        public HomePageViewModel(INavigationService navigationService, IGeolocatorService geolocatorService) : base(navigationService)
        {
            _navigationService = navigationService;
            _geolocatorService = geolocatorService;
            Title = "Select address";
        }

        public DelegateCommand GetAddressCommand => _getAddressCommand ?? (_getAddressCommand = new DelegateCommand(LoadSourceAsync));
        public DelegateCommand SendAddressCommand => _sendAddressCommand ?? (_sendAddressCommand = new DelegateCommand(SendAdressAsync));
        public string Source
        {
            get => _buttonLabel;
            set => SetProperty(ref _buttonLabel, value);
        }

        public string ButtonLabel
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private async void LoadSourceAsync()
        {
            await _geolocatorService.GetLocationAsync();
            if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
            {
                Position position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
                Geocoder geoCoder = new Geocoder();
                IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(position);
                List<string> addresses = new List<string>(sources);

                if (addresses.Count > 0)
                {
                    Source = addresses[0];
                }
            }
        }

        private async void SendAdressAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "source", Source }
            };

            await _navigationService.NavigateAsync("ReportPage", parameters);
        }

    }
}
