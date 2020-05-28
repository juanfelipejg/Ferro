using Ferroviario.Common.Models;
using Ferroviario.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class CreateChangePageViewModel : ViewModelBase
    {
        private ShiftResponse _shift;
        private ChangeResponse _change;
        public CreateChangePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.CreateChange;
        }

        public ChangeResponse Change
        {
            get => _change;
            set => SetProperty(ref _change, value);
        }
        public ShiftResponse Shift
        {
            get => _shift;
            set => SetProperty(ref _shift, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("shift"))
            {
                Shift = parameters.GetValue<ShiftResponse>("shift");
                Title = Shift.Service.Name;

            }
        }
    }
}
