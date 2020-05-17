using Ferroviario.Common.Models;
using Ferroviario.Common.Services;
using Ferroviario.Prism.Helpers;
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
        private readonly IApiService _apiService;
        private List<RequestResponse> _requests;
        public RequestsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = Languages.MyRequests;
            LoadRequestsAsync();
        }

        public List<RequestResponse> Requests
        {
            get => _requests;
            set => SetProperty(ref _requests, value);
        }


        private async void LoadRequestsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<RequestResponse>(
                url,
                "/api",
                "/Requests");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Requests = (List<RequestResponse>)response.Result;
        }
    }

}

