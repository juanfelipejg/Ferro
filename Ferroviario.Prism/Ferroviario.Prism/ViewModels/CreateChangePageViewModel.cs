using Ferroviario.Common.Helpers;
using Ferroviario.Common.Models;
using Ferroviario.Common.Services;
using Ferroviario.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class CreateChangePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private CurrentShiftRequest _currentShiftRequest;
        private ChangeRequest _changeRequest;
        private ChangeResponse _changeResponse;
        private ShiftResponse _shiftResponse;
        private ShiftResponse _secShift;
        private UserResponse _user;
        private DelegateCommand _createChangeCommand;
        private bool _isRunning;
        private bool _isEnabled;
        
        public CreateChangePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            CurrentShiftRequest = new CurrentShiftRequest();
            ChangeRequest = new ChangeRequest();
            Title = Languages.CreateChange;
            LoadCurrentShift();
        }

        public DelegateCommand CreateChangeCommand => _createChangeCommand ?? (_createChangeCommand = new DelegateCommand(CreateChangeAsync));

        public CurrentShiftRequest CurrentShiftRequest
        {
            get => _currentShiftRequest;
            set => SetProperty(ref _currentShiftRequest, value);
        }
        public ChangeRequest ChangeRequest
        {
            get => _changeRequest;
            set => SetProperty(ref _changeRequest, value);
        }
        public ChangeResponse ChangeResponse
        {
            get => _changeResponse;
            set => SetProperty(ref _changeResponse, value);
        }
        public ShiftResponse ShiftResponse
        {
            get => _shiftResponse;
            set => SetProperty(ref _shiftResponse, value);
        }

        public ShiftResponse SecShift
        {
            get => _secShift;
            set => SetProperty(ref _secShift, value);
        }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void LoadCurrentShift()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            CurrentShiftRequest.UserId = new Guid(User.Id);

            CurrentShiftRequest.CultureInfo = Languages.Culture;

            Response response = await _apiService.GetShiftForChangeAsync(url, "/api", "/Shifts/GetShiftForUser", CurrentShiftRequest, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            SecShift = (ShiftResponse)response.Result;

        }

        private async void CreateChangeAsync()
        {
            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            ChangeRequest.Date = DateTime.Today.AddDays(1).ToUniversalTime();

            ChangeRequest.FirstDriverId = new Guid(User.Id);

            ChangeRequest.FirstShift =  SecShift.Id;

            ChangeRequest.SecondDriverId = new Guid(ShiftResponse.User.Id);

            ChangeRequest.SecondShift = ShiftResponse.Id;

            ChangeRequest.CultureInfo = Languages.Culture;

            Response response = await _apiService.CreateChangeAsync(url, "/api", "/Changes/PostChange", ChangeRequest, "bearer", token.Token);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Languages.Ok, Languages.ChangeSuccessfully, Languages.Accept);
                await _navigationService.GoBackAsync();
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("shift"))
            {
                ShiftResponse = parameters.GetValue<ShiftResponse>("shift");                              
            }
        }
    }
}
