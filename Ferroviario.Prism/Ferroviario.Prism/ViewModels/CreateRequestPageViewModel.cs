using Ferroviario.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class CreateRequestPageViewModel : ViewModelBase
    {
        public CreateRequestPageViewModel(INavigationService navigationService): base(navigationService)
        {
            Title = Languages.NewRequest;
        }
    }
}
