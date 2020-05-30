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
    public class ConfirmChangePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private ConfirmChangeRequest _confirmChangeRequest;
        private ChangeResponse _changeResponse;
        private DelegateCommand _acceptChangeCommand;
        private bool _isRunning;
        private bool _isEnabled;

        public ConfirmChangePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            ConfirmChangeRequest = new ConfirmChangeRequest();
            Title = Languages.ConfirmChange;
        }

        public DelegateCommand AcceptChangeCommand => _acceptChangeCommand ?? (_acceptChangeCommand = new DelegateCommand(AcceptChangeAsync));

        public ConfirmChangeRequest ConfirmChangeRequest
        {
            get => _confirmChangeRequest;
            set => SetProperty(ref _confirmChangeRequest, value);
        }
        public ChangeResponse ChangeResponse
        {
            get => _changeResponse;
            set => SetProperty(ref _changeResponse, value);
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

        private async void AcceptChangeAsync()
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

            ConfirmChangeRequest.ChangeId = ChangeResponse.Id;

            ConfirmChangeRequest.FirstServiceId = ChangeResponse.FirstDriverService.Service.Id;

            ConfirmChangeRequest.SecondServiceId = ChangeResponse.SecondDriverService.Service.Id;

            ConfirmChangeRequest.CultureInfo = Languages.Culture;

            Response response = await _apiService.EditChangeAsync(url, "/api", "/Changes/PutChange", ConfirmChangeRequest, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
            await _navigationService.GoBackAsync();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("change"))
            {
                ChangeResponse = parameters.GetValue<ChangeResponse>("change");
            }
        }
    }
}
