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
    public class ChangesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<MyChangesItemViewModel> _changes;
        private bool _isRunning;
        public ChangesPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.MyChanges;
            LoadChangesAsync();
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<MyChangesItemViewModel> Changes
        {
            get => _changes;
            set => SetProperty(ref _changes, value);
        }

        private async void LoadChangesAsync()
        {
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var request = new ChangesForUserRequest
            {
                UserId = new Guid(user.Id),
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.GetChangesForUserAsync(url, "/api", "/Changes/GetChangesForUser", request, "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<ChangeResponse> list = (List<ChangeResponse>)response.Result;

            Changes = list.Select(c => new MyChangesItemViewModel(_navigationService)
            {
                Id = c.Id,
                FirstDriver = c.FirstDriver,
                FirstDriverService = c.FirstDriverService,
                SecondDriver = c.SecondDriver,
                SecondDriverService = c.SecondDriverService,
                State = c.State
            }).ToList();

        }
    }

}
