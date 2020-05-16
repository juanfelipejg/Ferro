using Ferroviario.Common.Helpers;
using Ferroviario.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class ShiftDetailPageViewModel : ViewModelBase
    {
        private readonly ITransformHelper _transformHelper; //

        private ShiftResponse _shift;

        private Service  _service; //

        public ShiftDetailPageViewModel(INavigationService navigationService, ITransformHelper transformHelper) : base(navigationService)
        {
            _transformHelper = transformHelper;
            Title = "Shift Detail";
        }

        public Service Service
        {
            get => _service;
            set => SetProperty(ref _service, value);
        }


        /*public ShiftResponse Shift
        {
            get => _shift;
            set => SetProperty(ref _shift, value);
        }*/

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("shift"))
            {
                _shift = parameters.GetValue<ShiftResponse>("shift");
                Title = $"Service's Detail: { _shift.Service.Name}";
                Service = _transformHelper.ToService(_shift.Service);
            }
        }
    }


}

