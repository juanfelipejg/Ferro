using Ferroviario.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferroviario.Prism.ViewModels
{
    public class ChangeItemViewModel : ShiftResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectChangeCommand;

        public ChangeItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectChangeCommand => _selectChangeCommand ?? (_selectChangeCommand = new DelegateCommand(SelectChangeAsync));

        private async void SelectChangeAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "shift", this }
            };

            await _navigationService.NavigateAsync("CreateChangePage", parameters);
        }
    }
}
