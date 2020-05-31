using Ferroviario.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Prism.ViewModels
{
    public class MyChangesItemViewModel:ChangeResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _confirmChangeCommand;
        private DelegateCommand _refresh;

        public MyChangesItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand ConfirmChangeCommand => _confirmChangeCommand ?? (_confirmChangeCommand = new DelegateCommand(ConfirmChangeAsync));
                
        private async void ConfirmChangeAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "change", this }
            };

            await _navigationService.NavigateAsync("ConfirmChangePage", parameters);
        }
    }
}
