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
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Prism.ViewModels
{
    public class CreateRequestPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private RequestRequest _request;
        private RequestTypeResponse _type;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _registerRequestCommand;
        private ObservableCollection<RequestTypeResponse> _types;
        public CreateRequestPageViewModel(INavigationService navigationService, IApiService apiService): base(navigationService)
        {
            Title = Languages.NewRequest;
            _navigationService = navigationService;
            _apiService = apiService;
            IsEnabled = true;
            Request = new RequestRequest();
            LoadTypesAsync();
        }

        public DelegateCommand RegisterRequestCommand => _registerRequestCommand ?? (_registerRequestCommand = new DelegateCommand(RegisterRequestAsync));

        public RequestRequest Request
        {
            get => _request;
            set => SetProperty(ref _request, value);
        }
        public RequestTypeResponse Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
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

        public ObservableCollection<RequestTypeResponse> Types
        {
            get => _types;
            set => SetProperty(ref _types, value);
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (Type.Id == 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.RequestTypeError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Request.InitialDate.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DateError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Request.FinishDate.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.DateError, Languages.Accept);
                return false;
            }            

            return true;
        }

        private async void RegisterRequestAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

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

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            Request.UserId = new Guid(user.Id);

            Request.TypeId = Type.Id;

            Request.CultureInfo = Languages.Culture;

            Response response = await _apiService.RegisterRequestAsync(url, "/api", "/Requests/PostRequest", Request, "bearer", token.Token);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
                await _navigationService.GoBackAsync();
            }


        }

        private async void LoadTypesAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            Response response = await _apiService.GetListAsync<RequestTypeResponse>(url, "/api", "/RequestType");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            List<RequestTypeResponse> list = (List<RequestTypeResponse>)response.Result;
            Types = new ObservableCollection<RequestTypeResponse>(list.OrderBy(t => t.Type));
        }
    }
}
