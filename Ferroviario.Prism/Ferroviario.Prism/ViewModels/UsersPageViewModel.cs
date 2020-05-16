using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class UsersPageViewModel : ViewModelBase
    {
        public UsersPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "My account";
        }
    }

}
