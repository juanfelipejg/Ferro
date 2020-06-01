using Ferroviario.Common.Helpers;
using Ferroviario.Common.Models;
using Ferroviario.Common.Services;
using Ferroviario.Prism.Helpers;
using Ferroviario.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Ferroviario.Prism.ViewModels
{
    public class FerroviarioMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private static FerroviarioMasterDetailPageViewModel _instance;
        private DelegateCommand _modifyUserCommand;
        private UserResponse _user;

        public FerroviarioMasterDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _apiService = apiService;
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }
        public DelegateCommand ModifyUserCommand => _modifyUserCommand ?? (_modifyUserCommand = new DelegateCommand(ModifyUserAsync));
        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public static FerroviarioMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }

        public async void ReloadUser()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = user.Email
            };

            Response response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            UserResponse userResponse = (UserResponse)response.Result;
            Settings.User = JsonConvert.SerializeObject(userResponse);
            LoadUser();
        }


        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "request",
                    PageName = "RequestsPage",
                    Title = Languages.MyRequests,
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "shift",
                    PageName = "ShiftsPage",
                    Title = Languages.MyShifts,
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "change",
                    PageName = "ChangesTabbedPage",
                    Title = Languages.Changes,
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "UsersPage",
                    Title = Languages.User,
                    IsLoginRequired = true
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "ReportPage",
                    Title = "Report",
                    IsLoginRequired = false
                },
                new Menu
                {
                 Icon = "login",
                 PageName = "LoginPage",
                 Title = Settings.IsLogin ? Languages.Logout : Languages.Login
                }

            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }

        private async void ModifyUserAsync()
        {
            await _navigationService.NavigateAsync($"/FerroviarioMasterDetailPage/NavigationPage/{nameof(UsersPage)}");
        }
    }

}

