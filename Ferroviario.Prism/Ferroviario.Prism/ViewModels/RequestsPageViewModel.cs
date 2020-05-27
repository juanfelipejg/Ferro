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
    public class RequestsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<RequestItemViewModel> _requests;        
        private bool _isRunning;
        public RequestsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.MyRequests;
            LoadRequestsAsync();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<RequestItemViewModel> Requests
        {
            get => _requests;
            set => SetProperty(ref _requests, value);
        }

        private async void LoadRequestsAsync()
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

            var request = new RequestsForUserRequest
            {                
                UserId = new Guid(user.Id),
                CultureInfo = Languages.Culture
            };

            Response response = await _apiService.GetRequestsForUserAsync(url, "/api", "/Requests/GetRequestsForUser", request, "bearer", token.Token);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            
            List<RequestResponse> list = (List<RequestResponse>)response.Result;
            Requests = list.Select(t => new RequestItemViewModel(_navigationService)
            {
                Id = t.Id,
                Type = t.Type,
                InitialDate = t.InitialDate,
                FinishDate = t.FinishDate,
                Description = t.Description,
                State = t.State,
                Comment = t.Comment,
                User = t.User
            }).ToList();

        }
    }

}

