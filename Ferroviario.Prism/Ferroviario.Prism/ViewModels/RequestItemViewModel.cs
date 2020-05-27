using Ferroviario.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Prism.ViewModels
{
    public class RequestItemViewModel : RequestResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectRequestCommand;

        public RequestItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectRequestCommand => _selectRequestCommand ?? (_selectRequestCommand = new DelegateCommand(SelectRequestAsync));

        private async void SelectRequestAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "request", this }
            };

            await _navigationService.NavigateAsync("RequestDetailPage", parameters);
        }

    }
}
