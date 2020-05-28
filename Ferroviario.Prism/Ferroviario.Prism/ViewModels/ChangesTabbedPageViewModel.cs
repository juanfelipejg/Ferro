using Ferroviario.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class ChangesTabbedPageViewModel : ViewModelBase
    {
        public ChangesTabbedPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.Changes;
        }
    }
}
