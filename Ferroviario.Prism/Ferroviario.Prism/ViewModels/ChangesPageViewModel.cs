using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class ChangesPageViewModel : ViewModelBase
    {
        public ChangesPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Changes";
        }
    }

}
