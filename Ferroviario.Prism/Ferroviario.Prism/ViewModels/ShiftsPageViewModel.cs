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
using Xamarin.Forms;

namespace Ferroviario.Prism.ViewModels
{
    public class ShiftsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<ShiftItemViewModel> _shifts;
        private bool _isRunning;

        public ShiftsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.MyShifts;
            LoadShiftsAsync();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<ShiftItemViewModel> Shifts
        {
            get => _shifts;
            set => SetProperty(ref _shifts, value);

        }

        private async void LoadShiftsAsync()
        {
            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var request = new ShiftsForUserRequest
            {
                UserId = new Guid(user.Id),
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.GetShiftsForUserAsync(url, "/api", "/Shifts/GetShiftsForUser", request, "bearer", token.Token);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
            }

            List<ShiftResponse> list = (List<ShiftResponse>)response.Result;
            Shifts = list.Select(t => new ShiftItemViewModel(_navigationService)
            {
                Id = t.Id,
                User = t.User,
                Service = t.Service,
                Date = t.Date,                
            }).ToList(); 

        }
    }
}
