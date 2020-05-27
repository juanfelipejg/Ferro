using Ferroviario.Common.Helpers;
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
    public class RequestDetailPageViewModel : ViewModelBase
    {
        private readonly ITransformHelper _transformHelper;
        private RequestResponse _requests;

        public RequestDetailPageViewModel(INavigationService navigationService, ITransformHelper transformHelper) : base(navigationService)
        {
            Title = Languages.RequestDetail;
            _transformHelper = transformHelper;
        }

        public RequestResponse Requests
        {
            get => _requests;
            set => SetProperty(ref _requests, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("request"))
            {
                Requests = parameters.GetValue<RequestResponse>("request");                
                
            }
        }
    }
}
