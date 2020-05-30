using Ferroviario.Common.Helpers;
using Ferroviario.Common.Models;
using Ferroviario.Common.Services;
using Ferroviario.Prism.Helpers;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferroviario.Prism.ViewModels
{
    public class LookChangePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<ChangeItemViewModel> _shifts;
        private ShiftResponse _currentShift;
        private bool _isRunning;

        public LookChangePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.SearchChange;
            LoadShiftsAsync();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<ChangeItemViewModel> Shifts
        {
            get => _shifts;
            set => SetProperty(ref _shifts, value);
        }

        public ShiftResponse CurrentShift
        {
            get => _currentShift;
            set => SetProperty(ref _currentShift, value);
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

            Response response = await _apiService.GetShiftsForChangeAsync(url, "/api", "/Shifts/GetShiftsForChange", request, "bearer", token.Token);

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
            }

            List<ShiftResponse> list = (List<ShiftResponse>)response.Result;

            ShiftResponse CurrentShift = list.FirstOrDefault(s => s.User == user);

            List<ShiftResponse> list2 =  list.Where(s => s.User.Id != user.Id).ToList();

            Shifts = list2.Select(t => new ChangeItemViewModel(_navigationService)
            {
                Id = t.Id,
                User = t.User,  
                Service = t.Service,
                Date = t.Date,
            }).ToList();

        }

    }
}
