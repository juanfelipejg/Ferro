using Ferroviario.Common.Models;
using Ferroviario.Common.Services;
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
        private readonly IApiService _apiService;
        private List<ShiftResponse> _shifts;
        public ShiftsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "My Shifts";
            LoadShiftsAsync();

        }

        public List<ShiftResponse> Shifts
        {
            get => _shifts;
            set => SetProperty(ref _shifts, value);
        }

        private async void LoadShiftsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<ShiftResponse>(
                url,
                "/api",
                "/Shifts");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Shifts = (List<ShiftResponse>)response.Result;
        }
    }
}
